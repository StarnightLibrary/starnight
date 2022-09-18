namespace Starnight.Internal.Gateway.Services;

using System;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Starnight.Exceptions;
using Starnight.Internal.Gateway.Payloads;

/// <summary>
/// Transports payloads to and from pipes into the websocket to communicate with discord.
/// </summary>
public class TransportService : IAsyncDisposable
{
	private readonly ILogger<TransportService> __logger;
	private readonly ClientWebSocket __socket;
	private readonly DiscordGatewayRestResource __gateway_resource;
	private readonly Channel<IDiscordGatewayEvent> __inbound_channel;
	private readonly Channel<IDiscordGatewayEvent> __outbound_channel;

	private readonly MemoryStream __reading_stream;
	private readonly Byte[] __reading_raw_buffer;
	private readonly Memory<Byte> __reading_buffer;

	private readonly MemoryStream __writing_stream;
	private readonly Byte[] __writing_raw_buffer;
	private readonly ReadOnlyMemory<Byte> __writing_buffer;

	private CancellationToken __ct;
	private Boolean __is_connected = false;
	private Boolean __is_disposed = false;

	/// <summary>
	/// Gets or sets the URL which should be used for resuming a session.
	/// </summary>
	public String? ResumeUrl { get; set; }


	/// <summary>
	/// Constructs a new TransportService.
	/// </summary>
	public TransportService
	(
		ILogger<TransportService> logger,
		DiscordGatewayRestResource gatewayResource
	)
	{
		this.__logger = logger;
		this.__socket = new();
		this.__gateway_resource = gatewayResource;

		this.__inbound_channel = Channel.CreateUnbounded<IDiscordGatewayEvent>();
		this.__outbound_channel = Channel.CreateUnbounded<IDiscordGatewayEvent>();

		this.Inbound = this.__inbound_channel.Reader;
		this.Outbound = this.__outbound_channel.Writer;

		this.__reading_stream = new();

		this.__reading_raw_buffer = new Byte[4096];
		this.__reading_buffer = new(this.__reading_raw_buffer);

		this.__writing_stream = new();
		this.__writing_raw_buffer = new Byte[4096];

		this.__writing_buffer = new(this.__writing_raw_buffer);
	}

	/// <summary>
	/// Represents the inbound side of the gateway connection.
	/// </summary>
	public ChannelReader<IDiscordGatewayEvent> Inbound { get; private set; }

	/// <summary>
	/// Represents the outbound side of the gateway connection.
	/// </summary>
	public ChannelWriter<IDiscordGatewayEvent> Outbound { get; private set; }

	/// <summary>
	/// Connects to the Discord websocket.
	/// </summary>
	/// <param name="ct">The cancellation token to use for this current connection.</param>
	/// <exception cref="StarnightGatewayConnectionRefusedException">
	/// Thrown if the session start limit for the day had been exceeded.
	/// </exception>
	public async ValueTask ConnectAsync(CancellationToken ct = default)
	{
		if(this.__is_connected)
		{
			this.__logger.LogWarning
			(
				"Attempted to connect, but there already is a connection opened. Ignoring."
			);

			return;
		}

		this.__ct = ct;

		if(this.ResumeUrl is null)
		{
			GetGatewayBotResponsePayload connectionObject = await this.__gateway_resource.GetBotGatewayInfoAsync();

			this.__logger.LogDebug
			(
				"Attempting to connect to the Discord gateway, recommending {shards} shards.",
				connectionObject.Shards
			);

			if(connectionObject.SessionStartLimit.Remaining == 0)
			{
				this.__logger.LogError
				(
					"Maximum session starts exceeded - wait {time} before attempting another start.",
					TimeSpan.FromMilliseconds(connectionObject.SessionStartLimit.ResetAfter)
				);

				throw new StarnightGatewayConnectionRefusedException
				(
					"Session start limit reached.",
					connectionObject.SessionStartLimit
				);
			}

			this.__logger.LogInformation
			(
				"Connecting. Remaining session starts: {remaining}/{total}.\n" +
				"This limit resets in {time}",
				connectionObject.SessionStartLimit.Remaining,
				connectionObject.SessionStartLimit.Total,
				TimeSpan.FromMilliseconds(connectionObject.SessionStartLimit.ResetAfter)
			);

			await this.__socket.ConnectAsync
			(
				new($"{connectionObject.Url}?v={DiscordApiConstants.ApiVersion}&encoding=json"),
				this.__ct
			);
		}
		else
		{
			this.__logger.LogInformation
			(
				"Attempting to resume existing session."
			);

			this.__socket.Dispose();

			await this.__socket.ConnectAsync
			(
				new($"{this.ResumeUrl}?v={DiscordApiConstants.ApiVersion}&encoding=json"),
				this.__ct
			);
		}

		this.__is_connected = true;

		this.__logger.LogInformation
		(
			"Connected to the Discord websocket."
		);

		_ = Task.Factory.StartNew(async () => await this.readAsync());
		_ = Task.Factory.StartNew(async () => await this.writeAsync());
	}

	/// <summary>
	/// Reads all payloads and enqueues them into the channel.
	/// </summary>
	private async ValueTask readAsync()
	{
		if(!this.__is_connected)
		{
			this.__logger.LogWarning
			(
				"Attempting to read from the Discord gateway, but there was no open connection. Ignoring."
			);
		}

		if(this.__socket.State != WebSocketState.Open)
		{
			this.__logger.LogWarning
			(
				"Attempting to read from the Discord gateway, but the socket was not open. " +
				"Current socket state: {state}",
				this.__socket.State.ToString()
			);
		}

		while(!this.__ct.IsCancellationRequested)
		{
			ValueWebSocketReceiveResult receiveResult;

			try
			{
				do
				{
					receiveResult = await this.__socket.ReceiveAsync(this.__reading_buffer, this.__ct);

					this.__reading_stream.Write(this.__reading_raw_buffer, 0, receiveResult.Count);

				} while(!receiveResult.EndOfMessage);
			}
			catch(OperationCanceledException) { }

			_ = this.__reading_stream.Seek(0, SeekOrigin.Begin);

			IDiscordGatewayEvent @event = JsonSerializer.Deserialize<IDiscordGatewayEvent>
			(
				this.__reading_stream,
				StarnightInternalConstants.DefaultSerializerOptions
			)!;

			this.__logger.LogTrace
			(
				"Inbound gateway event received:\n{event}",
				@event.ToString()
			);

#if DEBUG
			_ = this.__reading_stream.Seek(0, SeekOrigin.Begin);

			this.__logger.LogTrace
			(
				"Decompressed payload for the last inbound gateway event:\n{event}",
				Encoding.UTF8.GetString(this.__reading_stream.ToArray())
			);
#endif

			await this.__inbound_channel.Writer.WriteAsync(@event, this.__ct);

		}
	}

	/// <summary>
	/// Writes all payloads passed through the channel.
	/// </summary>
	private async ValueTask writeAsync()
	{
		if(!this.__is_connected)
		{
			this.__logger.LogWarning
			(
				"Attempting to read from the Discord gateway, but there was no open connection. Ignoring."
			);
		}

		if(this.__socket.State != WebSocketState.Open)
		{
			this.__logger.LogWarning
			(
				"Attempting to read from the Discord gateway, but the socket was not open. " +
				"Current socket state: {state}",
				this.__socket.State.ToString()
			);
		}

		while(!this.__ct.IsCancellationRequested)
		{
			IDiscordGatewayEvent @event = await this.__outbound_channel.Reader.ReadAsync();

			this.__logger.LogTrace
			(
				"Sending outbound gateway event:\n{event}",
				@event.ToString()
			);

			JsonSerializer.Serialize(this.__writing_stream, @event, StarnightInternalConstants.DefaultSerializerOptions);

			_ = this.__writing_stream.Seek(0, SeekOrigin.Begin);

			if(this.__writing_stream.Length > 4096)
			{
				throw new StarnightInvalidOutboundEventException
				(
					"The outbound event exceeded 4096 bytes after serialization",
					@event
				);
			}

			this.__writing_stream.Read(this.__writing_raw_buffer, 0, 4096);

#if DEBUG

			this.__logger.LogTrace
			(
				"Serialized payload for the last outbound event:\n{event}",
				Encoding.UTF8.GetString(this.__writing_raw_buffer)
			);
#endif

			await this.__socket.SendAsync
			(
				this.__writing_buffer[..(Int32)this.__writing_stream.Length],
				WebSocketMessageType.Text,
				true,
				this.__ct
			);
		}
	}

	/// <summary>
	/// Closes the current websocket connection.
	/// </summary>
	/// <param name="reconnect">Whether reconnection is intended. If so, this must be set to true.</param>
	/// <param name="closeStatus">The status message to give to the websocket.</param>
	public async ValueTask DisconnectAsync
	(
		Boolean reconnect,
		WebSocketCloseStatus closeStatus
	)
	{
		if(!this.__is_connected)
		{
			this.__logger.LogWarning
			(
				"Attempting to disconnect from the Discord gateway, but there was no open connection. Ignoring."
			);
		}

		switch(this.__socket.State)
		{
			case WebSocketState.CloseSent:
			case WebSocketState.CloseReceived:
			case WebSocketState.Closed:
			case WebSocketState.Aborted:

				this.__logger.LogWarning
				(
					"Attempting to disconnect from the Discord gateway, but there is a disconnect in progress or complete. " +
					"Current websocket state: {state}",
					this.__socket.State.ToString()
				);

				return;

			case WebSocketState.Open:
			case WebSocketState.Connecting:

				this.__logger.LogDebug
				(
					"Disconnecting. Current websocket state: {state}",
					this.__socket.State.ToString()
				);

				try
				{
					await this.__socket.CloseAsync
					(
						reconnect ? (WebSocketCloseStatus)1012 : closeStatus,
						"Disconnecting.",
						this.__ct
					);
				}
				catch(WebSocketException) { }
				catch(OperationCanceledException) { }

				break;
		}

		this.__is_connected = false;

		if(!reconnect)
		{
			this.__socket.Dispose();
		}
	}

	public async ValueTask DisposeAsync()
	{
		if(!this.__is_disposed)
		{
			if(this.__is_connected)
			{
				await this.DisconnectAsync(false, WebSocketCloseStatus.NormalClosure);
			}
		}

		this.__is_disposed = true;

		GC.SuppressFinalize(this);
	}
}

namespace Starnight.Internal.Gateway.Services;

using System;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Starnight.Exceptions;
using Starnight.Internal.Gateway.Payloads;

/// <summary>
/// Transports payloads to and from pipes into the websocket to communicate with discord.
/// </summary>
public class TransportService : IAsyncDisposable
{
	private readonly ILogger<TransportService> logger;
	private readonly ClientWebSocket socket;
	private readonly DiscordGatewayRestResource gatewayRestResource;

	private readonly Byte[] readingRawBuffer;
	private readonly Memory<Byte> readingBuffer;

	private readonly Byte[] writingRawBuffer;
	private readonly ReadOnlyMemory<Byte> writingBuffer;

	private readonly MemoryStream readingStream;
	private readonly MemoryStream writingStream;
	private readonly SemaphoreSlim readingSemaphore;
	private readonly SemaphoreSlim writingSemaphore;

	private Boolean isConnected = false;
	private Boolean isDisposed = false;

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
		this.logger = logger;
		this.socket = new();
		this.socket.Options.KeepAliveInterval = TimeSpan.Zero;
		this.gatewayRestResource = gatewayResource;

		this.readingRawBuffer = new Byte[4096];
		this.readingBuffer = new(this.readingRawBuffer);

		this.writingRawBuffer = new Byte[4096];
		this.writingBuffer = new(this.writingRawBuffer);

		this.readingStream = new();
		this.writingStream = new();
		this.readingSemaphore = new(1);
		this.writingSemaphore = new(1);
	}

	/// <summary>
	/// Connects to the Discord websocket.
	/// </summary>
	/// <param name="ct">The cancellation token to use for this current connection.</param>
	/// <exception cref="StarnightGatewayConnectionRefusedException">
	/// Thrown if the session start limit for the day had been exceeded.
	/// </exception>
	public async ValueTask ConnectAsync(CancellationToken ct = default)
	{
		if(this.isConnected)
		{
			this.logger.LogWarning
			(
				"Attempted to connect, but there already is a connection opened. Ignoring."
			);

			return;
		}

		if(this.ResumeUrl is null)
		{
			GetGatewayBotResponsePayload connectionObject = await this.gatewayRestResource.GetBotGatewayInfoAsync();

			this.logger.LogDebug
			(
				"Attempting to connect to the Discord gateway, recommending {shards} shards.",
				connectionObject.Shards
			);

			if(connectionObject.SessionStartLimit.Remaining == 0)
			{
				this.logger.LogError
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

			this.logger.LogInformation
			(
				"Connecting. Remaining session starts: {remaining}/{total}.\n" +
				"This limit resets in {time}",
				connectionObject.SessionStartLimit.Remaining,
				connectionObject.SessionStartLimit.Total,
				TimeSpan.FromMilliseconds(connectionObject.SessionStartLimit.ResetAfter)
			);

			await this.socket.ConnectAsync
			(
				new($"{connectionObject.Url}?v={DiscordApiConstants.ApiVersion}&encoding=json"),
				ct
			);
		}
		else
		{
			this.logger.LogInformation
			(
				"Attempting to resume existing session."
			);

			await this.socket.ConnectAsync
			(
				new($"{this.ResumeUrl}?v={DiscordApiConstants.ApiVersion}&encoding=json"),
				ct
			);
		}

		this.isConnected = true;

		this.logger.LogInformation
		(
			"Connected to the Discord websocket."
		);

	}

	/// <summary>
	/// Reads all payloads and enqueues them into the channel.
	/// </summary>
	internal async ValueTask<InboundGatewayFrame> ReadAsync(CancellationToken ct)
	{
		if(this.isDisposed)
		{
			return InboundGatewayFrame.Disposed;
		}

		if(this.socket.State != WebSocketState.Open)
		{
			return InboundGatewayFrame.NotConnected;
		}

		await this.readingSemaphore.WaitAsync
		(
			ct
		);

		ValueWebSocketReceiveResult receiveResult;

		this.readingStream.SetLength(0);

		try
		{
			do
			{
				receiveResult = await this.socket.ReceiveAsync(this.readingBuffer, ct);

				this.readingStream.Write(this.readingRawBuffer, 0, receiveResult.Count);

			} while(!receiveResult.EndOfMessage);
		}
		catch(OperationCanceledException) { }

#if DEBUG
		this.readingStream.Position = 0;

		this.logger.LogTrace
		(
			"Length for the last inbound gatway event: {length}",
			this.readingStream.Length
		);

		this.logger.LogTrace
		(
			"Payload for the last inbound gateway event:\n{event}",
			Encoding.UTF8.GetString(this.readingStream.ToArray())
		);
#endif

		if(this.readingStream.Length == 0)
		{
			return InboundGatewayFrame.EmptyResponse;
		}

		this.readingStream.Position = 0;

		IDiscordGatewayEvent @event = JsonSerializer.Deserialize<IDiscordGatewayEvent>
		(
			this.readingStream,
			StarnightInternalConstants.DefaultSerializerOptions
		)!;

		this.logger.LogTrace
		(
			"Inbound gateway event received:\n{event}",
			@event.ToString()
		);

		this.readingSemaphore.Release();

		return new()
		{
			Event = @event,
			IsDisconnected = false,
			IsDisposed = false
		};
	}

	/// <summary>
	/// Writes all payloads passed through the channel.
	/// </summary>
	internal async ValueTask WriteAsync
	(
		IDiscordGatewayEvent @event,
		CancellationToken ct
	)
	{
		this.logger.LogTrace
		(
			"Sending outbound gateway event:\n{event}",
			@event.ToString()
		);

		await this.writingSemaphore.WaitAsync
		(
			ct
		);

		this.writingStream.SetLength(0);

		JsonSerializer.Serialize
		(
			this.writingStream,
			@event,
			StarnightInternalConstants.DefaultSerializerOptions
		);

		this.writingStream.Position = 0;

		if(this.writingStream.Length > 4096)
		{
			throw new StarnightInvalidOutboundEventException
			(
				"The outbound event exceeded 4096 bytes after serialization",
				@event
			);
		}

		this.writingStream.Read(this.writingRawBuffer, 0, 4096);

#if DEBUG

		this.logger.LogTrace
		(
			"Serialized payload for the last outbound event:\n{event}",
			Encoding.UTF8.GetString(this.writingStream.ToArray())
		);
#endif

		await this.socket.SendAsync
		(
			this.writingBuffer[..(Int32)this.writingStream.Length],
			WebSocketMessageType.Text,
			true,
			ct
		);

		this.writingSemaphore.Release();
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
		if(!this.isConnected)
		{
			this.logger.LogWarning
			(
				"Attempting to disconnect from the Discord gateway, but there was no open connection. Ignoring."
			);
		}

		switch(this.socket.State)
		{
			case WebSocketState.CloseSent:
			case WebSocketState.CloseReceived:
			case WebSocketState.Closed:
			case WebSocketState.Aborted:

				this.logger.LogWarning
				(
					"Attempting to disconnect from the Discord gateway, but there is a disconnect in progress or complete. " +
					"Current websocket state: {state}",
					this.socket.State.ToString()
				);

				return;

			case WebSocketState.Open:
			case WebSocketState.Connecting:

				this.logger.LogDebug
				(
					"Disconnecting. Current websocket state: {state}",
					this.socket.State.ToString()
				);

				try
				{
					await this.socket.CloseAsync
					(
						reconnect ? (WebSocketCloseStatus)1012 : closeStatus,
						"Disconnecting.",
						CancellationToken.None
					);
				}
				catch(WebSocketException) { }
				catch(OperationCanceledException) { }

				break;
		}

		this.isConnected = false;

		if(!reconnect)
		{
			this.socket.Dispose();
		}
	}

	public async ValueTask DisposeAsync()
	{
		if(!this.isDisposed)
		{
			if(this.isConnected)
			{
				await this.DisconnectAsync(false, WebSocketCloseStatus.NormalClosure);
			}
		}

		this.isDisposed = true;

		GC.SuppressFinalize(this);
	}
}

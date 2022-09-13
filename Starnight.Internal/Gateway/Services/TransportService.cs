namespace Starnight.Internal.Gateway.Services;

using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Starnight.Internal.Exceptions;
using Starnight.Internal.Gateway.Payloads;

/// <summary>
/// Transports payloads to and from pipes into the websocket to communicate with discord.
/// </summary>
public class TransportService : IAsyncDisposable
{
	private readonly ILogger<TransportService> __logger;
	private readonly ClientWebSocket __socket;
	private readonly DiscordGatewayRestResource __gateway_resource;
	private readonly Channel<IDiscordGatewayEvent> __channel;

	private CancellationToken __ct;
	private Boolean __is_connected = false;
	private Boolean __is_disposed = false;

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

		this.__channel = Channel.CreateUnbounded<IDiscordGatewayEvent>();

		this.Inbound = this.__channel.Reader;
		this.Outbound = this.__channel.Writer;
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
			new(connectionObject.Url),
			this.__ct
		);

		this.__is_connected = true;

		this.__logger.LogInformation
		(
			"Connected to the Discord websocket."
		);
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
					"Attempting to disconnect from the Discord gateway, but there is a disconnect in progress or complete." +
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
			this.__is_disposed = true;
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

		GC.SuppressFinalize(this);
	}
}

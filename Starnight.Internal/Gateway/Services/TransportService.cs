namespace Starnight.Internal.Gateway.Services;

using System;
using System.IO.Pipelines;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Starnight.Internal.Exceptions;
using Starnight.Internal.Gateway.Payloads;

/// <summary>
/// Transports payloads to and from pipes into the websocket to communicate with discord.
/// </summary>
public class TransportService : IDuplexPipe
{
	private readonly ILogger<TransportService> __logger;
	private readonly Pipe __pipe;
	private readonly ClientWebSocket __socket;
	private readonly DiscordGatewayRestResource __gateway_resource;
	private readonly CancellationToken __ct;

	private Boolean __is_connected = false;

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
		this.__pipe = new Pipe();
		this.__socket = new();
		this.__gateway_resource = gatewayResource;

		this.Input = this.__pipe.Reader;
		this.Output = this.__pipe.Writer;
	}

	/// <summary>
	/// Represents the inbound side of the gateway connection.
	/// </summary>
	public PipeReader Input { get; private set; }

	/// <summary>
	/// Represents the outbound side of the gateway connection.
	/// </summary>
	public PipeWriter Output { get; private set; }

	/// <summary>
	/// Connects to the Discord websocket.
	/// </summary>
	/// <param name="ct">The cancellation token to use for this current connection.</param>
	/// <exception cref="StarnightGatewayConnectionRefusedException">
	/// Thrown if the session start limit for the day had been exceeded.
	/// </exception>
	public async ValueTask ConnectAsync(CancellationToken ct = default)
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
			new(connectionObject.Url),
			this.__ct
		);

		this.__is_connected = true;

		this.__logger.LogInformation
		(
			"Connected to the Discord websocket."
		);
	}
}

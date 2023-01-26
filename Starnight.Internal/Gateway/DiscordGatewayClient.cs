namespace Starnight.Internal.Gateway;

using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Starnight.Exceptions;
using Starnight.Internal.Entities.Users;
using Starnight.Internal.Gateway.Events;
using Starnight.Internal.Gateway.Events.Control;
using Starnight.Internal.Gateway.Events.Inbound;
using Starnight.Internal.Gateway.Listeners;
using Starnight.Internal.Gateway.Payloads.Outbound;
using Starnight.Internal.Gateway.Services;
using Starnight.Internal.Gateway.State;

/// <summary>
/// Represents the main gateway client.
/// </summary>
public class DiscordGatewayClient : IHostedService
{
	private readonly ILogger<DiscordGatewayClient> logger;
	private readonly DiscordGatewayClientOptions options;
	private readonly String token;
	private readonly TransportService transportService;
	private readonly IInboundGatewayService inboundGatewayService;
	private readonly IOutboundGatewayService outboundGatewayService;
	private readonly CancellationTokenSource gatewayTokenSource;
	private readonly ListenerService listenerService;
	private GatewayClientStateTracker gatewayClientStateTracker;

	private Int32 responselessHeartbeats = 0;
	private Int32 heartbeatInterval;
	private String sessionId = null!;

	/// <summary>
	/// Gets the last received sequence number of any event.
	/// </summary>
	public Int32 LastReceivedSequence => this.inboundGatewayService.LastReceivedSequence;

	/// <summary>
	/// Gets the current state of the gateway client.
	/// </summary>
	public DiscordGatewayClientState State => this.gatewayClientStateTracker.State;

	/// <summary>
	/// Gets the amount of consecutive heartbeats without response.
	/// </summary>
	/// <remarks>
	/// It is normal - and expected - for this number to alternate between 0 and 1.
	/// </remarks>
	public Int32 ResponselessHeartbeats
	{
		get => this.responselessHeartbeats;
		private set
		{
			this.responselessHeartbeats = value;

			// equality comparison because we don't want to fire this event twice for the same zombie.
			if(this.responselessHeartbeats == this.options.ZombieThreshold)
			{
				this.logger.LogWarning
				(
					"The current gateway connection is considered zombied. Missed heartbeats: {heartbeats}",
					value
				);

				_ = this.listenerService.Writer.TryWrite
				(
					new ZombiedEvent
					{
						LastReceivedSequence = this.LastReceivedSequence
					}
				);
			}
		}
	}

	/// <summary>
	/// Gets the time at which the last heartbeat was sent.
	/// </summary>
	public DateTimeOffset LastHeartbeatSent { get; private set; }

	/// <summary>
	/// Gets the time at which the last heartbeat was received.
	/// </summary>
	public DateTimeOffset LastHeartbeatReceived { get; private set; }

	/// <summary>
	/// Gets the approximate latency between the client and Discord's gateway, calculated using heartbeats.
	/// </summary>
	public TimeSpan Latency { get; private set; }

	public DiscordGatewayClient
	(
		ILogger<DiscordGatewayClient> logger,
		IOptions<DiscordGatewayClientOptions> options,
		IOptions<TokenContainer> container,
		TransportService transportService,
		IInboundGatewayService inboundService,
		IOutboundGatewayService outboundService,
		ListenerService listenerService
	)
	{
		this.logger = logger;
		this.options = options.Value;
		this.transportService = transportService;
		this.inboundGatewayService = inboundService;
		this.outboundGatewayService = outboundService;
		this.listenerService = listenerService;

		this.token = container.Value.Token;

		this.gatewayClientStateTracker = new()
		{
			State = DiscordGatewayClientState.Disconnected
		};

		this.gatewayTokenSource = new();

		this.LastHeartbeatSent = DateTimeOffset.MinValue;
		this.LastHeartbeatReceived = DateTimeOffset.MinValue;
		this.Latency = TimeSpan.Zero;
	}

	public async Task StartAsync
	(
		CancellationToken cancellationToken
	)
	{
		if(this.options.ShardInformation is not null && this.options.ShardInformation.Length != 2)
		{
			throw new StarnightInvalidConnectionException
			(
				$"The shard information data passed must comprise of two elements, found {this.options.ShardInformation.Length}."
			);
		}

		this.gatewayClientStateTracker.SetConnecting();

		await this.transportService.ConnectAsync
		(
			this.gatewayTokenSource.Token
		);

		this.gatewayClientStateTracker.SetIdentifying();

		await this.identifyAsync
		(
			cancellationToken
		);

		this.gatewayClientStateTracker.SetConnected();

		await this.inboundGatewayService.StartAsync
		(
			this.gatewayTokenSource.Token
		);

		_ = Task.Factory.StartNew
		(
			async () => await this.handleControlEventsAsync
			(
				cancellationToken
			),
			TaskCreationOptions.LongRunning
		);
	}

	public async Task StopAsync
	(
		CancellationToken cancellationToken
	)
	{
		this.gatewayClientStateTracker.SetDisconnected();

		await this.transportService.DisconnectAsync
		(
			false,
			WebSocketCloseStatus.NormalClosure
		);

		this.gatewayTokenSource.Cancel();
	}

	/// <summary>
	/// Reconnects to the discord gateway.
	/// </summary>
	/// <param name="ct">The cancellation token to use for this operation.</param>
	public async ValueTask ReconnectAsync(CancellationToken ct)
	{
		this.transportService.ResumeUrl = null;

		this.gatewayClientStateTracker.SetConnecting();

		await this.transportService.ConnectAsync
		(
			ct
		);

		this.gatewayClientStateTracker.SetIdentifying();

		await this.identifyAsync
		(
			ct
		);

		this.gatewayClientStateTracker.SetConnected();
	}

	/// <summary>
	/// Sends an outbound event to the discord gateway.
	/// </summary>
	/// <param name="event">The event to be sent off to the gateway</param>
	/// <exception cref="StarnightInvalidOutboundEventException">Thrown if the event used an opcode reserved for inbound events.</exception>
	public async ValueTask SendOutboundEventAsync
	(
		IDiscordGatewayEvent @event
	)
	{
		if
		(
			@event.Opcode is DiscordGatewayOpcode.Dispatch
				or DiscordGatewayOpcode.Reconnect
				or DiscordGatewayOpcode.InvalidSession
				or DiscordGatewayOpcode.Hello
				or DiscordGatewayOpcode.HeartbeatAck
		)
		{
			throw new StarnightInvalidOutboundEventException
			(
				"Attempted to send an outbound event with an opcode reserved for inbound events",
				@event
			);
		}

		await this.outboundGatewayService.SendEventAsync
		(
			@event
		);
	}

	private async ValueTask identifyAsync
	(
		CancellationToken ct
	)
	{
		ArgumentNullException.ThrowIfNull
		(
			this.token
		);

		IDiscordGatewayEvent? @event = (await this.transportService.ReadAsync
		(
			ct
		))
		.Event;

		if(@event is not DiscordHelloEvent helloEvent)
		{
			throw new StarnightGatewayConnectionRefusedException
			(
				"The first received event was not a hello event."
			);
		}

		this.logger.LogDebug
		(
			"Received hello event, starting heartbeating with an interval of {interval} and identifying.",
			TimeSpan.FromMilliseconds
			(
				helloEvent.Data.HeartbeatInterval
			)
		);

		this.heartbeatInterval = helloEvent.Data.HeartbeatInterval;
		_ = Task.Factory.StartNew
		(
			async () => await this.heartbeatAsync
			(
				this.gatewayTokenSource.Token
			),
			TaskCreationOptions.LongRunning
		);

		IdentifyPayload identify = new()
		{
			Token = this.token,
			ConnectionProperties = new()
			{
				Browser = StarnightInternalConstants.LibraryName,
				Device = StarnightInternalConstants.LibraryName,
				OS = RuntimeInformation.OSDescription
			},
			Compress = false,
			LargeGuildThreshold = this.options.LargeGuildThreshold,

			Shard = this.options.ShardInformation is not null
				? this.options.ShardInformation
				: Optional<IEnumerable<Int32>>.Empty,

			Presence = this.options.Presence is not null
				? this.options.Presence
				: Optional<DiscordPresence>.Empty,

			Intents = (Int32)this.options.Intents
		};

		await this.outboundGatewayService.IdentifyAsync
		(
			identify
		);

		this.logger.LogInformation
		(
			"Connected to the Discord gateway."
		);
	}

	private async ValueTask heartbeatAsync
	(
		CancellationToken ct
	)
	{
		Double jitter = Random.Shared.NextDouble();

		await Task.Delay
		(
			(Int32)(this.heartbeatInterval * jitter),
			ct
		);

		await this.outboundGatewayService.SendHeartbeatAsync
		(
			this.LastReceivedSequence
		);

		this.ResponselessHeartbeats++;
		this.LastHeartbeatSent = DateTimeOffset.UtcNow;

		this.logger.LogTrace
		(
			"Sent initial heartbeat."
		);

		while(!ct.IsCancellationRequested)
		{
			await Task.Delay
			(
				this.heartbeatInterval,
				ct
			);

			await this.outboundGatewayService.SendHeartbeatAsync
			(
				this.LastReceivedSequence
			);

			this.ResponselessHeartbeats++;
			this.LastHeartbeatSent = DateTimeOffset.UtcNow;

			this.logger.LogTrace
			(
				"Heartbeat sent."
			);
		}
	}

	private async ValueTask handleControlEventsAsync
	(
		CancellationToken ct
	)
	{
		while(!ct.IsCancellationRequested)
		{
			IDiscordGatewayEvent @event = await this.inboundGatewayService.ControlEvents.ReadAsync
			(
				ct
			);

			switch(@event)
			{
				case DiscordConnectedEvent connectedEvent:

					this.transportService.ResumeUrl = connectedEvent.Data.ResumeGatewayUrl;
					this.sessionId = connectedEvent.Data.SessionId;

					break;

				case DiscordInboundHeartbeatEvent:

					this.ResponselessHeartbeats = 0;
					this.LastHeartbeatReceived = DateTimeOffset.UtcNow;
					this.Latency = this.LastHeartbeatReceived - this.LastHeartbeatSent;

					this.logger.LogTrace
					(
						"Heartbeat received, with {delay}ms latency.",
						this.Latency.Milliseconds
					);

					break;

				case DiscordInvalidSessionEvent invalidSessionEvent:

					this.logger.LogWarning
					(
						"The current session is considered invalid by Discord, {resumable}",
						invalidSessionEvent.Data
							? "attempting to resume..."
							: "abandoning..."
					);

					if(invalidSessionEvent.Data)
					{
						this.gatewayClientStateTracker.SetDisconnectedResumable();

						_ = Task.Run
						(
							async () => await this.resume(),
							ct
						);
					}
					else
					{
						this.gatewayClientStateTracker.SetDisconnected();

						throw new StarnightInvalidConnectionException
						(
							"The current connection was considered invalid by Discord."
						);
					}

					break;

				case DiscordReconnectEvent reconnectEvent:

					this.gatewayClientStateTracker.SetDisconnected();

					_ = Task.Run
					(
						async () => await this.ReconnectAsync
						(
							ct
						),
						ct
					);

					break;
			}
		}
	}

	private async ValueTask resume()
	{
		ArgumentNullException.ThrowIfNull
		(
			this.token
		);

		this.gatewayClientStateTracker.SetResuming();

		await this.transportService.ConnectAsync();

		ResumePayload resume = new()
		{
			Sequence = this.LastReceivedSequence,
			Token = this.token,
			SessionId = this.sessionId
		};

		await this.outboundGatewayService.ResumeAsync
		(
			resume
		);

		this.gatewayClientStateTracker.SetConnected();
	}
}

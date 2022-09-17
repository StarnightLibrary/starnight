namespace Starnight.Internal.Gateway;

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Starnight.Internal.Entities.Users;
using Starnight.Internal.Exceptions;
using Starnight.Internal.Gateway.Events.Inbound;
using Starnight.Internal.Gateway.Payloads.Outbound;
using Starnight.Internal.Gateway.Services;

public delegate void ZombiedDelegate(DiscordGatewayClient client);

/// <summary>
/// Represents the main gateway client.
/// </summary>
public class DiscordGatewayClient : IHostedService
{
	private readonly ILogger<DiscordGatewayClient> __logger;
	private readonly DiscordGatewayClientOptions __options;
	private readonly TransportService __transport_service;
	private readonly IInboundGatewayService __inbound_gateway_service;
	private readonly IOutboundGatewayService __outbound_gateway_service;

	private Int32 __responseless_heartbeats = 0;
	private Int32 __heartbeat_interval;
	private String __session_id = null!;

	/// <summary>
	/// An event indicating that the current connection is considered zombied.
	/// </summary>
	/// <remarks>
	/// This event is fired after a certain amount of heartbeats were missed. This threshold is configurable in
	/// <see cref="DiscordGatewayClientOptions"/>.
	/// </remarks>
	public event ZombiedDelegate ZombiedEvent = null!;

	/// <summary>
	/// Gets the last received sequence number of any event.
	/// </summary>
	public Int32 LastReceivedSequence => this.__inbound_gateway_service.LastReceivedSequence;

	/// <summary>
	/// Gets the amount of consecutive heartbeats without response.
	/// </summary>
	/// <remarks>
	/// It is normal - and expected - for this number to alternate between 0 and 1.
	/// </remarks>
	public Int32 ResponselessHeartbeats
	{
		get => this.__responseless_heartbeats;
		private set
		{
			this.__responseless_heartbeats = value;

			// equality comparison because we don't want to fire this event twice for the same zombie.
			if(this.__responseless_heartbeats == this.__options.ZombieThreshold)
			{
				this.__logger.LogWarning
				(
					"The current gateway connection is considered zombied. Missed heartbeats: {heartbeats}",
					value
				);

				this.ZombiedEvent(this);
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
		TransportService transportService,
		IInboundGatewayService inboundService,
		IOutboundGatewayService outboundService
	)
	{
		this.__logger = logger;
		this.__options = options.Value;
		this.__transport_service = transportService;
		this.__inbound_gateway_service = inboundService;
		this.__outbound_gateway_service = outboundService;

		this.LastHeartbeatSent = DateTimeOffset.MinValue;
		this.LastHeartbeatReceived = DateTimeOffset.MinValue;
		this.Latency = TimeSpan.Zero;
	}

	public async Task StartAsync(CancellationToken cancellationToken)
	{

		if(this.__options.ShardInformation is not null && this.__options.ShardInformation.Length != 2)
		{
			throw new StarnightInvalidConnectionException
			(
				$"The shard information data passed must comprise of two elements, found {this.__options.ShardInformation.Length}."
			);
		}

		await this.__inbound_gateway_service.StartAsync(cancellationToken);
		await this.__transport_service.ConnectAsync(cancellationToken);

		await this.identifyAsync(cancellationToken);

		_ = Task.Factory.StartNew(async () => await this.handleControlEventsAsync(cancellationToken));
	}

	public Task StopAsync(CancellationToken cancellationToken)
		=> Task.CompletedTask;

	/// <summary>
	/// Reconnects to the discord gateway.
	/// </summary>
	/// <param name="ct">The cancellation token to use for this operation.</param>
	public async ValueTask ReconnectAsync(CancellationToken ct)
	{
		this.__transport_service.ResumeUrl = null;

		await this.__transport_service.ConnectAsync(ct);

		await this.identifyAsync(ct);
	}

	/// <summary>
	/// Sends an outbound event to the discord gateway.
	/// </summary>
	/// <param name="event">The event to be sent off to the gateway</param>
	/// <exception cref="StarnightInvalidOutboundEventException">Thrown if the event used an opcode reserved for inbound events.</exception>
	public async ValueTask SendOutboundEventAsync(IDiscordGatewayEvent @event)
	{
		if(@event.Opcode is DiscordGatewayOpcode.Dispatch
			or DiscordGatewayOpcode.Reconnect
			or DiscordGatewayOpcode.InvalidSession
			or DiscordGatewayOpcode.Hello
			or DiscordGatewayOpcode.HeartbeatAck)
		{
			throw new StarnightInvalidOutboundEventException
			(
				"Attempted to send an outbound event with an opcode reserved for inbound events",
				@event
			);
		}

		await this.__outbound_gateway_service.SendEventAsync(@event);
	}

	private async ValueTask identifyAsync(CancellationToken ct)
	{
		ArgumentNullException.ThrowIfNull(this.__options.Token);

		IDiscordGatewayEvent @event = await this.__inbound_gateway_service.ControlEvents.ReadAsync(ct);

		if(@event is not DiscordHelloEvent helloEvent)
		{
			throw new StarnightGatewayConnectionRefusedException
			(
				"The first received event was not a hello event."
			);
		}

		this.__logger.LogDebug
		(
			"Received hello event, starting heartbeating with an interval of {interval} and identifying.",
			TimeSpan.FromMilliseconds(helloEvent.Data.HeartbeatInterval)
		);

		this.__heartbeat_interval = helloEvent.Data.HeartbeatInterval;
		_ = Task.Factory.StartNew(async () => await this.heartbeatAsync(ct));

		IdentifyPayload identify = new()
		{
			Token = this.__options.Token,
			ConnectionProperties = new()
			{
				Browser = StarnightInternalConstants.LibraryName,
				Device = StarnightInternalConstants.LibraryName,
				OS = RuntimeInformation.OSDescription
			},
			Compress = false,
			LargeGuildThreshold = this.__options.LargeGuildThreshold,

			Shard = this.__options.ShardInformation is not null
				? this.__options.ShardInformation
				: Optional<IEnumerable<Int32>>.Empty,

			Presence = this.__options.Presence is not null
				? this.__options.Presence
				: Optional<DiscordPresence>.Empty,

			Intents = (Int32)this.__options.Intents
		};

		await this.__outbound_gateway_service.IdentifyAsync(identify);

		this.__logger.LogDebug
		(
			"Identified, awaiting Connected event..."
		);

		@event = await this.__inbound_gateway_service.ControlEvents.ReadAsync(ct);

		if(@event is not DiscordConnectedEvent connectedEvent)
		{
			throw new StarnightGatewayConnectionRefusedException
			(
				$"Expected connected event, got {@event}"
			);
		}

		this.__logger.LogInformation
		(
			"Connected to the Discord gateway."
		);

		this.__transport_service.ResumeUrl = connectedEvent.Data.ResumeGatewayUrl;
		this.__session_id = connectedEvent.Data.SessionId;
	}

	private async ValueTask heartbeatAsync(CancellationToken ct)
	{
		Double jitter = Random.Shared.NextDouble();

		await Task.Delay((Int32)(this.__heartbeat_interval * jitter), ct);

		await this.__outbound_gateway_service.SendHeartbeatAsync(this.LastReceivedSequence);

		this.ResponselessHeartbeats++;
		this.LastHeartbeatSent = DateTimeOffset.UtcNow;

		this.__logger.LogTrace
		(
			"Sent initial heartbeat."
		);

		while(!ct.IsCancellationRequested)
		{
			await Task.Delay(this.__heartbeat_interval, ct);

			await this.__outbound_gateway_service.SendHeartbeatAsync(this.LastReceivedSequence);

			this.ResponselessHeartbeats++;
			this.LastHeartbeatSent = DateTimeOffset.UtcNow;

			this.__logger.LogTrace
			(
				"Heartbeat sent."
			);
		}
	}

	private async ValueTask handleControlEventsAsync(CancellationToken ct)
	{
		while(!ct.IsCancellationRequested)
		{
			IDiscordGatewayEvent @event = await this.__inbound_gateway_service.ControlEvents.ReadAsync(ct);

			switch(@event)
			{
				case DiscordInboundHeartbeatEvent:

					this.ResponselessHeartbeats = 0;
					this.LastHeartbeatReceived = DateTimeOffset.UtcNow;
					this.Latency = this.LastHeartbeatReceived - this.LastHeartbeatSent;

					this.__logger.LogTrace
					(
						"Heartbeat received, with {delay} latency.",
						this.Latency
					);

					break;

				case DiscordInvalidSessionEvent invalidSessionEvent:

					if(invalidSessionEvent.Data)
					{
						_ = Task.Run
						(
							async () => await this.resume(),
							ct
						);
					}

					this.__logger.LogWarning
					(
						"The current session is considered invalid by Discord, {resumable}",
						invalidSessionEvent.Data
							? "attempting to resume..."
							: "abandoning..."
					);

					if(!invalidSessionEvent.Data)
					{
						throw new StarnightInvalidConnectionException
						(
							"The current connection was considered invalid by Discord."
						);
					}

					break;

				case DiscordReconnectEvent reconnectEvent:

					_ = Task.Run
					(
						async () => await this.ReconnectAsync(ct),
						ct
					);

					break;
			}
		}
	}

	private async ValueTask resume()
	{
		ArgumentNullException.ThrowIfNull(this.__options.Token);
		await this.__transport_service.ConnectAsync();

		ResumePayload resume = new()
		{
			Sequence = this.LastReceivedSequence,
			Token = this.__options.Token,
			SessionId = this.__session_id
		};

		await this.__outbound_gateway_service.ResumeAsync(resume);
	}
}

namespace Starnight.Internal.Gateway.Services;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Starnight.Internal.Gateway.Events.Inbound;
using Starnight.Internal.Gateway.Responders;

/// <summary>
/// Receives gateway events from discord and handles them.
/// </summary>
public class InboundGatewayService : IInboundGatewayService
{
	private readonly ILogger<InboundGatewayService> __logger;
	private readonly TransportService __transport_service;
	private readonly Channel<IDiscordGatewayEvent> __control_channel;
	private readonly Channel<IDiscordGatewayEvent> __responder_channel;
	private ResponderService __responder_service;

	private readonly Dictionary<Type, Boolean> __control_events;

	private readonly ILogger<ResponderService> __responder_logger;
	private readonly IServiceProvider __service_provider;
	private readonly ResponderCollection __responders;

	public Int32 LastReceivedSequence { get; private set; }

	public ChannelReader<IDiscordGatewayEvent> ControlEvents { get; }

	public InboundGatewayService
	(
		ILogger<InboundGatewayService> logger,
		ILogger<ResponderService> responderLogger,
		TransportService transportService,

		IServiceProvider services,
		ResponderCollection responders
	)
	{
		this.__logger = logger;
		this.__transport_service = transportService;
		this.__control_events = new();

		this.__responder_logger = responderLogger;
		this.__service_provider = services;
		this.__responders = responders;

		this.__control_channel = Channel.CreateUnbounded<IDiscordGatewayEvent>();
		this.__responder_channel = Channel.CreateUnbounded<IDiscordGatewayEvent>();

		this.ControlEvents = this.__control_channel.Reader;
		this.LastReceivedSequence = 0;

		// we'll assign this in StartAsync
		this.__responder_service = null!;
	}

	/// <inheritdoc/>
	public ValueTask StartAsync(CancellationToken ct)
	{
		this.__responder_service = new
		(
			this.__responder_logger,
			this.__service_provider,
			this.__responders,
			this.__responder_channel.Reader,
			ct
		);

		this.__logger.LogDebug
		(
			"Initialized responder handling."
		);

		_ = Task.Factory.StartNew(async () => await this.handleEventsAsync(ct));

		return ValueTask.CompletedTask;
	}

	private async ValueTask handleEventsAsync(CancellationToken ct)
	{
		while(!ct.IsCancellationRequested)
		{
			IDiscordGatewayEvent @event = await this.__transport_service.Inbound.ReadAsync(ct);

			_ = Task.Run
			(
				async () =>
				{
					await this.__responder_channel.Writer.WriteAsync(@event);


					if(!this.__control_events.TryGetValue(@event.GetType(), out Boolean isControlEvent))
					{
						isControlEvent = @event.Opcode is 
							DiscordGatewayOpcode.Hello or
							DiscordGatewayOpcode.Reconnect or
							DiscordGatewayOpcode.InvalidSession or
							DiscordGatewayOpcode.HeartbeatAck
							|| @event.GetType() == typeof(DiscordConnectedEvent);

						this.__control_events.Add(@event.GetType(), isControlEvent);
					}

					if(isControlEvent)
					{
						await this.__control_channel.Writer.WriteAsync(@event);
					}
				},
				ct
			);
		}

		GC.KeepAlive(this.__responder_service);
	}
}

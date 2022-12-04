namespace Starnight.Internal.Gateway.Services;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Starnight.Internal.Gateway.Events.Inbound;
using Starnight.Internal.Gateway.Responders;

/// <summary>
/// Receives gateway events from discord and handles them.
/// </summary>
public class InboundGatewayService : IInboundGatewayService
{
	private readonly ILogger<InboundGatewayService> logger;
	private readonly TransportService transportService;
	private readonly Channel<IDiscordGatewayEvent> controlChannel;
	private readonly Channel<IDiscordGatewayEvent> responderChannel;
	private ResponderService responderService;

	private readonly Dictionary<Type, Boolean> controlEvents;

	private readonly ILogger<ResponderService> responderLogger;
	private readonly IServiceProvider serviceProvider;
	private readonly ResponderCollection responders;

	public Int32 LastReceivedSequence { get; private set; }

	public ChannelReader<IDiscordGatewayEvent> ControlEvents { get; }

	public InboundGatewayService
	(
		ILogger<InboundGatewayService> logger,
		ILogger<ResponderService> responderLogger,
		TransportService transportService,

		IServiceProvider services,
		IOptions<ResponderCollection> responders
	)
	{
		this.logger = logger;
		this.transportService = transportService;
		this.controlEvents = new();

		this.responderLogger = responderLogger;
		this.serviceProvider = services;
		this.responders = responders.Value;

		this.controlChannel = Channel.CreateUnbounded<IDiscordGatewayEvent>();
		this.responderChannel = Channel.CreateUnbounded<IDiscordGatewayEvent>();

		this.ControlEvents = this.controlChannel.Reader;
		this.LastReceivedSequence = 0;

		// we'll assign this in StartAsync
		this.responderService = null!;
	}

	/// <inheritdoc/>
	public ValueTask StartAsync(CancellationToken ct)
	{
		this.responderService = new
		(
			this.responderLogger,
			this.serviceProvider,
			this.responders,
			this.responderChannel.Reader,
			ct
		);

		this.logger.LogDebug
		(
			"Initialized responder handling."
		);

		_ = Task.Factory.StartNew(async () => await this.HandleEventsAsync(ct));

		return ValueTask.CompletedTask;
	}

	internal async ValueTask HandleEventsAsync(CancellationToken ct)
	{
		while(!ct.IsCancellationRequested)
		{
			IDiscordGatewayEvent? @event = null!;

			try
			{
				@event = await this.transportService.ReadAsync(ct);
			}
			catch(Exception ex)
			{
				this.logger.LogError
				(
					ex,
					"Failed to deserialize inbound gateway event."
				);

				continue;
			}

			if(@event is null)
			{
				continue;
			}

			_ = Task.Run
			(
				async () =>
				{
					await this.responderChannel.Writer.WriteAsync(@event);


					if(!this.controlEvents.TryGetValue(@event.GetType(), out Boolean isControlEvent))
					{
						isControlEvent = @event.Opcode is
							DiscordGatewayOpcode.Hello or
							DiscordGatewayOpcode.Reconnect or
							DiscordGatewayOpcode.InvalidSession or
							DiscordGatewayOpcode.HeartbeatAck
							|| @event.GetType() == typeof(DiscordConnectedEvent);

						this.controlEvents.Add(@event.GetType(), isControlEvent);
					}

					if(isControlEvent)
					{
						await this.controlChannel.Writer.WriteAsync(@event);
					}
				},
				ct
			);
		}

		GC.KeepAlive(this.responderService);
	}
}

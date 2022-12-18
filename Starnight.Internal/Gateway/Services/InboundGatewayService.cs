namespace Starnight.Internal.Gateway.Services;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Starnight.Internal.Gateway.Events.Inbound;
using Starnight.Internal.Gateway.Listeners;

/// <summary>
/// Receives gateway events from discord and handles them.
/// </summary>
public class InboundGatewayService : IInboundGatewayService
{
	private readonly ILogger<InboundGatewayService> logger;
	private readonly TransportService transportService;
	private readonly Channel<IDiscordGatewayEvent> controlChannel;
	private readonly Channel<IDiscordGatewayEvent> listenerChannel;
	private ListenerService listenerService;

	private readonly Dictionary<Type, Boolean> controlEvents;

	private readonly ILogger<ListenerService> listenerLogger;
	private readonly IServiceProvider serviceProvider;
	private readonly ListenerCollection listeners;

	public Int32 LastReceivedSequence { get; private set; }

	public ChannelReader<IDiscordGatewayEvent> ControlEvents { get; }

	public InboundGatewayService
	(
		ILogger<InboundGatewayService> logger,
		ILogger<ListenerService> listenerLogger,
		TransportService transportService,

		IServiceProvider services,
		IOptions<ListenerCollection> listeners
	)
	{
		this.logger = logger;
		this.transportService = transportService;
		this.controlEvents = new();

		this.listenerLogger = listenerLogger;
		this.serviceProvider = services;
		this.listeners = listeners.Value;

		this.controlChannel = Channel.CreateUnbounded<IDiscordGatewayEvent>();
		this.listenerChannel = Channel.CreateUnbounded<IDiscordGatewayEvent>();

		this.ControlEvents = this.controlChannel.Reader;
		this.LastReceivedSequence = 0;

		// we'll assign this in StartAsync
		this.listenerService = null!;
	}

	/// <inheritdoc/>
	public ValueTask StartAsync(CancellationToken ct)
	{
		this.listenerService = new
		(
			this.listenerLogger,
			this.serviceProvider,
			this.listeners,
			this.listenerChannel.Reader,
			ct
		);

		this.logger.LogDebug
		(
			"Initialized listener handling."
		);

		_ = Task.Factory.StartNew
		(
			async () => await this.HandleEventsAsync(ct),
			TaskCreationOptions.LongRunning
		);

		return ValueTask.CompletedTask;
	}

	internal async ValueTask HandleEventsAsync(CancellationToken ct)
	{
		while(!ct.IsCancellationRequested)
		{
			InboundGatewayFrame @event;

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

			if(@event is { IsDisconnected: true } or { IsDisposed: true })
			{
				return;
			}

			if(@event is { Event: null })
			{
				continue;
			}

			_ = Task.Run
			(
				async () =>
				{
					await this.listenerChannel.Writer.WriteAsync(@event.Event!);


					if(!this.controlEvents.TryGetValue(@event.GetType(), out Boolean isControlEvent))
					{
						isControlEvent = @event.Event!.Opcode is
							DiscordGatewayOpcode.Hello or
							DiscordGatewayOpcode.Reconnect or
							DiscordGatewayOpcode.InvalidSession or
							DiscordGatewayOpcode.HeartbeatAck
							|| @event.GetType() == typeof(DiscordConnectedEvent);

						this.controlEvents.Add(@event.GetType(), isControlEvent);
					}

					if(isControlEvent)
					{
						await this.controlChannel.Writer.WriteAsync(@event.Event!);
					}
				},
				ct
			);
		}

		GC.KeepAlive(this.listenerService);
	}
}

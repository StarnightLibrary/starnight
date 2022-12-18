namespace Starnight.Internal.Gateway.Listeners;

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

#pragma warning disable IDE0001
using DispatchDelegate = System.Func
<
	Starnight.Internal.Gateway.IDiscordGatewayEvent,
	System.Collections.Generic.IEnumerable
	<
		System.Collections.Generic.IEnumerable
		<
			System.Type
		>
	>,
	Microsoft.Extensions.DependencyInjection.IServiceScope,
	System.Threading.Tasks.ValueTask
>;
#pragma warning restore IDE0001

/// <summary>
/// Contains all handling logic for dispatching gateway events to listeners.
/// </summary>
public class ListenerService
{
	private readonly ILogger<ListenerService> logger;
	private readonly IServiceProvider serviceProvider;
	private readonly ListenerCollection listenerCollection;

	private readonly ChannelReader<IDiscordGatewayEvent> eventChannel;

	private readonly Dictionary<Type, DispatchDelegate> cachedDelegates;

	public ListenerService
	(
		ILogger<ListenerService> logger,
		IServiceProvider serviceProvider,
		ListenerCollection listeners,
		ChannelReader<IDiscordGatewayEvent> eventChannel,
		CancellationToken ct
	)
	{
		this.logger = logger;
		this.serviceProvider = serviceProvider;
		this.listenerCollection = listeners;
		this.eventChannel = eventChannel;

		this.cachedDelegates = new();

		_ = Task.Factory.StartNew
		(
			async () => await this.dispatchAsync(ct),
			TaskCreationOptions.LongRunning
		);
	}

	private async ValueTask dispatchAsync(CancellationToken ct)
	{
		while(!ct.IsCancellationRequested)
		{
			try
			{
				IDiscordGatewayEvent @event = await this.eventChannel.ReadAsync(ct);

				_ = Task.Run
				(
					async () => await this.dispatchEventAsync(@event),
					ct
				);
			}
			catch(OperationCanceledException) { }
		}
	}

	private async ValueTask dispatchEventAsync(IDiscordGatewayEvent @event)
	{
		Type eventType = @event.GetType();

		IServiceScope scope = this.serviceProvider.CreateScope();

		IEnumerable<Type>[] listeners = new IEnumerable<Type>[]
		{
			this.listenerCollection.GetListeners(eventType, ListenerPhase.PreEvent),
			this.listenerCollection.GetListeners(eventType, ListenerPhase.Early),
			this.listenerCollection.GetListeners(eventType, ListenerPhase.Normal),
			this.listenerCollection.GetListeners(eventType, ListenerPhase.Late),
			this.listenerCollection.GetListeners(eventType, ListenerPhase.PostEvent)
		};

		DispatchDelegate dispatchDelegate;

		if(!this.cachedDelegates.TryGetValue(eventType, out dispatchDelegate!))
		{
			Type delegateType = typeof(Func<,,,>).MakeGenericType
			(
				eventType,
				typeof(IEnumerable<IEnumerable<Type>>),
				typeof(IServiceScope),
				typeof(ValueTask)
			);

			dispatchDelegate = Unsafe.As<DispatchDelegate>
			(
				typeof(ListenerService)
					.GetMethod
					(
						nameof(invokeListenersAsync),
						BindingFlags.NonPublic | BindingFlags.Instance
					)!
					.MakeGenericMethod
					(
						@event.GetType()
					)
					.CreateDelegate
					(
						delegateType,
						this
					)
			);

			this.cachedDelegates.Add(@event.GetType(), dispatchDelegate);
		}

		await dispatchDelegate(@event, listeners, scope);
	}

	private async ValueTask invokeListenersAsync<TEvent>
	(
		TEvent @event,
		IEnumerable<IEnumerable<Type>> listeners,
		IServiceScope scope
	)
		where TEvent : IDiscordGatewayEvent
	{
		foreach(IEnumerable<Type> phase in listeners)
		{
			await Parallel.ForEachAsync
			(
				phase,
				async (xm, _) =>
				{
					try
					{
						IListener<TEvent> listener = Unsafe.As<IListener<TEvent>>(scope.ServiceProvider.GetRequiredService(xm));

						await listener.ListenAsync(@event);
					}
					catch(Exception e)
					{
						this.logger.LogError(e, "An error occured during event dispatch.");
					}
				}
			);
		}
	}
}

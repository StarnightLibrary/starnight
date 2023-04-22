namespace Starnight.Internal.Gateway.Listeners;

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

using CommunityToolkit.HighPerformance.Helpers;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Starnight.Internal.Gateway.Events;

#pragma warning disable IDE0001
using DispatchDelegate = System.Func
<
	Starnight.Internal.Gateway.Events.IGatewayEvent,
	System.Type[][],
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

	private readonly Channel<IGatewayEvent> eventChannel;

	private readonly Dictionary<Type, DispatchDelegate> cachedDelegates;

	public ChannelWriter<IGatewayEvent> Writer => this.eventChannel.Writer;

	public ListenerService
	(
		ILogger<ListenerService> logger,
		IServiceProvider serviceProvider,
		IOptions<ListenerCollection> listeners
	)
	{
		this.logger = logger;
		this.serviceProvider = serviceProvider;
		this.listenerCollection = listeners.Value;
		this.eventChannel = Channel.CreateUnbounded<IGatewayEvent>();

		this.cachedDelegates = new();
	}

	public ValueTask StartAsync
	(
		CancellationToken ct
	)
	{
		_ = Task.Factory.StartNew
		(
			async () => await this.dispatchAsync(ct),
			TaskCreationOptions.LongRunning
		);

		return ValueTask.CompletedTask;
	}

	private async ValueTask dispatchAsync(CancellationToken ct)
	{
		while(!ct.IsCancellationRequested)
		{
			try
			{
				IGatewayEvent @event = await this.eventChannel.Reader.ReadAsync(ct);

				_ = Task.Run
				(
					async () => await this.dispatchEventAsync(@event),
					ct
				);
			}
			catch(OperationCanceledException) { }
		}
	}

	private async ValueTask dispatchEventAsync(IGatewayEvent @event)
	{
		Type eventType = @event.GetType();

		IServiceScope scope = this.serviceProvider.CreateScope();

		Type[][] listeners = new Type[][]
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
				typeof(Type[][]),
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

	private ValueTask invokeListenersAsync<TEvent>
	(
		TEvent @event,
		Type[][] listeners,
		IServiceScope scope
	)
		where TEvent : class, IGatewayEvent
	{
		foreach(Type[] phase in listeners)
		{
			ParallelHelper.ForEach<Type, ListenerDispatcher<TEvent>>
			(
				phase,
				new ListenerDispatcher<TEvent>
				(
					scope,
					this.logger,
					@event
				)
			);
		}

		return ValueTask.CompletedTask;
	}
}

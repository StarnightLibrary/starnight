namespace Starnight.Internal.Gateway.Responders;

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
/// Contains all handling logic for dispatching gateway events to responders.
/// </summary>
public class ResponderService
{
	private readonly ILogger<ResponderService> logger;
	private readonly IServiceProvider serviceProvider;
	private readonly ResponderCollection responderCollection;

	private readonly ChannelReader<IDiscordGatewayEvent> eventChannel;

	private readonly Dictionary<Type, DispatchDelegate> cachedDelegates;

	public ResponderService
	(
		ILogger<ResponderService> logger,
		IServiceProvider serviceProvider,
		ResponderCollection responders,
		ChannelReader<IDiscordGatewayEvent> eventChannel,
		CancellationToken ct
	)
	{
		this.logger = logger;
		this.serviceProvider = serviceProvider;
		this.responderCollection = responders;
		this.eventChannel = eventChannel;

		this.cachedDelegates = new();

		_ = Task.Factory.StartNew(async () => await this.dispatchAsync(ct));
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

		IEnumerable<Type>[] responders = new IEnumerable<Type>[]
		{
			this.responderCollection.GetResponders(eventType, ResponderPhase.PreEvent),
			this.responderCollection.GetResponders(eventType, ResponderPhase.Early),
			this.responderCollection.GetResponders(eventType, ResponderPhase.Normal),
			this.responderCollection.GetResponders(eventType, ResponderPhase.Late),
			this.responderCollection.GetResponders(eventType, ResponderPhase.PostEvent)
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
				typeof(ResponderService)
					.GetMethod
					(
						nameof(invokeRespondersAsync),
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

		await dispatchDelegate(@event, responders, scope);
	}

	private async ValueTask invokeRespondersAsync<TEvent>
	(
		TEvent @event,
		IEnumerable<IEnumerable<Type>> responders,
		IServiceScope scope
	)
		where TEvent : IDiscordGatewayEvent
	{
		foreach(IEnumerable<Type> phase in responders)
		{
			await Parallel.ForEachAsync
			(
				phase,
				async (xm, _) =>
				{
					try
					{
						IResponder<TEvent> responder = Unsafe.As<IResponder<TEvent>>(scope.ServiceProvider.GetRequiredService(xm));

						await responder.RespondAsync(@event);
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

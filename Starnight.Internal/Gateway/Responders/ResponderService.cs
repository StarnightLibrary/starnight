namespace Starnight.Internal.Gateway.Responders;

using System;
using System.Collections.Generic;
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
	private readonly ILogger<ResponderService> __logger;
	private readonly IServiceProvider __service_provider;
	private readonly ResponderCollection __responder_collection;

	private readonly ChannelReader<IDiscordGatewayEvent> __event_channel;

	private readonly Dictionary<Type, DispatchDelegate> __cached_delegates;

	public ResponderService
	(
		ILogger<ResponderService> logger,
		IServiceProvider serviceProvider,
		ResponderCollection responders,
		ChannelReader<IDiscordGatewayEvent> eventChannel,
		CancellationToken ct
	)
	{
		this.__logger = logger;
		this.__service_provider = serviceProvider;
		this.__responder_collection = responders;
		this.__event_channel = eventChannel;

		this.__cached_delegates = new();

		_ = Task.Factory.StartNew(async () => await this.dispatchAsync(ct));
	}

	private async ValueTask dispatchAsync(CancellationToken ct)
	{
		while(!ct.IsCancellationRequested)
		{
			try
			{
				IDiscordGatewayEvent @event = await this.__event_channel.ReadAsync(ct);

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

		IServiceScope scope = this.__service_provider.CreateScope();

		IEnumerable<Type>[] responders = new IEnumerable<Type>[]
		{
			this.__responder_collection.GetResponders(eventType, ResponderPhase.PreEvent),
			this.__responder_collection.GetResponders(eventType, ResponderPhase.Early),
			this.__responder_collection.GetResponders(eventType, ResponderPhase.Normal),
			this.__responder_collection.GetResponders(eventType, ResponderPhase.Late),
			this.__responder_collection.GetResponders(eventType, ResponderPhase.PostEvent)
		};

		DispatchDelegate dispatchDelegate;

		if(!this.__cached_delegates.TryGetValue(@event.GetType(), out dispatchDelegate!))
		{
			Type delegateType = typeof(Func<,,,>).MakeGenericType
			(
				typeof(IDiscordGatewayEvent),
				typeof(IEnumerable<IEnumerable<Type>>),
				typeof(IServiceScope),
				typeof(ValueTask)
			);

			dispatchDelegate = Unsafe.As<DispatchDelegate>
			(
				typeof(ResponderService)
					.GetMethod(nameof(dispatchEventAsync))!
					.MakeGenericMethod(@event.GetType())
					.CreateDelegate(delegateType, this)
			);

			this.__cached_delegates.Add(@event.GetType(), dispatchDelegate);
		}

		await dispatchDelegate(@event, responders, scope);
	}

	private async ValueTask dispatchEventAsync<TEvent>
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
						this.__logger.LogError(e, "An error occured during event dispatch.");
					}
				}
			);
		}
	}
}

namespace Starnight.Internal.Gateway.Listeners;

using System;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

using CommunityToolkit.HighPerformance.Helpers;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Starnight.Internal.Gateway.Events;

internal readonly struct ListenerDispatcher<TEvent> : IInAction<Type>
	where TEvent : class, IGatewayEvent
{
	private readonly IServiceScope scope;
	private readonly ILogger<ListenerService> logger;
	private readonly TEvent @event;

	public ListenerDispatcher
	(
		IServiceScope scope,
		ILogger<ListenerService> logger,
		TEvent @event
	)
	{
		this.scope = scope;
		this.logger = logger;
		this.@event = @event;
	}

	public void Invoke(in Type x)
	{
		Type type = x;
		IServiceScope scope = this.scope;
		ILogger<ListenerService> logger = this.logger;
		TEvent @event = this.@event;

		_ = Task.Run
		(
			async () =>
			{
				try
				{
					IListener<TEvent> listener = Unsafe.As<IListener<TEvent>>
					(
						scope.ServiceProvider.GetRequiredService(type)
					);

					await listener.ListenAsync(@event);
				}
				catch(Exception e)
				{
					logger.LogError(e, "An error occured during event dispatch.");
				}
			}
		);
	}
}

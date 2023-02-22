namespace Starnight.Internal.Gateway.Listeners;

using System;

using Microsoft.Extensions.DependencyInjection;

using Starnight.Internal.Exceptions;

/// <summary>
/// Contains an extension to the service collection, enabling adding listeners.
/// </summary>
public static class ServiceCollectionExtensions
{
	/// <summary>
	/// Registers a new listener with Starnight.
	/// </summary>
	/// <param name="services">The service collection to add this listener to.</param>
	/// <param name="listener">The listener type to add.</param>
	/// <param name="phase">The phase into which this listener will be implemented.</param>
	/// <returns>The service collection for chaining.</returns>
	/// <exception cref="StarnightInvalidListenerException">Thrown if the listener didn't implement IListener.</exception>
	public static IServiceCollection AddListener
	(
		this IServiceCollection services,
		Type listener,
		ListenerPhase phase = ListenerPhase.Normal
	)
	{
		if(listener.GetInterface(nameof(IListener)) is null)
		{
			throw new StarnightInvalidListenerException
			(
				$"The passed listener did not implement {nameof(IListener)}.",
				listener
			);
		}

		_ = services.AddScoped(listener);

		_ = services.Configure<ListenerCollection>
		(
			xm => xm.RegisterListener
			(
				listener,
				phase
			)
		);

		return services;
	}
}

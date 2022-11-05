namespace Starnight.Internal.Gateway.Responders;

using System;

using Microsoft.Extensions.DependencyInjection;

using Starnight.Exceptions;

/// <summary>
/// Contains an extension to the service collection, enabling adding responders.
/// </summary>
public static class ServiceCollectionExtensions
{
	/// <summary>
	/// Registers a new responder with Starnight.
	/// </summary>
	/// <param name="services">The service collection to add this responder to.</param>
	/// <param name="responder">The responder type to add.</param>
	/// <param name="phase">The phase into which this responder will be implemented.</param>
	/// <returns>The service collection for chaining.</returns>
	/// <exception cref="StarnightInvalidResponderException">Thrown if the responder didn't implement IResponder.</exception>
	public static IServiceCollection AddResponder
	(
		this IServiceCollection services,
		Type responder,
		ResponderPhase phase = ResponderPhase.Normal
	)
	{
		if(responder.GetInterface(nameof(IResponder)) is null)
		{
			throw new StarnightInvalidResponderException
			(
				$"The passed responder did not implement {nameof(IResponder)}.",
				responder
			);
		}

		_ = services.AddScoped(responder);

		services
			.BuildServiceProvider()
			.GetRequiredService<ResponderCollection>()
			.RegisterResponder
			(
				responder,
				phase
			);

		return services;
	}
}

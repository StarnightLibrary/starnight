namespace Starnight.Internal.Utils;

using System;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Contains extension methods to <see cref="IServiceCollection"/>
/// </summary>
public static class ServiceCollectionExtensions
{
	/// <summary>
	/// Decorates a given <typeparamref name="TInterface"/> with a decorator of type <typeparamref name="TDecorator"/>
	/// </summary>
	/// <typeparam name="TInterface">The interface type to be decorated. The newly registered can be decorated again if needed.</typeparam>
	/// <typeparam name="TDecorator">The decorator type.</typeparam>
	/// <returns>The previous IServiceCollection for chaining.</returns>
	/// <exception cref="InvalidOperationException">Thrown if this method is called before a service of type <typeparamref name="TInterface"/> was registered</exception>
	public static IServiceCollection Decorate<TInterface, TDecorator>
	(
		this IServiceCollection services
	)
		where TInterface : class
		where TDecorator : class, TInterface
	{
		ServiceDescriptor? previousRegistration = services.LastOrDefault
		(
			xm => xm.ServiceType == typeof(TInterface)
		);

		if(previousRegistration is null)
		{
			throw new InvalidOperationException
			(
				$"Tried to register a decorator for {typeof(TInterface).Name}, but there was no underlying service to decorate."
			);
		}

		Func<IServiceProvider, Object>? previousFactory = previousRegistration.ImplementationFactory;

		if(previousFactory is null && previousRegistration.ImplementationInstance is not null)
		{
			previousFactory = _ => previousRegistration.ImplementationInstance;
		}
		else if(previousFactory is null && previousRegistration.ImplementationType is not null)
		{
			previousFactory = provider => ActivatorUtilities.CreateInstance
			(
				provider,
				previousRegistration.ImplementationType
			);
		}

		services.Add
		(
			new ServiceDescriptor
			(
				typeof(TInterface),
				CreateDecorator,
				previousRegistration.Lifetime
			)
		);

		return services;

		TDecorator CreateDecorator
		(
			IServiceProvider provider
		)
		{
			TInterface previousInstance = (TInterface)previousFactory!
			(
				provider

				);
			TDecorator decorator = (TDecorator)ActivatorUtilities.CreateFactory
			(
				typeof(TDecorator),
				new[]
				{
					typeof(TInterface)
				}
			)
			.Invoke
			(
				provider,
				new[]
				{
					previousInstance
				}
			);

			return decorator;
		}
	}
}

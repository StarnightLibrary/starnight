namespace Starnight.Internal.Extensions;

using System;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

using Starnight.Caching.Providers;
using Starnight.Internal.Gateway;
using Starnight.Internal.Rest;

/// <summary>
/// Contains extensions to <see cref="IServiceCollection"/> to aid in registering and utilizing Starnight.
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
		)
		?? throw new InvalidOperationException
		(
			$"Tried to register a decorator for {typeof(TInterface).Name}, but there was no underlying service to decorate."
		);

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

	/// <summary>
	/// Adds a default setup of Starnight to the service collection.
	/// </summary>
	/// <param name="services">The service collection in question.</param>
	/// <param name="token">The Discord bot token to use.</param>
	/// <returns>The service collection for chaining.</returns>
	public static IServiceCollection AddStarnightLibrary
	(
		this IServiceCollection services,
		String token
	)
	{
		return services.AddStarnightLibrary
		(
			token,
			StarnightCacheKind.Memory
		);
	}

	/// <summary>
	/// Registers a setup of Starnight using the specified cache provider kind.
	/// </summary>
	/// <param name="services">The service collection in question.</param>
	/// <param name="token">The Discord bot token to utilize.</param>
	/// <param name="cacheKind">The type of cache provider to utilize.</param>
	/// <returns>The service collection for chaining.</returns>
	/// <exception cref="ArgumentException">Thrown if the <paramref name="cacheKind"/> was unknown.</exception>
	public static IServiceCollection AddStarnightLibrary
	(
		this IServiceCollection services,
		String token,
		StarnightCacheKind cacheKind
	)
	{
		return services.AddStarnightLibrary
		(
			token,
			cacheKind,
			new()
			{
				MedianFirstRequestRetryDelay = TimeSpan.FromSeconds(2),
				RatelimitedRetryCount = 100,
				RetryCount = 100
			}
		);
	}

	/// <summary>
	/// Registers a setup of Starnight using the specified options.
	/// </summary>
	/// <param name="services">The service collection in question.</param>
	/// <param name="token">The Discord bot token to utilize.</param>
	/// <param name="restClientOptions">Options for the rest client implementation.</param>
	/// <returns>The service collection for chaining.</returns>
	public static IServiceCollection AddStarnightLibrary
	(
		this IServiceCollection services,
		String token,
		RestClientOptions restClientOptions
	)
	{
		return services.AddStarnightLibrary
		(
			token,
			StarnightCacheKind.Memory,
			restClientOptions
		);
	}

	/// <summary>
	/// Registers a setup of Starnight using the specified options.
	/// </summary>
	/// <param name="services">The service collection in question.</param>
	/// <param name="token">The Discord bot token to utilize.</param>
	/// <param name="cacheKind">The type of caching provider to utilize.</param>
	/// <param name="restClientOptions">Options for the rest client implementation.</param>
	/// <returns>The service collection for chaining.</returns>
	/// <exception cref="ArgumentException">Thrown if the given <paramref name="cacheKind"/> was unknown.</exception>
	public static IServiceCollection AddStarnightLibrary
	(
		this IServiceCollection services,
		String token,
		StarnightCacheKind cacheKind,
		RestClientOptions restClientOptions
	)
	{
		_ = services.Configure<TokenContainer>
		(
			xm => xm.Token = token
		);

		_ = cacheKind switch
		{
			StarnightCacheKind.Memory => services
				.AddMemoryCache()
				.AddStarnightMemoryCache(),
			_ => throw new ArgumentException
			(
				"Unknown cache provider kind.",
				nameof(cacheKind)
			),
		};

		return services
			.AddStarnightGateway()
			.AddStarnightRestClient
			(
				restClientOptions
			);
	}
}

namespace Starnight.Internal.Extensions;

using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Starnight.Caching.Providers;
using Starnight.Internal.Gateway;
using Starnight.Internal.Rest;

/// <summary>
/// Contains extension methods on <see cref="IHostBuilder"/> to simplify Starnight registration.
/// </summary>
public static class HostBuilderExtensions
{
	/// <summary>
	/// Registers Starnight onto the specified host builder.
	/// </summary>
	/// <param name="host">The host builder in question.</param>
	/// <param name="token">The Discord bot token to utilize.</param>
	/// <returns>The host builder for chaining.</returns>
	public static IHostBuilder AddStarnightLibrary
	(
		this IHostBuilder host,
		String token
	)
	{
		return host.AddStarnightLibrary
		(
			token,
			StarnightCacheKind.Memory
		);
	}

	/// <summary>
	/// Registers Starnight onto the specified host builder.
	/// </summary>
	/// <param name="host">The host builder in question.</param>
	/// <param name="token">The Discord bot token to utilize.</param>
	/// <param name="cacheKind">The kind of caching provider to utilize.</param>
	/// <returns>The host builder for chaining.</returns>
	public static IHostBuilder AddStarnightLibrary
	(
		this IHostBuilder host,
		String token,
		StarnightCacheKind cacheKind
	)
	{
		return host.AddStarnightLibrary
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
	/// Registers Starnight onto the specified host builder.
	/// </summary>
	/// <param name="host">The host builder in question.</param>
	/// <param name="token">The Discord bot token to utilize.</param>
	/// <param name="restClientOptions">Options for the rest client implementations.</param>
	/// <returns>The host builder for chaining.</returns>
	public static IHostBuilder AddStarnightLibrary
	(
		this IHostBuilder host,
		String token,
		RestClientOptions restClientOptions
	)
	{
		return host.AddStarnightLibrary
		(
			token,
			StarnightCacheKind.Memory,
			restClientOptions
		);
	}

	/// <summary>
	/// Registers Starnight onto the specified host builder.
	/// </summary>
	/// <param name="host">The host builder in question.</param>
	/// <param name="token">The Discord bot token to utilize.</param>
	/// <param name="cacheKind">The kind of caching provider to utilize.</param>
	/// <param name="restClientOptions">Options for the rest client implementations.</param>
	/// <returns>The host builder for chaining.</returns>
	/// <exception cref="ArgumentException">Thrown if the given <paramref name="cacheKind"/> was unknown.</exception>
	public static IHostBuilder AddStarnightLibrary
	(
		this IHostBuilder host,
		String token,
		StarnightCacheKind cacheKind,
		RestClientOptions restClientOptions
	)
	{
		_ = host.AddStarnightGateway();

		return host.ConfigureServices
		(
			(ctx, services) =>
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

				_ = services.AddStarnightRestClient
				(
					restClientOptions
				);
			}
		);
	}
}

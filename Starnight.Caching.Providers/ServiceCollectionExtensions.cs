namespace Starnight.Caching.Providers;

using Microsoft.Extensions.DependencyInjection;

using Starnight.Caching.Providers.Abstractions;
using Starnight.Caching.Providers.Memory;

/// <summary>
/// Provides extension methods to <see cref="IServiceCollection"/> for starnight's caching.
/// </summary>
public static class ServiceCollectionExtensions
{
	/// <summary>
	/// Adds starnight's memory caching to the service collection.
	/// </summary>
	public static IServiceCollection AddStarnightMemoryCache(this IServiceCollection services)
		=> services.AddSingleton<ICacheProvider, MemoryCacheProvider>();
}

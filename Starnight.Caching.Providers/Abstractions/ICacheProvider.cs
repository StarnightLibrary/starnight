namespace Starnight.Caching.Providers.Abstractions;

using System;
using System.Threading.Tasks;

/// <summary>
/// Exposes a minimal API to access caches without needing to rely on IMemoryCache, IDistributedCache or the likes.
/// Any more detailed API is the responsibility of implementers, and users may have to cast down to a concrete type
/// with additional API to use specialized features.
/// </summary>
public interface ICacheProvider
{
	/// <summary>
	/// Adds an item to the cache.
	/// </summary>
	/// <typeparam name="TItem">
	///	    Type of the item.
	/// </typeparam>
	/// <param name="key">The cache key this item should use.</param>
	/// <param name="item">The item to be cached.</param>
	public ValueTask CacheAsync<TItem>
	(
		String key,
		TItem item
	);

	/// <summary>
	/// Adds an item to the cache.
	/// </summary>
	/// <param name="entry">The cache entry data to be applied.</param>
	public ValueTask CacheAsync
	(
		BaseCacheEntry entry
	);

	/// <summary>
	/// Removes an item from the cache.
	/// </summary>
	/// <typeparam name="T">The type of the item to be removed.</typeparam>
	/// <param name="key">The cache key used for this item.</param>
	/// <returns>The formerly cached item.</returns>
	public ValueTask<T?> RemoveAsync<T>
	(
		String key
	);

	/// <summary>
	/// Obtains an item from cache.
	/// </summary>
	/// <typeparam name="T">The type of the item to obtain.</typeparam>
	/// <param name="key">The cache key of this item.</param>
	/// <returns>The item, or null if no item was found.</returns>
	public ValueTask<T?> GetAsync<T>
	(
		String key
	);


	/// <summary>
	/// Sets the absolute expiration for a specific type.
	/// </summary>
	/// <typeparam name="T">The type to be set.</typeparam>
	/// <param name="expiration">The new absolute expiration timespan.</param>
	/// <returns>The cache service for chaining.</returns>
	public ICacheProvider SetAbsoluteExpiration<T>
	(
		TimeSpan expiration
	);

	/// <summary>
	/// Sets the sliding expiration for a specific type.
	/// </summary>
	/// <typeparam name="T">The type to be set.</typeparam>
	/// <param name="expiration">The new sliding expiration timespan.</param>
	/// <returns>The cache service for chaining.</returns>
	public ICacheProvider SetSlidingExpiration<T>
	(
		TimeSpan expiration
	);
}

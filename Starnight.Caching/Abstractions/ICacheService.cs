namespace Starnight.Caching.Abstractions;

using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Exposes a minimal API to access caches without needing to rely on IMemoryCache, IDistributedCache or the likes.
/// Any more detailed API is the responsibility of implementers, and users may have to cast down to a concrete type
/// with additional API to use specialized features.
/// </summary>
public interface ICacheService
{
	/// <summary>
	/// Adds an item to the cache.
	/// </summary>
	/// <typeparam name="T">
	///	    Type of the item. Implementers should handle the item according to this type, not according to its concrete type.
	/// </typeparam>
	/// <param name="key">The cache key this item should use.</param>
	/// <param name="item">The item to be cached.</param>
	public void Set<T>
	(
		Object key,
		T item
	);

	/// <summary>
	/// Adds an item to the cache.
	/// </summary>
	/// <typeparam name="TItem">
	///	    Type of the item. Implementers should neglect this type parameter.
	/// </typeparam>
	/// <typeparam name="TInterface">
	///	    Interface type underlying to this item. Implementers should handle the item according to this type, not according
	///	    to <typeparamref name="TItem"/> or its concrete type.
	/// </typeparam>
	/// <param name="key">The cache key this item should use.</param>
	/// <param name="item">The item to be cached.</param>
	public void Set<TItem, TInterface>
	(
		Object key,
		TItem item
	)
		where TItem : TInterface;

	/// <summary>
	/// Adds an item to the cache.
	/// </summary>
	/// <param name="entry">The cache entry data to be applied.</param>
	public void Set
	(
		AbstractCacheEntry entry
	);

	/// <summary>
	/// Adds an item to the cache and treats its <see cref="AbstractCacheEntry.Value"/> as <typeparamref name="TInterface"/>.
	/// </summary>
	/// <typeparam name="TInterface">
	///     Interface type underlying to this item. Implementers should handle the item according to this type.
	/// </typeparam>
	/// <param name="entry">The cache entry data.</param>
	public void Set<TInterface>
	(
		AbstractCacheEntry entry
	);

	/// <summary>
	/// Removes an item from the cache.
	/// </summary>
	/// <typeparam name="T">The type of the item to be removed.</typeparam>
	/// <param name="key">The cache key used for this item.</param>
	/// <returns>The formerly cached item.</returns>
	public T? Remove<T>
	(
		Object key
	);


	/// <summary>
	/// Adds an item to the cache.
	/// </summary>
	/// <typeparam name="T">
	///	    Type of the item. Implementers should handle the item according to this type, not according to its concrete type.
	/// </typeparam>
	/// <param name="key">The cache key this item should use.</param>
	/// <param name="item">The item to be cached.</param>
	public ValueTask SetAsync<T>
	(
		Object key,
		T item
	);

	/// <summary>
	/// Adds an item to the cache.
	/// </summary>
	/// <typeparam name="TItem">
	///	    Type of the item. Implementers should neglect this type parameter.
	/// </typeparam>
	/// <typeparam name="TInterface">
	///	    Interface type underlying to this item. Implementers should handle the item according to this type, not according
	///	    to <typeparamref name="TItem"/> or its concrete type.
	/// </typeparam>
	/// <param name="key">The cache key this item should use.</param>
	/// <param name="item">The item to be cached.</param>
	public ValueTask SetAsync<TItem, TInterface>
	(
		Object key,
		TItem item
	);

	/// <summary>
	/// Adds an item to the cache.
	/// </summary>
	/// <param name="entry">The cache entry data to be applied.</param>
	public ValueTask SetAsync
	(
		AbstractCacheEntry entry
	);

	/// <summary>
	/// Adds an item to the cache and treats its <see cref="AbstractCacheEntry.Value"/> as <typeparamref name="TInterface"/>.
	/// </summary>
	/// <typeparam name="TInterface">
	///     Interface type underlying to this item. Implementers should handle the item according to this type.
	/// </typeparam>
	/// <param name="entry">The cache entry data.</param>
	public ValueTask SetAsync<TInterface>
	(
		AbstractCacheEntry entry
	);

	/// <summary>
	/// Removes an item from the cache.
	/// </summary>
	/// <typeparam name="T">The type of the item to be removed.</typeparam>
	/// <param name="key">The cache key used for this item.</param>
	/// <returns>The formerly cached item.</returns>
	public ValueTask<T?> RemoveAsync<T>
	(
		Object key
	);


	/// <summary>
	/// Sets the absolute expiration for a specific type.
	/// </summary>
	/// <typeparam name="T">The type to be set.</typeparam>
	/// <param name="expiration">The new absolute expiration timespan.</param>
	/// <returns>The cache service for chaining.</returns>
	public ICacheService SetAbsoluteExpiration<T>
	(
		TimeSpan expiration
	);

	/// <summary>
	/// Sets the sliding expiration for a specific type.
	/// </summary>
	/// <typeparam name="T">The type to be set.</typeparam>
	/// <param name="expiration">The new sliding expiration timespan.</param>
	/// <returns>The cache service for chaining.</returns>
	public ICacheService SetSlidingExpiration<T>
	(
		TimeSpan expiration
	);
}

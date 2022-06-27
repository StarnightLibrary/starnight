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
	public void Set<T>(Object key, T item);

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
	public void Set<TItem, TInterface>(Object key, TItem item)
		where TItem : TInterface;

	/// <summary>
	/// Removes an item from the cache.
	/// </summary>
	/// <typeparam name="T">The type of the item to be removed.</typeparam>
	/// <param name="key">The cache key used for this item.</param>
	/// <returns>The formerly cached item.</returns>
	public T Remove<T>(Object key);

	/// <summary>
	/// Adds an item to the cache.
	/// </summary>
	/// <typeparam name="T">
	///	    Type of the item. Implementers should handle the item according to this type, not according to its concrete type.
	/// </typeparam>
	/// <param name="key">The cache key this item should use.</param>
	/// <param name="item">The item to be cached.</param>
	/// <param name="token">Cancellation token for this operation.</param>
	public ValueTask SetAsync<T>(Object key, T item, CancellationToken token);

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
	/// <param name="token">Cancellation token for this operation.</param>
	public ValueTask SetAsync<TItem, TInterface>(Object key, TItem item, CancellationToken token);

	/// <summary>
	/// Removes an item from the cache.
	/// </summary>
	/// <typeparam name="T">The type of the item to be removed.</typeparam>
	/// <param name="key">The cache key used for this item.</param>
	/// <param name="token">Cancellation token for this operation.</param>
	/// <returns>The formerly cached item.</returns>
	public ValueTask<T> RemoveAsync<T>(Object key, CancellationToken token);
}

namespace Starnight.Caching.Services;

using System;
using System.Threading.Tasks;

/// <summary>
/// Represents a specialized caching service for Starnight specifically.
/// </summary>
/// <remarks>
/// Implementations are required to correctly special-case all types Discord treats specially. The exact list
/// of types may vary, and implementers are recommended to keep up to date with the API and the main
/// implementation.
/// </remarks>
public interface IStarnightCacheService
{
	// --- general purpose methods --- //

	/// <summary>
	/// Caches any object, special-casing as needed.
	/// </summary>
	/// <typeparam name="TItem">The type of the object in question.</typeparam>
	/// <param name="cacheKey">The key to cache this object with.</param>
	/// <param name="object">The object in question.</param>
	public ValueTask CacheObjectAsync<TItem>
	(
		String cacheKey,
		TItem @object
	);

	/// <summary>
	/// Deletes an object by its cache key.
	/// </summary>
	/// <typeparam name="TItem">The type of the object to delete.</typeparam>
	/// <returns>The evicted object.</returns>
	public ValueTask<TItem?> EvictObjectAsync<TItem>
	(
		String cacheKey
	);

	/// <summary>
	/// Retrieves an object if possible, and returns <see langword="default"/> if none was to be found.
	/// </summary>
	/// <typeparam name="TItem">The type of the object to retrieve.</typeparam>
	/// <param name="cacheKey">The key by which to retrieve this object.</param>
	public ValueTask<TItem?> RetrieveObjectAsync<TItem>
	(
		String cacheKey
	);
}

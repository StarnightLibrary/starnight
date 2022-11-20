namespace Starnight.Caching.Services;

using System;
using System.Collections.Generic;
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
	/// Caches a list of objects, special-casing as needed.
	/// </summary>
	/// <typeparam name="TItem">The underlying list type.</typeparam>
	/// <typeparam name="TId">The ID type to this item type.</typeparam>
	/// <param name="listKey">The key to cache the list with.</param>
	/// <param name="list">The list to cache.</param>
	/// <param name="itemIdFunction">A function to get the ID of an item from the item.</param>
	/// <param name="itemKeyFunction">
	/// A function to get the individual cache key of an item from its ID.
	/// </param>
	public ValueTask CacheListAsync<TItem, TId>
	(
		String listKey,
		IEnumerable<TItem> list,
		Func<TItem, TId> itemIdFunction,
		Func<TId, String> itemKeyFunction
	);

	/// <summary>
	/// Deletes a list and all its sub-items.
	/// </summary>
	/// <typeparam name="TId">The type used for IDs within this list.</typeparam>
	/// <param name="listKey">The cache key used for this list.</param>
	/// <param name="itemKeyFunction">
	/// A function to get the individual cache key of an item from its ID.
	/// </param>
	public ValueTask DeleteListAsync<TId>
	(
		String listKey,
		Func<TId, String> itemKeyFunction
	);

	/// <summary>
	/// Adds a single item to a list. If the list does not yet exist, a new list is created.
	/// </summary>
	/// <typeparam name="TItem">The type of the item.</typeparam>
	/// <typeparam name="TId">The type used for IDs for this item.</typeparam>
	/// <param name="listKey">The cache key used for this list.</param>
	/// <param name="item">The item to be added.</param>
	/// <param name="itemId">The ID of this item.</param>
	/// <param name="itemKey">The individual cache key for this item.</param>
	public ValueTask AddToListAsync<TItem, TId>
	(
		String listKey,
		TItem item,
		TId itemId,
		String itemKey
	);

	/// <summary>
	/// Removes a single item from a list.
	/// </summary>
	/// <typeparam name="TId">The type used for IDs for this item.</typeparam>
	/// <param name="listKey">The cache key used for this list.</param>
	/// <param name="itemId">The ID of this item.</param>
	/// <param name="itemKey">The cache key for this item.</param>
	/// <returns></returns>
	public ValueTask RemoveFromListAsync<TId>
	(
		String listKey,
		TId itemId,
		String itemKey
	);

	/// <summary>
	/// Deletes an object by its cache key.
	/// </summary>
	/// <typeparam name="TItem">The type of the object to delete.</typeparam>
	public ValueTask DeleteObjectAsync<TItem>
	(
		String cacheKey
	);
}

namespace Starnight.Extensions.Caching.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Starnight.Caching.Providers.Abstractions;

internal static class CacheServiceExtensions
{
	/// <summary>
	/// Caches an object by the given cache key.
	/// </summary>
	/// <typeparam name="T">The type of the object.</typeparam>
	/// <param name="cache">The ICacheService instance this refers to.</param>
	/// <param name="object">The object to be cached.</param>
	/// <param name="cacheKey">The key to cache the object with.</param>
	public static async ValueTask CacheObjectAsync<T>
	(
		this ICacheProvider cache,
		T @object,
		String cacheKey
	)
	{
		String key = cacheKey;

		await cache.CacheAsync
		(
			key,
			@object
		);
	}

	/// <summary>
	/// Caches a list and all its sub-items in the format expected by other components of Starnight.
	/// </summary>
	/// <typeparam name="T">The type of the list.</typeparam>
	/// <typeparam name="TId">The type of the ID used for child items.</typeparam>
	/// <param name="cache">The ICacheService instance in question.</param>
	/// <param name="list">The list to be cached.</param>
	/// <param name="listKey">The key to cache this list under.</param>
	/// <param name="itemKeyFunction">A function to get the cache key from an individual item from the object.</param>
	/// <param name="idFunction">A function to get the ID of a list item.</param>
	public static async ValueTask CacheListAsync<T, TId>
	(
		this ICacheProvider cache,
		IEnumerable<T> list,
		String listKey,
		Func<T, String> itemKeyFunction,
		Func<T, TId> idFunction
	)
	{
		List<TId> ids = new
		(
			list.Select
			(
				xm => idFunction(xm)
			)
		);

		await cache.CacheAsync
		(
			listKey,
			ids
		);

		foreach(T item in list)
		{
			String itemKey = itemKeyFunction(item);

			await cache.CacheAsync
			(
				itemKey,
				item
			);
		}
	}

	/// <summary>
	/// Removes a single item from an existing list.
	/// </summary>
	/// <typeparam name="T">The type of the list items.</typeparam>
	/// <typeparam name="TId">The type of the ID used by the list.</typeparam>
	/// <param name="cache">The ICacheService instance in question.</param>
	/// <param name="id">The ID of the item to remove.</param>
	/// <param name="listKey">The cache key for the list.</param>
	/// <param name="itemKey">The cache key for the item.</param>
	public static async ValueTask RemoveListItemAsync<T, TId>
	(
		this ICacheProvider cache,
		TId id,
		String listKey,
		String itemKey
	)
	{
		List<TId>? ids = await cache.GetAsync<List<TId>>
		(
			listKey
		);

		if(ids is null)
		{
			return;
		}

		if(ids.Remove(id))
		{
			_ = await cache.RemoveAsync<T>
			(
				itemKey
			);
		}
	}

	/// <summary>
	/// Adds a single item to a list. If the list does not yet exist, a new one will be created.
	/// </summary>
	/// <typeparam name="T">The type of the list items.</typeparam>
	/// <typeparam name="TId">The type of the ID used by the list.</typeparam>
	/// <param name="cache">The ICacheService instance in question.</param>
	/// <param name="item">The item to add to the list.</param>
	/// <param name="listKey">The cache key used for this list.</param>
	/// <param name="itemKey">The cache key used for this item.</param>
	/// <param name="idFunction">A function to get the ID of an item from the item.</param>
	public static async ValueTask AddListItemAsync<T, TId>
	(
		this ICacheProvider cache,
		T item,
		String listKey,
		String itemKey,
		Func<T, TId> idFunction
	)
	{
		List<TId>? ids = await cache.GetAsync<List<TId>>
		(
			listKey
		);

		if(ids is null)
		{
			List<TId> list = new()
			{
				idFunction(item)
			};

			await cache.CacheAsync
			(
				listKey,
				list
			);
		}
		else
		{
			ids.Add
			(
				idFunction
				(
					item
				)
			);
		}

		await cache.CacheAsync
		(
			itemKey,
			item
		);
	}

	/// <summary>
	/// Deletes a list and all its child items.
	/// </summary>
	/// <typeparam name="T">The type of the list items.</typeparam>
	/// <typeparam name="TId">The type of the ID used by the list.</typeparam>
	/// <param name="cache">The IMemoryCache instance in question.</param>
	/// <param name="listKey">The cache key used for the list.</param>
	/// <param name="itemKeyFunction">A function to get an item key from an item ID.</param>
	public static async ValueTask DeleteListAsync<T, TId>
	(
		this ICacheProvider cache,
		String listKey,
		Func<TId, String> itemKeyFunction
	)
	{
		List<TId>? list = await cache.RemoveAsync<List<TId>>
		(
			listKey
		);

		if(list is null)
		{
			return;
		}

		foreach(TId id in list)
		{
			_ = await cache.RemoveAsync<T>
			(
				itemKeyFunction(id)
			);
		}
	}
}

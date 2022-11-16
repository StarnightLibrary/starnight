namespace Starnight.Extensions.Caching.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Starnight.Caching.Abstractions;

internal static class CacheServiceExtensions
{
	public static async ValueTask CacheObjectAsync<T>
	(
		this ICacheService cache,
		T @object,
		String cacheKey
	)
	{
		String key = cacheKey;

		await cache.SetAsync
		(
			key,
			@object
		);
	}

	public static async ValueTask CacheListAsync<T, TParent, TId>
	(
		this ICacheService cache,
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

		await cache.SetAsync
		(
			listKey,
			ids
		);

		foreach(T item in list)
		{
			String itemKey = itemKeyFunction(item);

			await cache.SetAsync
			(
				itemKey,
				item
			);
		}
	}

	public static async ValueTask RemoveListItemAsync<T, TId>
	(
		this ICacheService cache,
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

	public static async ValueTask AddListItemAsync<T, TId>
	(
		this ICacheService cache,
		T item,
		String listKey,
		Func<T, String> itemKeyFunction,
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

			await cache.SetAsync
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

		await cache.SetAsync
		(
			itemKeyFunction(item),
			item
		);
	}

	public static async ValueTask DeleteListAsync<T, TId>
	(
		this ICacheService cache,
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

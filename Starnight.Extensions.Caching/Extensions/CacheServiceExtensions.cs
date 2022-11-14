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
		Func<T, String> cacheKeyFunction
	)
	{
		String key = cacheKeyFunction(@object);

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
		TParent parent,
		Func<TParent, IEnumerable<T>, String> listKeyFunction,
		Func<TParent, T, String> itemKeyFunction,
		Func<T, TId> idFunction
	)
	{
		String listKey = listKeyFunction(parent, list);

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
			String itemKey = itemKeyFunction(parent, item);

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
		Func<String> listKeyFunction,
		Func<String> itemKeyFunction
	)
	{
		List<TId>? ids = await cache.GetAsync<List<TId>>
		(
			listKeyFunction()
		);

		if(ids is null)
		{
			return;
		}

		if(ids.Remove(id))
		{
			_ = await cache.RemoveAsync<T>
			(
				itemKeyFunction()
			);
		}
	}

	public static async ValueTask AddListItemAsync<T, TId>
	(
		this ICacheService cache,
		T item,
		Func<String> listKeyFunction,
		Func<T, String> itemKeyFunction,
		Func<T, TId> idFunction
	)
	{
		List<TId>? ids = await cache.GetAsync<List<TId>>
		(
			listKeyFunction()
		);

		if(ids is null)
		{
			List<TId> list = new()
			{
				idFunction(item)
			};

			await cache.SetAsync
			(
				listKeyFunction(),
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
}

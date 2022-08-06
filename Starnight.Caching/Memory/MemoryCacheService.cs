namespace Starnight.Caching.Memory;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

using Starnight.Caching.Abstractions;

/// <summary>
/// A delegate created from a compiled expression to invoke a strictly-typed Set method from an object.
/// </summary>
internal delegate void SetGenericEntryDelegate(Object key, Object value);

// the same, but for async methods
internal delegate ValueTask AsyncSetGenericEntryDelegate(Object key, Object value);

/// <summary>
/// An implementation of <see cref="ICacheService"/> relying on a memory cache.
/// </summary>
public class MemoryCacheService : ICacheService
{
	private readonly MemoryCacheOptions __options;
	private readonly MemoryCache __backing;
	private readonly Dictionary<Type, SetGenericEntryDelegate> __generic_delegates;
	private readonly Dictionary<Type, AsyncSetGenericEntryDelegate> __async_delegates;

	/// <inheritdoc/>
	public MemoryCacheService
	(
		IOptions<MemoryCacheOptions> options,
		IMemoryCache cache
	)
	{
		this.__options = options.Value;
		this.__backing = (cache as MemoryCache) ?? throw new ArgumentException("At this time, MemoryCacheService only supports " +
			"Microsoft.Extensions.Caching.Memory.MemoryCache as backing cache.");

		this.__generic_delegates = new();
		this.__async_delegates = new();
	}

	/// <inheritdoc/>
	public MemoryCacheService
	(
		IOptions<MemoryCacheOptions> options
	)
	{
		this.__options = options.Value;

		Microsoft.Extensions.Caching.Memory.MemoryCacheOptions settings = new()
		{
			CompactionPercentage = this.__options.CompactionPercentage,
			ExpirationScanFrequency = this.__options.ExpirationScanFrequency,
			SizeLimit = this.__options.SizeLimit,
			TrackStatistics = this.__options.TrackStatistics
		};

		this.__backing = new MemoryCache(settings);

		this.__generic_delegates = new();
		this.__async_delegates = new();
	}

	/// <summary>
	/// Creates a new instance of this type, with a new backing cache.
	/// </summary>
	/// <param name="options">The options to be passed to the new instance.</param>
	public static MemoryCacheService Create
	(
		IOptions<MemoryCacheOptions> options
	)
		=> new(options);

	/// <inheritdoc/>
	public T? Remove<T>
	(
		Object key
	)
	{
		T? value = this.__backing.Get<T>(key);

		this.__backing.Remove(key);

		return value;
	}

	/// <inheritdoc/>
	public ValueTask<T?> RemoveAsync<T>
	(
		Object key
	)
		=> ValueTask.FromResult(this.Remove<T>(key));

	/// <summary>
	/// Adds an item to the cache.
	/// </summary>
	/// <typeparam name="T">Type of the item.</typeparam>
	/// <param name="key">The cache key this item should use.</param>
	/// <param name="item">The item to be cached.</param>
	public void Set<T>
	(
		Object key,
		T item
	)
	{
		TimeSpan absolute = this.__options.AbsoluteExpirations.TryGetValue(typeof(T).TypeHandle.Value, out TimeSpan value)
			? value
			: this.__options.DefaultAbsoluteExpiration;

		TimeSpan sliding = this.__options.SlidingExpirations.TryGetValue(typeof(T).TypeHandle.Value, out value)
			? value
			: this.__options.DefaultSlidingExpiration;

		_ = this.__backing.CreateEntry(key)
			.SetAbsoluteExpiration(absolute)
			.SetSlidingExpiration(sliding)
			.SetValue(item!);
	}

	/// <summary>
	/// Adds an item to the cache.
	/// </summary>
	/// <typeparam name="TItem">Type of the item.</typeparam>
	/// <typeparam name="TInterface">Interface type this item should be cached as.</typeparam>
	/// <param name="key">The cache key this item should use.</param>
	/// <param name="item">The item to be cached.</param>
	public void Set<TItem, TInterface>
	(
		Object key,
		TItem item
	)
		where TItem : TInterface
	{
		TimeSpan absolute = this.__options.AbsoluteExpirations.TryGetValue(typeof(TInterface).TypeHandle.Value, out TimeSpan value)
			? value
			: this.__options.DefaultAbsoluteExpiration;

		TimeSpan sliding = this.__options.SlidingExpirations.TryGetValue(typeof(TInterface).TypeHandle.Value, out value)
			? value
			: this.__options.DefaultSlidingExpiration;

		_ = this.__backing.CreateEntry(key)
			.SetAbsoluteExpiration(absolute)
			.SetSlidingExpiration(sliding)
			.SetValue(item!);
	}

	/// <inheritdoc/>
	public void Set
	(
		AbstractCacheEntry entry
	)
	{
		if(entry is not MemoryCacheEntry memoryEntry)
		{
			if(this.__generic_delegates.TryGetValue(entry.Value.GetType(), out SetGenericEntryDelegate? del))
			{
				del!(entry.Key, entry.Value);

				return;
			}

			SetGenericEntryDelegate currentDelegate = this.createDelegate(entry.Value.GetType());

			this.__generic_delegates.Add(entry.Value.GetType(), currentDelegate);

			currentDelegate(entry.Key, entry.Value);

			return;
		}

		TimeSpan absolute = memoryEntry.AbsoluteExpiration ??
			(this.__options.AbsoluteExpirations.TryGetValue(memoryEntry.Value.GetType().TypeHandle.Value, out TimeSpan value)
				? value
				: this.__options.DefaultAbsoluteExpiration);

		TimeSpan sliding = memoryEntry.SlidingExpiration ??
			(this.__options.SlidingExpirations.TryGetValue(memoryEntry.Value.GetType().TypeHandle.Value, out value)
				? value
				: this.__options.DefaultSlidingExpiration);

		ICacheEntry finalEntry = this.__backing.CreateEntry(memoryEntry.Key)
			.SetValue(memoryEntry.Value)
			.SetAbsoluteExpiration(absolute)
			.SetSlidingExpiration(sliding);

		if(memoryEntry.PostEvictionCallback is not null)
		{
			_ = finalEntry.RegisterPostEvictionCallback(memoryEntry.PostEvictionCallback);
		}
	}

	/// <inheritdoc/>
	public void Set<TInterface>
	(
		AbstractCacheEntry entry
	)
	{
		ICacheEntry cacheEntry = this.__backing.CreateEntry(entry.Key)
			.SetValue(entry.Value);

		if(entry is not MemoryCacheEntry memoryEntry)
		{
			return;
		}

		TimeSpan absolute = memoryEntry.AbsoluteExpiration ??
			(this.__options.AbsoluteExpirations.TryGetValue(memoryEntry.Value.GetType().TypeHandle.Value, out TimeSpan value)
				? value
				: this.__options.DefaultAbsoluteExpiration);

		TimeSpan sliding = memoryEntry.SlidingExpiration ??
			(this.__options.SlidingExpirations.TryGetValue(memoryEntry.Value.GetType().TypeHandle.Value, out value)
				? value
				: this.__options.DefaultSlidingExpiration);

		_ = cacheEntry.SetAbsoluteExpiration(absolute)
			.SetSlidingExpiration(sliding);

		if(memoryEntry.PostEvictionCallback is not null)
		{
			_ = cacheEntry.RegisterPostEvictionCallback(memoryEntry.PostEvictionCallback);
		}
	}

	/// <inheritdoc/>
	public ICacheService SetAbsoluteExpiration<T>
	(
		TimeSpan expiration
	)
	{
		this.__options.AbsoluteExpirations[typeof(T).TypeHandle.Value] = expiration;

		return this;
	}

	/// <inheritdoc/>
	public ValueTask SetAsync<T>
	(
		Object key,
		T item
	)
	{
		TimeSpan absolute = this.__options.AbsoluteExpirations.TryGetValue(typeof(T).TypeHandle.Value, out TimeSpan value)
			? value
			: this.__options.DefaultAbsoluteExpiration;

		TimeSpan sliding = this.__options.SlidingExpirations.TryGetValue(typeof(T).TypeHandle.Value, out value)
			? value
			: this.__options.DefaultSlidingExpiration;

		_ = this.__backing.CreateEntry(key)
			.SetAbsoluteExpiration(absolute)
			.SetSlidingExpiration(sliding)
			.SetValue(item!);

		return ValueTask.CompletedTask;
	}

	/// <inheritdoc/>
	public ValueTask SetAsync<TItem, TInterface>
	(
		Object key,
		TItem item
	)
		where TItem : TInterface
	{
		TimeSpan absolute = this.__options.AbsoluteExpirations.TryGetValue(typeof(TInterface).TypeHandle.Value, out TimeSpan value)
			? value
			: this.__options.DefaultAbsoluteExpiration;

		TimeSpan sliding = this.__options.SlidingExpirations.TryGetValue(typeof(TInterface).TypeHandle.Value, out value)
			? value
			: this.__options.DefaultSlidingExpiration;

		_ = this.__backing.CreateEntry(key)
			.SetAbsoluteExpiration(absolute)
			.SetSlidingExpiration(sliding)
			.SetValue(item!);

		return ValueTask.CompletedTask;
	}

	/// <inheritdoc/>
	public ValueTask SetAsync
	(
		AbstractCacheEntry entry
	)
	{
		if(entry is not MemoryCacheEntry memoryEntry)
		{
			if(this.__async_delegates.TryGetValue(entry.Value.GetType(), out AsyncSetGenericEntryDelegate? del))
			{
				return del!(entry.Key, entry.Value);
			}

			AsyncSetGenericEntryDelegate currentDelegate = this.createAsyncDelegate(entry.Value.GetType());

			this.__async_delegates.Add(entry.Value.GetType(), currentDelegate);

			return currentDelegate(entry.Key, entry.Value);
		}

		TimeSpan absolute = memoryEntry.AbsoluteExpiration ??
			(this.__options.AbsoluteExpirations.TryGetValue(memoryEntry.Value.GetType().TypeHandle.Value, out TimeSpan value)
				? value
				: this.__options.DefaultAbsoluteExpiration);

		TimeSpan sliding = memoryEntry.SlidingExpiration ??
			(this.__options.SlidingExpirations.TryGetValue(memoryEntry.Value.GetType().TypeHandle.Value, out value)
				? value
				: this.__options.DefaultSlidingExpiration);

		ICacheEntry finalEntry = this.__backing.CreateEntry(memoryEntry.Key)
			.SetValue(memoryEntry.Value)
			.SetAbsoluteExpiration(absolute)
			.SetSlidingExpiration(sliding);

		if(memoryEntry.PostEvictionCallback is not null)
		{
			_ = finalEntry.RegisterPostEvictionCallback(memoryEntry.PostEvictionCallback);
		}

		return ValueTask.CompletedTask;
	}

	/// <inheritdoc/>
	public ValueTask SetAsync<TInterface>
	(
		AbstractCacheEntry entry
	)
	{
		ICacheEntry cacheEntry = this.__backing.CreateEntry(entry.Key)
			.SetValue(entry.Value);

		if(entry is not MemoryCacheEntry memoryEntry)
		{
			return ValueTask.CompletedTask;
		}

		TimeSpan absolute = memoryEntry.AbsoluteExpiration ??
			(this.__options.AbsoluteExpirations.TryGetValue(memoryEntry.Value.GetType().TypeHandle.Value, out TimeSpan value)
				? value
				: this.__options.DefaultAbsoluteExpiration);

		TimeSpan sliding = memoryEntry.SlidingExpiration ??
			(this.__options.SlidingExpirations.TryGetValue(memoryEntry.Value.GetType().TypeHandle.Value, out value)
				? value
				: this.__options.DefaultSlidingExpiration);

		_ = cacheEntry.SetAbsoluteExpiration(absolute)
			.SetSlidingExpiration(sliding);

		if(memoryEntry.PostEvictionCallback is not null)
		{
			_ = cacheEntry.RegisterPostEvictionCallback(memoryEntry.PostEvictionCallback);
		}

		return ValueTask.CompletedTask;
	}

	/// <inheritdoc/>
	public ICacheService SetSlidingExpiration<T>
	(
		TimeSpan expiration
	)
	{
		this.__options.SlidingExpirations[typeof(T).TypeHandle.Value] = expiration;

		return this;
	}

	// gets a compiled expression for invoking the single-generic-parameter Set method from an Object type
	private SetGenericEntryDelegate createDelegate
	(
		Type valueType
	)
	{
		// get the single-generic-parameter Set method
		// adapted from https://stackoverflow.com/a/44569347
		MethodInfo? method = typeof(MemoryCacheService)
			.GetRuntimeMethods()
			.Where(xm => xm.Name == "Set")
			.Select(xm => new
			{
				Method = xm,
				Parameters = xm.GetParameters(),
				GenericArguments = xm.GetGenericArguments()
			})
			.FirstOrDefault(xm =>
				xm.Parameters.Length == 2 &&
				xm.GenericArguments.Length == 1
			)
			?.Method;

		// ascertain it exists
		if(method is null)
		{
			throw new MissingMethodException($"The method {nameof(MemoryCacheService)}#{nameof(Set)} went missing.");
		}

		// get the implemented generic for our type
		MethodInfo generic = method!.MakeGenericMethod(valueType);

		// create parameter expressions for the delegate parameters
		ParameterExpression key = Expression.Parameter(typeof(Object), "key");
		ParameterExpression value = Expression.Parameter(valueType, "value");

		// call our generic method
		MethodCallExpression call = Expression.Call(generic, key, value);

		// compile the expression and return
		return Expression.Lambda<SetGenericEntryDelegate>(call).Compile();
	}

	// same as above but for async
	private AsyncSetGenericEntryDelegate createAsyncDelegate(Type valueType)
	{
		// get the single-generic-parameter SetAsync method
		// adapted from https://stackoverflow.com/a/44569347
		MethodInfo? method = typeof(MemoryCacheService)
			.GetRuntimeMethods()
			.Where(xm => xm.Name == "SetAsync")
			.Select(xm => new
			{
				Method = xm,
				Parameters = xm.GetParameters(),
				GenericArguments = xm.GetGenericArguments()
			})
			.FirstOrDefault(xm =>
				xm.Parameters.Length == 2 &&
				xm.GenericArguments.Length == 1
			)
			?.Method;

		// ascertain it exists
		if(method is null)
		{
			throw new MissingMethodException($"The method {nameof(MemoryCacheService)}#{nameof(Set)} went missing.");
		}

		// get the implemented generic for our type
		MethodInfo generic = method!.MakeGenericMethod(valueType);

		// create parameter expressions for the delegate parameters
		ParameterExpression key = Expression.Parameter(typeof(Object), "key");
		ParameterExpression value = Expression.Parameter(valueType, "value");

		// call our generic method
		MethodCallExpression call = Expression.Call(generic, key, value);

		// compile the expression and return
		return Expression.Lambda<AsyncSetGenericEntryDelegate>(call).Compile();
	}

	/// <inheritdoc/>
	public T? Get<T>
	(
		Object key
	)
	{
		_ = this.__backing.TryGetValue(key, out T? value);

		return value;
	}

	/// <inheritdoc/>
	public ValueTask<T?> GetAsync<T>
	(
		Object key
	)
	{
		_ = this.__backing.TryGetValue(key, out T? value);

		return ValueTask.FromResult(value);
	}
}

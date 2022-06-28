namespace Starnight.Caching.Memory;

using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

using Starnight.Caching.Abstractions;

/// <summary>
/// An implementation of <see cref="ICacheService"/> relying on a memory cache.
/// </summary>
public class MemoryCacheService : ICacheService
{
	private readonly MemoryCacheOptions __options;
	private readonly MemoryCache __backing;

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
		TimeSpan absolute = this.__options.AbsoluteExpirations.ContainsKey(typeof(T).TypeHandle.Value)
			? this.__options.AbsoluteExpirations[typeof(T).TypeHandle.Value]
			: this.__options.DefaultAbsoluteExpiration;

		TimeSpan sliding = this.__options.SlidingExpirations.ContainsKey(typeof(T).TypeHandle.Value)
			? this.__options.SlidingExpirations[typeof(T).TypeHandle.Value]
			: this.__options.DefaultSlidingExpiration;

		_ = this.__backing.CreateEntry(key)
			.SetAbsoluteExpiration(absolute)
			.SetSlidingExpiration(sliding)
			.SetValue(item!);
	}

	public void Set<TItem, TInterface>(Object key, TItem item) where TItem : TInterface => throw new NotImplementedException();
	public void Set(AbstractCacheEntry entry) => throw new NotImplementedException();
	public void Set<TInterface>(AbstractCacheEntry entry) => throw new NotImplementedException();
	public ICacheService SetAbsoluteExpiration<T>(TimeSpan expiration) => throw new NotImplementedException();
	public ValueTask SetAsync<T>(Object key, T item) => throw new NotImplementedException();
	public ValueTask SetAsync<TItem, TInterface>(Object key, TItem item) => throw new NotImplementedException();
	public ValueTask SetAsync(AbstractCacheEntry entry) => throw new NotImplementedException();
	public ValueTask SetAsync<TInterface>(AbstractCacheEntry entry) => throw new NotImplementedException();
	public ICacheService SetSlidingExpiration<T>(TimeSpan expiration) => throw new NotImplementedException();
}

namespace Starnight.Caching.Providers.Memory;

using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

using Starnight.Caching.Providers.Abstractions;

/// <summary>
/// An implementation of <see cref="ICacheProvider"/> relying on a memory cache.
/// </summary>
public class MemoryCacheProvider : ICacheProvider
{
	private readonly MemoryCacheProviderOptions options;
	private readonly IMemoryCache backing;

	/// <inheritdoc/>
	public MemoryCacheProvider
	(
		IOptions<MemoryCacheProviderOptions> options,
		IMemoryCache cache
	)
	{
		this.options = options.Value;
		this.backing = cache;
	}

	/// <inheritdoc/>
	public MemoryCacheProvider
	(
		IOptions<MemoryCacheProviderOptions> options
	)
	{
		this.options = options.Value;

		MemoryCacheOptions settings = new()
		{
			CompactionPercentage = this.options.CompactionPercentage,
			ExpirationScanFrequency = this.options.ExpirationScanFrequency,
			SizeLimit = this.options.SizeLimit,
			TrackStatistics = this.options.TrackStatistics
		};

		this.backing = new MemoryCache
		(
			settings
		);
	}

	/// <summary>
	/// Creates a new instance of this type, with a new backing cache.
	/// </summary>
	/// <param name="options">The options to be passed to the new instance.</param>
	public static MemoryCacheProvider Create
	(
		IOptions<MemoryCacheProviderOptions> options
	)
		=> new(options);

	/// <inheritdoc/>
	public ValueTask<T?> RemoveAsync<T>
	(
		String key
	)
	{
		T? item = this.backing.Get<T>
		(
			key
		);

		this.backing.Remove
		(
			key
		);

		return ValueTask.FromResult(item);
	}

	/// <inheritdoc/>
	public ValueTask CacheAsync<T>
	(
		String key,
		T item
	)
	{
		this.backing.CreateEntry
			(
				key
			)
			.SetAbsoluteExpiration
			(
				this.options.GetAbsoluteExpiration<T>()
			)
			.SetSlidingExpiration
			(
				this.options.GetSlidingExpiration<T>()
			)
			.SetValue
			(
				item!
			)
			.Dispose();

		return ValueTask.CompletedTask;
	}

	/// <inheritdoc/>
	public ValueTask CacheAsync
	(
		BaseCacheEntry entry
	)
	{
		using ICacheEntry finalEntry = this.backing.CreateEntry
		(
			entry.Key
		);

		if(entry is MemoryCacheEntry memoryEntry)
		{
			TimeSpan absolute = memoryEntry.AbsoluteExpiration ?? this.options.GetAbsoluteExpiration<Object>();

			TimeSpan sliding = memoryEntry.SlidingExpiration ?? this.options.GetSlidingExpiration<Object>();

			_ = finalEntry.SetValue
				(
					memoryEntry.Value
				)
				.SetAbsoluteExpiration
				(
					absolute
				)
				.SetSlidingExpiration
				(
					sliding
				);


			if(memoryEntry.PostEvictionCallback is not null)
				_ = finalEntry.RegisterPostEvictionCallback
				(
					memoryEntry.PostEvictionCallback
				);
		}
		else
			_ = finalEntry.SetValue
				(
					entry.Value
				)
				.SetAbsoluteExpiration
				(
					this.options.GetAbsoluteExpiration<Object>()
				)
				.SetSlidingExpiration
				(
					this.options.GetSlidingExpiration<Object>()
				);

		return ValueTask.CompletedTask;
	}

	/// <summary>
	/// Creates a new <see cref="ICacheEntry"/> and returns it, leaving responsibility for what to do up to the caller.
	/// </summary>
	/// <param name="key">The cache key for this entry.</param>
	public ValueTask<ICacheEntry> SetAsync
	(
		Object key
	)
	{
		return ValueTask.FromResult
		(
			this.backing.CreateEntry
			(
				key
			)
		);
	}

	/// <inheritdoc/>
	public ICacheProvider SetSlidingExpiration<T>
	(
		TimeSpan expiration
	)
	{
		this.options.SlidingExpirations[typeof(T)] = expiration;

		return this;
	}

	/// <inheritdoc/>
	public ICacheProvider SetAbsoluteExpiration<T>
	(
		TimeSpan expiration
	)
	{
		this.options.AbsoluteExpirations[typeof(T)] = expiration;

		return this;
	}

	/// <inheritdoc/>
	public ValueTask<T?> GetAsync<T>
	(
		String key
	)
	{
		_ = this.backing.TryGetValue
		(
			key,
			out T? value
		);

		return ValueTask.FromResult
		(
			value
		);
	}
}

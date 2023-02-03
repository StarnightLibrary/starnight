namespace Starnight.Caching.Providers.Memory;

using System;

using Microsoft.Extensions.Options;

using Starnight.Caching.Providers.Abstractions;

/// <summary>
/// Represents options for <see cref="MemoryCacheProvider"/>.
/// </summary>
public class MemoryCacheProviderOptions : BaseCacheProviderOptions, IOptions<MemoryCacheProviderOptions>
{
	MemoryCacheProviderOptions IOptions<MemoryCacheProviderOptions>.Value => this;

	/// <summary>
	/// Specifies how much the cache should be compacted by when the maximum size is exceeded.
	/// </summary>
	public Double CompactionPercentage { get; set; }

	/// <summary>
	/// Specifies the minimum time between successive scans for expired items.
	/// </summary>
	public TimeSpan ExpirationScanFrequency { get; set; }

	/// <summary>
	/// Specifies the maximum size of the cache.
	/// </summary>
	public Int64? SizeLimit { get; set; }

	/// <summary>
	/// Specifies whether statistics should be tracked for this cache.
	/// </summary>
	public Boolean TrackStatistics { get; set; }


	// working set: expose statistics pertaining to reflection use
	//
	// /// <summary>
	// /// Requires <see cref="TrackStatistics"/> to be set to true. If enabled, the memory cache service
	// /// will expose its statistics to the rest of the world.
	// /// </summary>
	// public Boolean ExposeStatistics { get; set; }
}

namespace Starnight.Caching.Memory;

using System;

using Microsoft.Extensions.Options;

using Starnight.Caching.Abstractions;

/// <summary>
/// Represents options for <see cref="MemoryCacheService"/>.
/// </summary>
public class MemoryCacheOptions : AbstractCacheOptions, IOptions<MemoryCacheOptions>
{
	MemoryCacheOptions IOptions<MemoryCacheOptions>.Value => this;

	/// <summary>
	/// Specifies whether statistics should be tracked for this cache.
	/// </summary>
	public Boolean TrackStatistics { get; set; }

	/// <summary>
	/// Requires <see cref="TrackStatistics"/> to be set to true. If enabled, the memory cache service
	/// will expose its statistics to the rest of the world.
	/// </summary>
	public Boolean ExposeStatistics { get; set; }
}

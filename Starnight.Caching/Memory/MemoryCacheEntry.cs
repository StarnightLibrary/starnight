namespace Starnight.Caching.Memory;

using System;

using Microsoft.Extensions.Caching.Memory;

using Starnight.Caching.Abstractions;

/// <summary>
/// Represents an entry in a <see cref="MemoryCacheService"/>
/// </summary>
public record MemoryCacheEntry : AbstractCacheEntry
{
    /// <summary>
	/// Overrides the absolute expiration for this entry.
	/// </summary>
    public TimeSpan? AbsoluteExpiration { get; set; }

    /// <summary>
	/// Overrides the sliding expiration for this entry.
	/// </summary>
    public TimeSpan? SlidingExpiration { get; set; }

    /// <summary>
	/// Sets a post-eviction callback for this entry.
	/// </summary>
    public PostEvictionDelegate? PostEvictionCallback { get; set; }
}

namespace Starnight.Caching.Providers.Abstractions;

using System;

/// <summary>
/// Represents an extendable class for more complex cache entries supported by <see cref="ICacheProvider"/>.
/// </summary>
public record BaseCacheEntry
{
	/// <summary>
	/// The key used to store this object into cache.
	/// </summary>
	public String Key { get; set; } = null!;

	/// <summary>
	/// The value of this cache entry.
	/// </summary>
	public Object Value { get; set; } = null!;
}

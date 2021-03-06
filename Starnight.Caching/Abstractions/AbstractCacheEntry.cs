namespace Starnight.Caching.Abstractions;

using System;

/// <summary>
/// Represents an extendable class for more complex cache entries supported by <see cref="ICacheService"/>.
/// </summary>
public abstract record AbstractCacheEntry
{
	/// <summary>
	/// The key used to store this object into cache.
	/// </summary>
	public Object Key { get; set; } = null!;

	/// <summary>
	/// The value of this cache entry.
	/// </summary>
	public Object Value { get; set; } = null!;
}

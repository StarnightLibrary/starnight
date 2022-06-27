namespace Starnight.Caching.Abstractions;

using System;

/// <summary>
/// Represents an extendable interface for more complex cache entries supported by <see cref="ICacheService"/>.
/// </summary>
public interface ICacheEntry
{
	/// <summary>
	/// The key used to store this object into cache.
	/// </summary>
	public Object Key { get; }

	/// <summary>
	/// The value of this cache entry.
	/// </summary>
	public Object Value { get; }	
}

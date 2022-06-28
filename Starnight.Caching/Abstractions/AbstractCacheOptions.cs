namespace Starnight.Caching.Abstractions;

using System;
using System.Collections.Generic;

/// <summary>
/// Represents a base option class for all cache options.
/// </summary>
public abstract class AbstractCacheOptions
{
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
    /// Specifies a default absolute expiration for all types without a specified expiration time.
    /// </summary>
    public TimeSpan DefaultAbsoluteExpiration { get; set; }

    /// <summary>
    /// Specifies a default sliding expiration for all types without a specified expiration time.
    /// </summary>
    public TimeSpan DefaultSlidingExpiration { get; set; }

    /// <summary>
    /// Stores all absolute expirations for concrete types.
    /// </summary>
    public Dictionary<IntPtr, TimeSpan> AbsoluteExpirations { get; set; } = new();

    /// <summary>
    /// Stores all sliding expirations for concrete types.
    /// </summary>
    public Dictionary<IntPtr, TimeSpan> SlidingExpirations { get; set; } = new();

    /// <summary>
    /// Sets the absolute expiration for the specified type.
    /// </summary>
    /// <remarks>
    /// This method distinguishes between interfaces and concrete types in its implementation.
    /// </remarks>
    /// <typeparam name="T">The type registered to the cache.</typeparam>
    /// <param name="time">The absolute expiration time for this type.</param>
    /// <returns>The cache options object for chaining.</returns>
    public AbstractCacheOptions SetAbsoluteExpiration<T>
    (
        TimeSpan time
    )
    {
        this.AbsoluteExpirations[typeof(T).TypeHandle.Value] = time;

        return this;
    }

    /// <summary>
    /// Sets the sliding expiration for the specified type.
    /// </summary>
    /// <remarks>
    /// This method distinguishes between interfaces and concrete types in its implementation.
    /// </remarks>
    /// <typeparam name="T">The type registered to the cache.</typeparam>
    /// <param name="time">The sliding expiration time for this type.</param>
    /// <returns>The cache options object for chaining.</returns>
    public AbstractCacheOptions SetSlidingExpiration<T>
    (
        TimeSpan time
    )
    {
        this.SlidingExpirations[typeof(T).TypeHandle.Value] = time;

        return this;
    }
}

namespace Starnight.Caching.Providers.Abstractions;

using System;
using System.Collections.Generic;

/// <summary>
/// Represents a base option class for all cache options.
/// </summary>
public class BaseCacheProviderOptions
{
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
	public Dictionary<Type, TimeSpan> AbsoluteExpirations { get; set; } = new();

	/// <summary>
	/// Stores all sliding expirations for concrete types.
	/// </summary>
	public Dictionary<Type, TimeSpan> SlidingExpirations { get; set; } = new();

	/// <summary>
	/// Sets the absolute expiration for the specified type.
	/// </summary>
	/// <remarks>
	/// This method distinguishes between interfaces and concrete types in its implementation.
	/// </remarks>
	/// <typeparam name="T">The type registered to the cache.</typeparam>
	/// <param name="time">The absolute expiration time for this type.</param>
	/// <returns>The cache options object for chaining.</returns>
	public BaseCacheProviderOptions SetAbsoluteExpiration<T>
	(
		TimeSpan time
	)
	{
		this.AbsoluteExpirations[typeof(T)] = time;

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
	public BaseCacheProviderOptions SetSlidingExpiration<T>
	(
		TimeSpan time
	)
	{
		this.SlidingExpirations[typeof(T)] = time;

		return this;
	}

	/// <summary>
	/// Returns the absolute expiration for the specified type.
	/// </summary>
	public TimeSpan GetAbsoluteExpiration<T>()
	{
		return this.AbsoluteExpirations.TryGetValue
		(
			typeof(T),
			out TimeSpan value
		)
			? value
			: this.DefaultAbsoluteExpiration;
	}

	/// <summary>
	/// Returns the sliding expiration for the specified type.
	/// </summary>
	public TimeSpan GetSlidingExpiration<T>()
	{
		return this.AbsoluteExpirations.TryGetValue
		(
			typeof(T),
			out TimeSpan value
		)
			? value
			: this.DefaultAbsoluteExpiration;
	}
}

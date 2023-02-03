namespace Starnight.Internal.Rest;

using System;

/// <summary>
/// Holds options for the Starnight rest client, as far as it is configurable.
/// </summary>
public class RestClientOptions
{
	/// <summary>
	/// Median delay before a rest request is retried the first time.
	/// </summary>
	public TimeSpan MedianFirstRequestRetryDelay { get; set; }

	/// <summary>
	/// Amount of retries before a non-429 request is dropped entirely.
	/// </summary>
	public Int32 RetryCount { get; set; }

	/// <summary>
	/// Amount of retries before a 429 request is dropped entirely.
	/// </summary>
	public Int32 RatelimitedRetryCount { get; set; }
}

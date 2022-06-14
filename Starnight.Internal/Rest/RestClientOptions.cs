namespace Starnight.Internal.Rest;

using System;

/// <summary>
/// Holds options for the Starnight rest client, as far as it is configurable.
/// </summary>
public record RestClientOptions
{
	/// <summary>
	/// Median delay before a rest request is retried the first time.
	/// </summary>
	public TimeSpan MedianFirstRequestRetryDelay { get; init; }

	/// <summary>
	/// Amount of retries before a non-429 request is dropped entirely.
	/// </summary>
	public Int32 RetryCount { get; init; }

	/// <summary>
	/// Amount of retries before a 429 request is dropped entirely.
	/// </summary>
	public Int32 RatelimitedRetryCount { get; init; }

	/// <summary>
	/// Authentication token used for all requests to Discord's API.
	/// </summary>
	public String Token { get; init; } = null!;
}

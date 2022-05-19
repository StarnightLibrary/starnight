namespace Starnight;

using System;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Stores options to be passed to the <see cref="StarnightClient"/>.
/// </summary>
public record StarnightClientOptions
{
	/// <summary>
	/// Existing service collection for the client's use.
	/// Starnight will register all its services into here.
	/// </summary>
	public IServiceCollection? Services { get; init; }

	/// <summary>
	/// Whether you want to use a custom logger.
	/// </summary>
	public Boolean UseCustomLogger { get; init; } = false;

	/// <summary>
	/// Rest: Median delay before a rest request is retried the first time.
	/// </summary>
	public TimeSpan MedianFirstRequestRetryDelay { get; init; }

	/// <summary>
	/// Rest: Amount of retries before a non-429 request is dropped entirely.
	/// </summary>
	public Int32 RetryCount { get; init; }

	/// <summary>
	/// Rest: Amount of retries before a 429 request is dropped entirely.
	/// </summary>
	public Int32 RatelimitedRetryCount { get; init; }

	/// <summary>
	/// Authentication token used for all requests to Discord's API.
	/// </summary>
	public String Token { get; init; } = null!;
}

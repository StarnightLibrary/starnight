namespace Starnight;

using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

/// <summary>
/// Stores options to be passed to the <see cref="StarnightClient"/>.
/// </summary>
public class StarnightClientOptions : IOptions<StarnightClientOptions>
{
	/// <summary>
	/// An optional existing service collection, if you choose to register your own services
	/// for Starnight to use.
	/// </summary>
	public IServiceCollection? Services { get; init; }

	/// <summary>
	/// Specifies whether Starnight should register its own logger. Default: false
	/// </summary>
	public Boolean UseDefaultLogger { get; init; } = false;

	/// <summary>
	/// Rest: Median delay before a rest request is retried the first time.
	/// </summary>
	public TimeSpan? AverageFirstRequestRetryDelay { get; init; }

	/// <summary>
	/// Rest: Amount of retries before a non-429 request is dropped entirely.
	/// </summary>
	public Int32? RetryCount { get; init; }

	/// <summary>
	/// Rest: Amount of retries before a 429 request is dropped entirely.
	/// </summary>
	public Int32? RatelimitedRetryCount { get; init; }

	/// <summary>
	/// Authentication token used for all requests to Discord's API.
	/// </summary>
	public required String Token { get; init; }

	/// <summary>
	/// Gateway: The amount of members a guild can have before the gateway will stop sending offline
	/// members.
	/// </summary>
	public Int32? LargeGuildThreshold { get; init; }

	/// <summary>
	/// Gateway: ID of the current shard.
	/// </summary>
	public Int32? ShardId { get; init; }

	/// <summary>
	/// Gateway: Total amount of shards for this bot.
	/// </summary>
	public Int32? ShardCount { get; init; }

	/// <summary>
	/// Gateway: The amount of consecutive missed heartbeats to cause Starnight to consider the connection
	/// zombied.
	/// </summary>
	public Int32? ZombieThreshold { get; init; }

	public StarnightClientOptions Value => this;
}

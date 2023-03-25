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
	public IServiceCollection? Services { get; set; }

	/// <summary>
	/// Specifies whether Starnight should register its own logger. Defaults to true.
	/// </summary>
	public Boolean UseDefaultLogger { get; set; } = true;

	/// <summary>
	/// Rest: Median delay before a rest request is retried the first time.
	/// </summary>
	public TimeSpan? AverageFirstRequestRetryDelay { get; set; }

	/// <summary>
	/// Rest: Amount of retries before a non-429 request is dropped entirely.
	/// </summary>
	public Int32? RetryCount { get; set; }

	/// <summary>
	/// Rest: Amount of retries before a 429 request is dropped entirely.
	/// </summary>
	public Int32? RatelimitedRetryCount { get; set; }

	/// <summary>
	/// Authentication token used for all requests to Discord's API.
	/// </summary>
	public required String Token { get; set; }

	/// <summary>
	/// Gateway: The amount of members a guild can have before the gateway will stop sending offline
	/// members.
	/// </summary>
	public Int32? LargeGuildThreshold { get; set; }

	/// <summary>
	/// Gateway: ID of the current shard.
	/// </summary>
	public Int32? ShardId { get; set; }

	/// <summary>
	/// Gateway: Total amount of shards for this bot.
	/// </summary>
	public Int32? ShardCount { get; set; }

	/// <summary>
	/// Gateway: The amount of consecutive missed heartbeats to cause Starnight to consider the connection
	/// zombied.
	/// </summary>
	public Int32? ZombieThreshold { get; set; }

	/// <summary>
	/// Gateway: The intents passed to the Discord gateway. See <seealso cref="StarnightIntents"/> for more
	/// details.
	/// </summary>
	public required StarnightIntents Intents { get; set; }

	public StarnightClientOptions Value => this;
}

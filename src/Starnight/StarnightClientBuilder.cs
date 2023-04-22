namespace Starnight;

using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Starnight.Caching.Extensions;
using Starnight.Caching.Providers;
using Starnight.Infrastructure.Logging;
using Starnight.Infrastructure.TransformationServices;
using Starnight.Internal;
using Starnight.Internal.Gateway;
using Starnight.Internal.Rest;

/// <summary>
/// Represents a convenient way to construct a Starnight client.
/// </summary>
public sealed class StarnightClientBuilder
{
	private readonly IServiceCollection services;
	private TimeSpan averageFirstRequestRetryDelay = TimeSpan.FromSeconds(2);
	private Int32 retryCount = 100;
	private Int32 ratelimitedRetryCount = 100;
	private String? token;
	private Int32 largeGuildThreshold = 50;
	private Int32 shardId = 0;
	private Int32 shardCount = 0;
	private Int32 zombieThreshold = 5;
	private StarnightIntents intents = StarnightIntents.None;
	private StarnightCacheKind cacheKind = StarnightCacheKind.Memory;

	public StarnightClientBuilder() : this(new ServiceCollection()) { }

	public StarnightClientBuilder
	(
		IServiceCollection services
	)
	{
		this.services = services;

		_ = this.services.AddStarnightRestResources()
			.AddStarnightCaching()
			.AddStarnightGateway();

		_ = this.services.AddSingleton<ICollectionTransformerService, CollectionTransformerService>();
	}

	/// <summary>
	/// Removes any existing registered loggers, and adds the Starnight default logger.
	/// </summary>
	/// <param name="minimumLogLevel">The minimum log level to go through.</param>
	/// <returns>The builder instance for chaining.</returns>
	public StarnightClientBuilder WithDefaultLogger
	(
		LogLevel minimumLogLevel
	)
	{
		_ = this.services.AddLogging
		(
			loggingBuilder => loggingBuilder.ClearProviders().AddProvider
			(
				new StarnightLoggerProvider
				(
					minimumLogLevel
				)
			)
		);

		return this;
	}

	/// <summary>
	/// Adds the Starnight default logger without clearing existing loggers.
	/// </summary>
	/// <param name="minimumLogLevel">The minimum log level to go through.</param>
	/// <returns>The builder instance for chaining.</returns>
	public StarnightClientBuilder AddDefaultLogger
	(
		LogLevel minimumLogLevel
	)
	{
		_ = this.services.AddLogging
		(
			loggingBuilder => loggingBuilder.AddProvider
			(
				new StarnightLoggerProvider
				(
					minimumLogLevel
				)
			)
		);

		return this;
	}

	/// <summary>
	/// Configures the registered logging scheme.
	/// </summary>
	/// <remarks>
	/// This method directly wraps the underlying service collection, and may have side effects not
	/// specific to Starnight.
	/// </remarks>
	/// <param name="configure">The configuration delegate.</param>
	/// <returns>The builder instance for chaining.</returns>
	public StarnightClientBuilder ConfigureLogging
	(
		Action<ILoggingBuilder> configure
	)
	{
		_ = this.services.AddLogging(configure);
		return this;
	}

	/// <summary>
	/// Configures the underlying service collection.
	/// </summary>
	/// <remarks>
	/// It is perfectly possible and reasonable to configure the service collection before creating the
	/// client builder from it; this method is provided for scenarios where such configuration would be
	/// inaccessible or for registrations that need to happen after the client builder is instantiated.
	/// </remarks>
	/// <param name="configure">The configuration delegate.</param>
	/// <returns>The builder instance for chaining.</returns>
	public StarnightClientBuilder ConfigureServices
	(
		Action<IServiceCollection> configure
	)
	{
		configure(this.services);
		return this;
	}

	/// <summary>
	/// Sets the average delay before a request is first retried.
	/// </summary>
	/// <returns>The builder instance for chaining</returns>
	public StarnightClientBuilder WithAverageFirstRequestRetryDelay
	(
		TimeSpan delay
	)
	{
		this.averageFirstRequestRetryDelay = delay;
		return this;
	}

	/// <summary>
	/// Sets the maximum retries for a request before it is dropped.
	/// </summary>
	/// <returns>The builder instance for chaining.</returns>
	public StarnightClientBuilder WithRetryCount
	(
		Int32 retryCount
	)
	{
		this.retryCount = retryCount;
		return this;
	}

	/// <summary>
	/// Sets the maximum retries for a request hitting a ratelimit before it is dropped.
	/// </summary>
	/// <returns>The builder instance for chaining.</returns>
	public StarnightClientBuilder WithRatelimitedRetryCount
	(
		Int32 ratelimitedRetryCount
	)
	{
		this.ratelimitedRetryCount = ratelimitedRetryCount;
		return this;
	}

	/// <summary>
	/// Sets the authentication token for the current instance.
	/// </summary>
	/// <returns>The builder instance for chaining.</returns>
	public StarnightClientBuilder WithToken
	(
		String token
	)
	{
		this.token = token;
		return this;
	}

	/// <summary>
	/// Sets the large guild threshold for the current instance.
	/// </summary>
	/// <returns>The builder instance for chaining.</returns>
	public StarnightClientBuilder WithLargeGuildThreshold
	(
		Int32 threshold
	)
	{
		this.largeGuildThreshold = threshold;
		return this;
	}

	/// <summary>
	/// Sets the shard information for the current instance.
	/// </summary>
	/// <param name="shardId">The ID of this shard in specific.</param>
	/// <param name="shardCount">The total shard count for this bot.</param>
	/// <returns>The builder instance for chaining.</returns>
	public StarnightClientBuilder WithShardInformation
	(
		Int32 shardId,
		Int32 shardCount
	)
	{
		this.shardId = shardId;
		this.shardCount = shardCount;
		return this;
	}

	/// <summary>
	/// Sets the zombie threshold for the current instance.
	/// </summary>
	/// <returns>The builder instance for chaining.</returns>
	public StarnightClientBuilder WithZombieThreshold
	(
		Int32 threshold
	)
	{
		this.zombieThreshold = threshold;
		return this;
	}

	/// <summary>
	/// Adds the specified intents to the current collection of intents.
	/// </summary>
	/// <returns>The builder instance for chaining.</returns>
	public StarnightClientBuilder AddIntents
	(
		StarnightIntents intents
	)
	{
		this.intents |= intents;
		return this;
	}

	/// <summary>
	/// Sets the cache kind for the current instance.
	/// </summary>
	/// <returns>The builder instance for chaining.</returns>
	public StarnightClientBuilder WithCacheKind
	(
		StarnightCacheKind cacheKind
	)
	{
		this.cacheKind = cacheKind;
		return this;
	}

	/// <summary>
	/// Returns whether the current instance can be built as-is, with all data valid.
	/// </summary>
	public Boolean Validate()
	{
#pragma warning disable IDE0046 // we don't actually want this to become a huge ternary
		if(this.token is null)
		{
			return false;
		}

		if(this.largeGuildThreshold is not <= 250 and >= 50)
		{
			return false;
		}

		if(this.shardId >= this.shardCount)
		{
			return false;
		}

		if(this.intents >= StarnightIntents.All)
		{
			return false;
		}

		return true;
#pragma warning restore IDE0046
	}

	/// <summary>
	/// Builds the Starnight client from the current builder.
	/// </summary>
	/// <exception cref="InvalidOperationException">Thrown if the builder was invalid.</exception>
	public StarnightClient Build()
	{
		if(!this.Validate())
		{
			throw new InvalidOperationException("The client builder was invalid.");
		}

		_ = this.services.AddStarnightRestClient
		(
			new RestClientOptions()
			{
				MedianFirstRequestRetryDelay = this.averageFirstRequestRetryDelay,
				RatelimitedRetryCount = this.ratelimitedRetryCount,
				RetryCount = this.retryCount
			},
			false
		)
		.Configure<TokenContainer>
		(
			xm => xm.Token = this.token!
		)
		.Configure<DiscordGatewayClientOptions>
		(
			xm =>
			{
				xm.ShardInformation = new Int32[]
				{
					this.shardId,
					this.shardCount
				};
				xm.LargeGuildThreshold = this.largeGuildThreshold;
				xm.ZombieThreshold = this.zombieThreshold;
				xm.Intents = (DiscordGatewayIntents)(Object)this.intents;
			}
		);

		if(this.cacheKind == StarnightCacheKind.Memory)
		{
			_ = this.services.AddMemoryCache()
				.AddStarnightMemoryCache();
		}

		_ = this.services.AddSingleton<StarnightClient>();

		return this.services.BuildServiceProvider()
			.GetRequiredService<StarnightClient>();
	}
}

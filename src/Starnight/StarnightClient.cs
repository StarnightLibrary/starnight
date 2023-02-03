namespace Starnight;

using Microsoft.Extensions.DependencyInjection;

using Starnight.Internal.Rest;

/// <summary>
/// The central, main client for any and all interactions with Discord.
/// </summary>
public partial class StarnightClient
{
	public IServiceCollection ServiceCollection { get; internal set; }

	public ServiceProvider Services { get; internal set; }

	public RestClient RestClient { get; internal set; }

	public StarnightClient(StarnightClientOptions options)
	{
		this.ServiceCollection = options.Services ?? new ServiceCollection();

		if(options.UseCustomLogger)
		{
			// todo: proper logging
			_ = this.ServiceCollection.AddLogging();
		}

		_ = this.ServiceCollection.AddMemoryCache()
			.AddStarnightRestClient(new()
			{
				MedianFirstRequestRetryDelay = options.MedianFirstRequestRetryDelay,
				RetryCount = options.RetryCount,
				RatelimitedRetryCount = options.RatelimitedRetryCount
			});

		this.Services = this.ServiceCollection!.BuildServiceProvider();

		this.RestClient = this.Services.GetRequiredService<RestClient>();
	}
}

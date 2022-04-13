namespace Starnight;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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

		// todo: register the starnight default logger if none is passed.
		_ = this.ServiceCollection.AddSingleton(typeof(ILogger), options.Logger!)
			.AddSingleton(typeof(RestClient));

		_ = this.ServiceCollection.AddMemoryCache();

		this.addRestClient(options);

		this.Services = this.ServiceCollection!.BuildServiceProvider();

		this.RestClient = this.Services.GetRequiredService<RestClient>();
	}
}

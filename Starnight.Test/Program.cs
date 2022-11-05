namespace Starnight.Test;

using System;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Starnight.Caching;
using Starnight.Internal.Gateway;
using Starnight.Internal.Rest;

public class Program
{
	public static async Task Main(String[] args)
	{
		IHostBuilder hostBuilder = Host
			.CreateDefaultBuilder(args)
			.UseConsoleLifetime();

		_ = hostBuilder.ConfigureServices(services =>
		{
			_ = services.AddMemoryCache().AddLogging(xm => xm.SetMinimumLevel(LogLevel.Trace));

			_ = services.AddStarnightMemoryCache();

			_ = services.AddStarnightRestClient(new RestClientOptions()
			{
				MedianFirstRequestRetryDelay = TimeSpan.FromSeconds(2),
				RatelimitedRetryCount = 2,
				RetryCount = 0,
				Token = " "
			});

			_ = services.Configure<DiscordGatewayClientOptions>(xm =>
			{
				xm.Token = " ";
				xm.Intents = DiscordGatewayIntents.Guilds;
			});
		});

		_ = hostBuilder.AddStarnightGateway();

		IHost host = hostBuilder.Build();

		await host.StartAsync();

		await host.WaitForShutdownAsync();

		Console.WriteLine("success");

		_ = Console.ReadKey();
	}
}

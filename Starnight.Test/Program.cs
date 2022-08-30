namespace Starnight.Test;

using System;

using Microsoft.Extensions.DependencyInjection;

using Starnight.Internal.Rest;

public class Program
{
	public static void Main(String[] args)
	{
		IServiceCollection collection = new ServiceCollection();
		_ = collection
			.AddMemoryCache()
			.AddLogging();

		try
		{
			_ = collection.AddStarnightRestClient(new RestClientOptions()
			{
				MedianFirstRequestRetryDelay = TimeSpan.FromSeconds(2),
				RatelimitedRetryCount = 2,
				RetryCount = 0,
				Token = "ksjdlfkjsd"
			});
		}
		catch(Exception e)
		{
			Console.WriteLine($"{e}: {e.Message}\n{e.StackTrace}");
		}

		Console.WriteLine("success");

		_ = Console.ReadKey();
	}
}

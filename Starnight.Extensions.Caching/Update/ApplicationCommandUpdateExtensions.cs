namespace Starnight.Extensions.Caching.Update;

using System;
using System.Threading.Tasks;

using Starnight.Caching.Abstractions;
using Starnight.Extensions.Caching.Update.Internal;
using Starnight.Internal.Entities.Interactions.ApplicationCommands;

internal static class ApplicationCommandUpdateExtensions
{
	public static async ValueTask<DiscordApplicationCommand> CacheApplicationCommandAsync
	(
		this ICacheService cache,
		DiscordApplicationCommand command
	)
	{
		String key = command.GenerateCacheKey();

		DiscordApplicationCommand? old = await cache.GetAsync<DiscordApplicationCommand>(key);

		if(old is null)
		{
			await cache.SetAsync(key, command);
		}
		else
		{
			command = UpdateApplicationCommandWrapper.UpdateDiscordApplicationCommand(old, command);

			await cache.SetAsync(key, command);
		}

		return command;
	}
}

namespace Starnight.Extensions.Caching.Update;

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
		DiscordApplicationCommand? old = await cache.GetAsync<DiscordApplicationCommand>(command.GenerateCacheKey());

		if(old is null)
		{
			await cache.SetAsync(command.GenerateCacheKey(), command);
		}
		else
		{
			command = UpdateApplicationCommandWrapper.UpdateDiscordApplicationCommand(old, command);

			await cache.SetAsync(command.GenerateCacheKey(), command);
		}

		return command;
	}
}

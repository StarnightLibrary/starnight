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

	public static async ValueTask<DiscordApplicationCommandPermissions> CacheApplicationCommandPermissionsAsync
	(
		this ICacheService cache,
		DiscordApplicationCommandPermissions permissions
	)
	{
		String key = permissions.GenerateCacheKey();

		DiscordApplicationCommandPermissions? old = await cache.GetAsync<DiscordApplicationCommandPermissions>(key);

		if(old is null)
		{
			await cache.SetAsync(key, permissions);
		}
		else
		{
			permissions = UpdateApplicationCommandWrapper.UpdateDiscordApplicationCommandPermissions(old, permissions);

			await cache.SetAsync(key, permissions);
		}

		return permissions;
	}
}

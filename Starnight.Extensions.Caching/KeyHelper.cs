namespace Starnight.Extensions.Caching;

using System;

using Starnight.Internal.Entities.Interactions.ApplicationCommands;

public static class KeyHelper
{
	public static String GenerateCacheKey
	(
		this DiscordApplicationCommand command
	)
		=> $"StarnightLibraryCache.ApplicationCommand.{command.Id}";
}

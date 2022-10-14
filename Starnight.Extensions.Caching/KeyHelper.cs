namespace Starnight.Extensions.Caching;

using System;

using Starnight.Internal.Entities.Interactions.ApplicationCommands;
using Starnight.Internal.Entities.Messages;

public static class KeyHelper
{
	public static String GenerateCacheKey
	(
		this DiscordApplicationCommand command
	)
		=> $"StarnightLibraryCache.ApplicationCommand.{command.Id}";

	public static String GenerateCacheKey
	(
		this DiscordMessage message
	)
		=> $"StarnightLibraryCache.Channel.{message.ChannelId}.Message.{message.Id}";

	public static String GenerateCacheKey
	(
		this DiscordApplicationCommandPermissions permissions
	)
		=> $"StarnightLibraryCache.ApplicationCommand.{permissions.ApplicationId}.Permissions.Guild.{permissions.GuildId}";
}

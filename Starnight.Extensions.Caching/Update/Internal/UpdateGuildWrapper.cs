namespace Starnight.Extensions.Caching.Update.Internal;

using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Entities.Guilds.AutoModeration;
using Starnight.Internal.Entities.Guilds.Invites;
using Starnight.SourceGenerators.Caching;

/// <summary>
/// Provides methods for updating guild methods
/// </summary>
internal partial class UpdateGuildWrapper
{
	[CacheUpdateMethod]
	public static partial DiscordEmoji UpdateDiscordEmoji
	(
		DiscordEmoji current,
		DiscordEmoji update
	);

	[CacheUpdateMethod]
	public static partial DiscordGuild UpdateDiscordGuild
	(
		DiscordGuild current,
		DiscordGuild update
	);

	[CacheUpdateMethod]
	public static partial DiscordGuildIntegration UpdateDiscordGuildIntegration
	(
		DiscordGuildIntegration current,
		DiscordGuildIntegration update
	);

	[CacheUpdateMethod]
	public static partial DiscordGuildMember UpdateDiscordGuildMember
	(
		DiscordGuildMember current,
		DiscordGuildMember update
	);

	[CacheUpdateMethod]
	public static partial DiscordGuildPreview UpdateDiscordGuildPreview
	(
		DiscordGuildPreview current,
		DiscordGuildPreview update
	);

	[CacheUpdateMethod]
	public static partial DiscordGuildTemplate UpdateDiscordGuildTemplate
	(
		DiscordGuildTemplate current,
		DiscordGuildTemplate update
	);

	[CacheUpdateMethod]
	public static partial DiscordRole UpdateDiscordRole
	(
		DiscordRole current,
		DiscordRole update
	);

	[CacheUpdateMethod]
	public static partial DiscordScheduledEvent UpdateDiscordScheduledEvent
	(
		DiscordScheduledEvent current,
		DiscordScheduledEvent update
	);

	[CacheUpdateMethod]
	public static partial DiscordScheduledEventUser UpdateDiscordScheduledEventUser
	(
		DiscordScheduledEventUser current,
		DiscordScheduledEventUser update
	);

	[CacheUpdateMethod]
	public static partial DiscordInvite UpdateDiscordInvite
	(
		DiscordInvite current,
		DiscordInvite update
	);

	[CacheUpdateMethod]
	public static partial DiscordAutoModerationRule UpdateDiscordAutoModerationRule
	(
		DiscordAutoModerationRule current,
		DiscordAutoModerationRule update
	);
}

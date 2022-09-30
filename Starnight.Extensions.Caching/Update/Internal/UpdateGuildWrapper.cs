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
	public partial DiscordEmoji UpdateDiscordEmoji
	(
		DiscordEmoji current,
		DiscordEmoji update
	);

	[CacheUpdateMethod]
	public partial DiscordGuild UpdateDiscordGuild
	(
		DiscordGuild current,
		DiscordGuild update
	);

	[CacheUpdateMethod]
	public partial DiscordGuildIntegration UpdateDiscordGuildIntegration
	(
		DiscordGuildIntegration current,
		DiscordGuildIntegration update
	);

	[CacheUpdateMethod]
	public partial DiscordGuildMember UpdateDiscordGuildMember
	(
		DiscordGuildMember current,
		DiscordGuildMember update
	);

	[CacheUpdateMethod]
	public partial DiscordGuildPreview UpdateDiscordGuildPreview
	(
		DiscordGuildPreview current,
		DiscordGuildPreview update
	);

	[CacheUpdateMethod]
	public partial DiscordGuildTemplate UpdateDiscordGuildTemplate
	(
		DiscordGuildTemplate current,
		DiscordGuildTemplate update
	);

	[CacheUpdateMethod]
	public partial DiscordRole UpdateDiscordRole
	(
		DiscordRole current,
		DiscordRole update
	);

	[CacheUpdateMethod]
	public partial DiscordScheduledEvent UpdateDiscordScheduledEvent
	(
		DiscordScheduledEvent current,
		DiscordScheduledEvent update
	);

	[CacheUpdateMethod]
	public partial DiscordScheduledEventUser UpdateDiscordScheduledEventUser
	(
		DiscordScheduledEventUser current,
		DiscordScheduledEventUser update
	);

	[CacheUpdateMethod]
	public partial DiscordInvite UpdateDiscordInvite
	(
		DiscordInvite current,
		DiscordInvite update
	);

	[CacheUpdateMethod]
	public partial DiscordAutoModerationRule UpdateDiscordAutoModerationRule
	(
		DiscordAutoModerationRule current,
		DiscordAutoModerationRule update
	);
}

namespace Starnight.Extensions.Caching.Update.Internal;

using Starnight.Internal.Entities.Guilds;
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
}

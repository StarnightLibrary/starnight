namespace Starnight.Extensions.Caching.Update.Internal;

using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Channels.Threads;
using Starnight.SourceGenerators.Caching;

internal partial class UpdateChannelWrapper
{
	[CacheUpdateMethod]
	public static partial DiscordChannel UpdateDiscordChannel
	(
		DiscordChannel current,
		DiscordChannel update
	);

	[CacheUpdateMethod]
	public static partial DiscordWebhook UpdateDiscordWebhook
	(
		DiscordWebhook current,
		DiscordWebhook update
	);

	[CacheUpdateMethod]
	public static partial DiscordThreadMember UpdateDiscordThreadMember
	(
		DiscordThreadMember current,
		DiscordThreadMember update
	);

	[CacheUpdateMethod]
	public static partial DiscordThreadMetadata UpdateDiscordThreadMetadata
	(
		DiscordThreadMetadata current,
		DiscordThreadMetadata update
	);
}

namespace Starnight.Extensions.Caching.Update.Internal;

using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Channels.Threads;
using Starnight.SourceGenerators.Caching;

internal partial class UpdateChannelWrapper
{
	[CacheUpdateMethod]
	public partial DiscordChannel UpdateDiscordChannel
	(
		DiscordChannel current,
		DiscordChannel update
	);

	[CacheUpdateMethod]
	public partial DiscordWebhook UpdateDiscordWebhook
	(
		DiscordWebhook current,
		DiscordWebhook update
	);

	[CacheUpdateMethod]
	public partial DiscordThreadMember UpdateDiscordThreadMember
	(
		DiscordThreadMember current,
		DiscordThreadMember update
	);

	[CacheUpdateMethod]
	public partial DiscordThreadMetadata UpdateDiscordThreadMetadata
	(
		DiscordThreadMetadata current,
		DiscordThreadMetadata update
	);
}

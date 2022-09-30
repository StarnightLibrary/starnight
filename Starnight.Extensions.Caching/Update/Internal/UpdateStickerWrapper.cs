namespace Starnight.Extensions.Caching.Update.Internal;

using Starnight.Internal.Entities.Stickers;
using Starnight.SourceGenerators.Caching;

internal partial class UpdateStickerWrapper
{
	[CacheUpdateMethod]
	public partial DiscordSticker UpdateDiscordSticker
	(
		DiscordSticker current,
		DiscordSticker update
	);
}

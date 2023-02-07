namespace Starnight.Caching.Shims;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Starnight.Caching.Services;
using Starnight.Internal.Entities.Stickers;
using Starnight.Internal.Rest.Payloads.Stickers;
using Starnight.Internal.Rest.Resources;
using Starnight.SourceGenerators.Shims;

[Shim<IDiscordStickerRestResource>]
public partial class CachingStickerRestResource : IDiscordStickerRestResource
{
	private readonly IDiscordStickerRestResource underlying;
	private readonly IStarnightCacheService cache;

	public CachingStickerRestResource
	(
		IDiscordStickerRestResource underlying,
		IStarnightCacheService cache
	)
	{
		this.underlying = underlying;
		this.cache = cache;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordSticker> GetGuildStickerAsync
	(
		Int64 guildId,
		Int64 stickerId,
		CancellationToken ct = default
	)
	{
		DiscordSticker sticker = await this.underlying.GetGuildStickerAsync
		(
			guildId,
			stickerId,
			ct
		);

		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetStickerKey
			(
				stickerId
			),
			sticker
		);

		return sticker;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordSticker> GetStickerAsync
	(
		Int64 stickerId,
		CancellationToken ct = default
	)
	{
		DiscordSticker sticker = await this.underlying.GetStickerAsync
		(
			stickerId,
			ct
		);

		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetStickerKey
			(
				stickerId
			),
			sticker
		);

		return sticker;
	}

	/// <inheritdoc/>
	public async ValueTask<IEnumerable<DiscordSticker>> ListGuildStickersAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	)
	{
		IEnumerable<DiscordSticker> stickers = await this.underlying.ListGuildStickersAsync
		(
			guildId,
			ct
		);

		await Parallel.ForEachAsync
		(
			stickers,
			async (sticker, _) =>
				await this.cache.CacheObjectAsync
				(
					KeyHelper.GetStickerKey
					(
						sticker.Id
					),
					sticker
				)
		);

		return stickers;
	}

	// redirects
	public partial ValueTask<DiscordSticker> CreateGuildStickerAsync(Int64 guildId, CreateGuildStickerRequestPayload payload, String? reason = null, CancellationToken ct = default);
	public partial ValueTask<Boolean> DeleteGuildStickerAsync(Int64 guildId, Int64 stickerId, String? reason = null, CancellationToken ct = default);
	public partial ValueTask<ListNitroStickerPacksResponsePayload> ListNitroStickerPacksAsync(CancellationToken ct = default);
	public partial ValueTask<DiscordSticker> ModifyGuildStickerAsync(Int64 guildId, Int64 stickerId, ModifyGuildStickerRequestPayload payload, String? reason = null, CancellationToken ct = default);
}

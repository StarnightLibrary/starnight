namespace Starnight.Internal.Entities.Stickers;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a sticker pack.
/// </summary>
public sealed record DiscordStickerPack : DiscordSnowflakeObject
{
	/// <summary>
	/// The stickers contained in this pack.
	/// </summary>
	[JsonPropertyName("stickers")]
	public required IEnumerable<DiscordSticker> Stickers { get; init; }

	/// <summary>
	/// The name of this sticker pack.
	/// </summary>
	[JsonPropertyName("name")]
	public required String Name { get; init; }

	/// <summary>
	/// Snowflake identifier of the pack SKU.
	/// </summary>
	[JsonPropertyName("sku_id")]
	public required Int64 SkuId { get; init; }

	/// <summary>
	/// Snowflake identifier of the cover sticker for this pack.
	/// </summary>
	[JsonPropertyName("cover_sticker_id")]
	public Optional<Int64> CoverStickerId { get; init; }

	/// <summary>
	/// This pack's description.
	/// </summary>
	[JsonPropertyName("description")]
	public required String Description { get; init; }

	/// <summary>
	/// Snowflake identifier of the banner asset for this pack.
	/// </summary>
	[JsonPropertyName("banner_asset_id")]
	public Optional<Int64> BannerAssetId { get; init; }
}

namespace Starnight.Internal.Entities.Sticker;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a sticker pack.
/// </summary>
public class DiscordStickerPack : DiscordSnowflakeObject
{
	/// <summary>
	/// The stickers contained in this pack.
	/// </summary>
	[JsonPropertyName("stickers")]
	public DiscordSticker[] Stickers { get; init; } = default!;

	/// <summary>
	/// The name of this sticker pack.
	/// </summary>
	[JsonPropertyName("name")]
	public String Name { get; init; } = default!;

	/// <summary>
	/// Snowflake identifier of the pack SKU.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("sku_id")]
	public Int64 SkuId { get; init; }

	/// <summary>
	/// Snowflake identifier of the cover sticker for this pack.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("cover_sticker_id")]
	public Int64? CoverStickerId { get; init; }

	/// <summary>
	/// This pack's description.
	/// </summary>
	[JsonPropertyName("description")]
	public String Description { get; init; } = default!;

	/// <summary>
	/// Snowflake identifier of the banner asset for this pack.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("banner_asset_id")]
	public Int64? BannerAssetId { get; init; }
}

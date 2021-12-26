namespace Starnight.Internal.Entities.Guild;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.User;

/// <summary>
/// Represents a discord sticker.
/// </summary>
public class DiscordSticker : DiscordSnowflakeObject
{
	/// <summary>
	/// ID of the sticker pack this sticker is from.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("pack_id")]
	public Int64? PackId { get; init; }

	/// <summary>
	/// Name of this sticker.
	/// </summary>
	[JsonPropertyName("name")]
	public String Name { get; init; } = default!;

	/// <summary>
	/// Description for this sticker.
	/// </summary>
	[JsonPropertyName("description")]
	public String? Description { get; init; }

	/// <summary>
	/// Comma-separated list of tags for this sticker, max. 200 characters.
	/// </summary>
	[JsonPropertyName("tags")]
	public String Tags { get; init; } = default!;

	/// <summary>
	/// Empty string.
	/// </summary>
	[JsonPropertyName("asset")]
	[Obsolete("Empty string, no longer in use.", DiagnosticId = "SE0003")]
	public String Asset { get; init; } = null!;

	/// <summary>
	/// Sticker type.
	/// </summary>
	[JsonPropertyName("type")]
	public DiscordStickerType Type { get; init; }

	/// <summary>
	/// Sticker format type.
	/// </summary>
	[JsonPropertyName("format_type")]
	public DiscordStickerFormatType FormatType { get; init; }

	/// <summary>
	/// Whether this sticker is available for use.
	/// </summary>
	[JsonPropertyName("available")]
	public Boolean? Available { get; init; }

	/// <summary>
	/// Snowflake identifier of the guild that owns this sticker.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("guild_id")]
	public Int64? GuildId { get; init; }

	/// <summary>
	/// Creator of this sticker.
	/// </summary>
	[JsonPropertyName("user")]
	public DiscordUser? User { get; init; }

	/// <summary>
	/// This standard sticker's sort order within its pack.
	/// </summary>
	[JsonPropertyName("sort_value")]
	public Int32? SortValue { get; init; }
}

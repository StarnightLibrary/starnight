namespace Starnight.Internal.Entities.Stickers;

using System;
using System.Text.Json.Serialization;

using Starnight.Entities;
using Starnight.Internal.Entities.Users;

/// <summary>
/// Represents a discord sticker.
/// </summary>
public sealed record DiscordSticker : DiscordSnowflakeObject
{
	/// <summary>
	/// ID of the sticker pack this sticker is from.
	/// </summary>
	[JsonPropertyName("pack_id")]
	public Optional<Int64> PackId { get; init; }

	/// <summary>
	/// Name of this sticker.
	/// </summary>
	[JsonPropertyName("name")]
	public required String Name { get; init; }

	/// <summary>
	/// Description for this sticker.
	/// </summary>
	[JsonPropertyName("description")]
	public String? Description { get; init; }

	/// <summary>
	/// Comma-separated list of tags for this sticker, max. 200 characters.
	/// </summary>
	[JsonPropertyName("tags")]
	public required String Tags { get; init; }

	/// <summary>
	/// Sticker type.
	/// </summary>
	[JsonPropertyName("type")]
	public required DiscordStickerType Type { get; init; }

	/// <summary>
	/// Sticker format type.
	/// </summary>
	[JsonPropertyName("format_type")]
	public required DiscordStickerFormatType FormatType { get; init; }

	/// <summary>
	/// Whether this sticker is available for use.
	/// </summary>
	[JsonPropertyName("available")]
	public Optional<Boolean> Available { get; init; }

	/// <summary>
	/// Snowflake identifier of the guild that owns this sticker.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public Optional<Int64> GuildId { get; init; }

	/// <summary>
	/// Creator of this sticker.
	/// </summary>
	[JsonPropertyName("user")]
	public Optional<DiscordUser> User { get; init; }

	/// <summary>
	/// This standard sticker's sort order within its pack.
	/// </summary>
	[JsonPropertyName("sort_value")]
	public Optional<Int32> SortValue { get; init; }
}

namespace Starnight.Internal.Entities.Stickers;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Messages;

/// <summary>
/// Represents a stripped-down sticker object, used in <see cref="DiscordMessage"/>s
/// </summary>
public sealed record DiscordMessageSticker : DiscordSnowflakeObject
{
	/// <summary>
	/// Name of this sticker.
	/// </summary>
	[JsonPropertyName("name")]
	public required String Name { get; init; }

	/// <summary>
	/// Format type of this sticker.
	/// </summary>
	[JsonPropertyName("format_type")]
	public required DiscordStickerFormatType Format { get; init; }
}

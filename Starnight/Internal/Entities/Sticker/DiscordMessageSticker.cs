using Starnight.Internal.Entities.Message;

namespace Starnight.Internal.Entities.Sticker;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a stripped-down sticker object, used in <see cref="DiscordMessage"/>s
/// </summary>
public class DiscordMessageSticker : DiscordSnowflakeObject
{
	/// <summary>
	/// Name of this sticker.
	/// </summary>
	[JsonPropertyName("name")]
	public String Name { get; init; } = default!;

	/// <summary>
	/// Format type of this sticker.
	/// </summary>
	[JsonPropertyName("format_type")]
	public DiscordStickerFormatType Format { get; init; }
}

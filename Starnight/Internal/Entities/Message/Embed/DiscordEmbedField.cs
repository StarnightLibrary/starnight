namespace Starnight.Internal.Entities.Message.Embed;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents an embed field.
/// </summary>
public class DiscordEmbedField
{
	/// <summary>
	/// Field title.
	/// </summary>
	[JsonPropertyName("name")]
	public String Name { get; init; } = default!;

	/// <summary>
	/// Field text.
	/// </summary>
	[JsonPropertyName("value")]
	public String Value { get; init; } = default!;

	/// <summary>
	/// Whether this field should be displayed inline.
	/// </summary>
	[JsonPropertyName("inline")]
	public Boolean? Inline { get; init; }
}

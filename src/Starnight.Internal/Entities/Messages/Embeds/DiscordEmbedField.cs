namespace Starnight.Internal.Entities.Messages.Embeds;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents an embed field.
/// </summary>
public sealed record DiscordEmbedField
{
	/// <summary>
	/// Field title.
	/// </summary>
	[JsonPropertyName("name")]
	public required String Name { get; init; }

	/// <summary>
	/// Field text.
	/// </summary>
	[JsonPropertyName("value")]
	public required String Value { get; init; }

	/// <summary>
	/// Whether this field should be displayed inline.
	/// </summary>
	[JsonPropertyName("inline")]
	public Optional<Boolean> Inline { get; init; }
}

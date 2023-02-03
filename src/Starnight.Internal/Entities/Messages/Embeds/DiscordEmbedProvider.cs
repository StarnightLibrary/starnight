namespace Starnight.Internal.Entities.Messages.Embeds;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents an embed provider.
/// </summary>
public sealed record DiscordEmbedProvider
{
	/// <summary>
	/// Provider name.
	/// </summary>
	[JsonPropertyName("name")]
	public Optional<String> Name { get; init; }

	/// <summary>
	/// Provider url.
	/// </summary>
	[JsonPropertyName("url")]
	public Optional<String> Url { get; init; }
}

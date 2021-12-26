namespace Starnight.Internal.Entities.Message.Embed;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents an embed provider.
/// </summary>
public class DiscordEmbedProvider
{
	/// <summary>
	/// Provider name.
	/// </summary>
	[JsonPropertyName("name")]
	public String? Name { get; init; }

	/// <summary>
	/// Provider url.
	/// </summary>
	[JsonPropertyName("url")]
	public String? Url { get; init; }
}

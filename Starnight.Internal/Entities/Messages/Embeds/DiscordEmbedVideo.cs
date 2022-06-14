namespace Starnight.Internal.Entities.Messages.Embeds;

using System.Text.Json.Serialization;
using System;

/// <summary>
/// Represents a video played in a discord embed.
/// </summary>
public record DiscordEmbedVideo
{
	/// <summary>
	/// Video source url.
	/// </summary>
	[JsonPropertyName("url")]
	public String Url { get; init; } = default!;

	/// <summary>
	/// A proxied source url for this video.
	/// </summary>
	[JsonPropertyName("proxy_url")]
	public String? ProxiedUrl { get; init; }

	/// <summary>
	/// Video height in pixels.
	/// </summary>
	[JsonPropertyName("height")]
	public Int32? Height { get; init; }

	/// <summary>
	/// Video width in pixels.
	/// </summary>
	[JsonPropertyName("width")]
	public Int32? Width { get; init; }
}

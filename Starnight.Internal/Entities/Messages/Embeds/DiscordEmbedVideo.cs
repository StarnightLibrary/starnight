namespace Starnight.Internal.Entities.Messages.Embeds;

using System.Text.Json.Serialization;
using System;

/// <summary>
/// Represents a video played in a discord embed.
/// </summary>
public sealed record DiscordEmbedVideo
{
	/// <summary>
	/// Video source url.
	/// </summary>
	[JsonPropertyName("url")]
	public required String Url { get; init; } 

	/// <summary>
	/// A proxied source url for this video.
	/// </summary>
	[JsonPropertyName("proxy_url")]
	public Optional<String> ProxiedUrl { get; init; }

	/// <summary>
	/// Video height in pixels.
	/// </summary>
	[JsonPropertyName("height")]
	public Optional<Int32> Height { get; init; }

	/// <summary>
	/// Video width in pixels.
	/// </summary>
	[JsonPropertyName("width")]
	public Optional<Int32> Width { get; init; }
}

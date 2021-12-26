namespace Starnight.Internal.Entities.Message.Embed;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the thumbnail of a discord embed.
/// </summary>
public class DiscordEmbedThumbnail
{
	/// <summary>
	/// Thumbnail source url. This only supports <c>http://</c>, <c>https://</c> and <c>attachment://</c>
	/// </summary>
	[JsonPropertyName("url")]
	public String Url { get; init; } = default!;

	/// <summary>
	/// A proxied source url for this thumbnail.
	/// </summary>
	[JsonPropertyName("proxy_url")]
	public String? ProxiedUrl { get; init; }

	/// <summary>
	/// Thumbnail height in pixels.
	/// </summary>
	[JsonPropertyName("height")]
	public Int32? Height { get; init; }

	/// <summary>
	/// Thumbnail width in pixels.
	/// </summary>
	[JsonPropertyName("width")]
	public Int32? Width { get; init; }
}

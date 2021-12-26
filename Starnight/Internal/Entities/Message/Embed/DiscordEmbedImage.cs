namespace Starnight.Internal.Entities.Message.Embed;

using System.Text.Json.Serialization;
using System;

/// <summary>
/// Represents an image displayed in an embed.
/// </summary>
public class DiscordEmbedImage
{
	/// <summary>
	/// Image source url.
	/// </summary>
	[JsonPropertyName("url")]
	public String Url { get; init; } = default!;

	/// <summary>
	/// A proxied source url for this image.
	/// </summary>
	[JsonPropertyName("proxy_url")]
	public String? ProxiedUrl { get; init; }

	/// <summary>
	/// Image height in pixels.
	/// </summary>
	[JsonPropertyName("height")]
	public Int32? Height { get; init; }

	/// <summary>
	/// Image width in pixels.
	/// </summary>
	[JsonPropertyName("width")]
	public Int32? Width { get; init; }
}

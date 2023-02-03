namespace Starnight.Internal.Entities.Messages.Embeds;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the thumbnail of a discord embed.
/// </summary>
public sealed record DiscordEmbedThumbnail
{
	/// <summary>
	/// Thumbnail source url. This only supports <c>http://</c>, <c>https://</c> and <c>attachment://</c>
	/// </summary>
	[JsonPropertyName("url")]
	public required String Url { get; init; }

	/// <summary>
	/// A proxied source url for this thumbnail.
	/// </summary>
	[JsonPropertyName("proxy_url")]
	public Optional<String> ProxiedUrl { get; init; }

	/// <summary>
	/// Thumbnail height in pixels.
	/// </summary>
	[JsonPropertyName("height")]
	public Optional<Int32> Height { get; init; }

	/// <summary>
	/// Thumbnail width in pixels.
	/// </summary>
	[JsonPropertyName("width")]
	public Optional<Int32> Width { get; init; }
}

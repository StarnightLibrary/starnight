namespace Starnight.Internal.Entities.Messages.Embeds;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a discord embed.
/// </summary>
public sealed record DiscordEmbed
{
	/// <summary>
	/// The embed title.
	/// </summary>
	[JsonPropertyName("title")]
	public Optional<String> Title { get; init; }

	/// <summary>
	/// The embed description.
	/// </summary>
	[JsonPropertyName("description")]
	public Optional<String> Description { get; init; }

	/// <summary>
	/// The embed URL.
	/// </summary>
	[JsonPropertyName("url")]
	public Optional<String> Url { get; init; }

	/// <summary>
	/// The embed content timestamp.
	/// </summary>
	[JsonPropertyName("timestamp")]
	public Optional<DateTimeOffset> Timestamp { get; init; }

	/// <summary>
	/// The color code of this embed.
	/// </summary>
	[JsonPropertyName("color")]
	public Optional<Int32> Color { get; init; }

	/// <summary>
	/// The embed footer.
	/// </summary>
	[JsonPropertyName("footer")]
	public Optional<DiscordEmbedFooter> Footer { get; init; }

	/// <summary>
	/// The embed image.
	/// </summary>
	[JsonPropertyName("image")]
	public Optional<DiscordEmbedImage> Image { get; init; }

	/// <summary>
	/// The embed thumbnail.
	/// </summary>
	[JsonPropertyName("thumbnail")]
	public Optional<DiscordEmbedThumbnail> Thumbnail { get; init; }

	/// <summary>
	/// The embed video.
	/// </summary>
	[JsonPropertyName("video")]
	public Optional<DiscordEmbedVideo> Video { get; init; }

	/// <summary>
	/// The embed provider.
	/// </summary>
	[JsonPropertyName("provider")]
	public Optional<DiscordEmbedProvider> Provider { get; init; }

	/// <summary>
	/// The embed author.
	/// </summary>
	[JsonPropertyName("author")]
	public Optional<DiscordEmbedAuthor> Author { get; init; }

	/// <summary>
	/// The embed fields.
	/// </summary>
	[JsonPropertyName("fields")]
	public Optional<IEnumerable<DiscordEmbedField>> Fields { get; init; }
}

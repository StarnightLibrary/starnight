namespace Starnight.Internal.Entities.Message.Embed;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a discord embed.
/// </summary>
public class DiscordEmbed
{
	/// <summary>
	/// The embed title.
	/// </summary>
	[JsonPropertyName("title")]
	public String? Title { get; init; }

	/// <summary>
	/// The embed type. This property is obsolete and should not be used.
	/// </summary>
	[JsonPropertyName("type")]
	[Obsolete("Obsolete as per discord API specification, might be removed in the future", DiagnosticId = "SE0001")]
	public String? Type { get; init; }

	/// <summary>
	/// The embed description.
	/// </summary>
	[JsonPropertyName("description")]
	public String? Description { get; init; }

	/// <summary>
	/// The embed URL.
	/// </summary>
	[JsonPropertyName("url")]
	public String? Url { get; init; }

	/// <summary>
	/// The embed content timestamp.
	/// </summary>
	[JsonPropertyName("timestamp")]
	public DateTime? Timestamp { get; init; }

	/// <summary>
	/// The color code of this embed.
	/// </summary>
	[JsonPropertyName("color")]
	public Int32? Color { get; init; }

	/// <summary>
	/// The embed footer.
	/// </summary>
	[JsonPropertyName("footer")]
	public DiscordEmbedFooter? Footer { get; init; }

	/// <summary>
	/// The embed image.
	/// </summary>
	[JsonPropertyName("image")]
	public DiscordEmbedImage? Image { get; init; }

	/// <summary>
	/// The embed thumbnail.
	/// </summary>
	[JsonPropertyName("thumbnail")]
	public DiscordEmbedThumbnail? Thumbnail { get; init; }

	/// <summary>
	/// The embed video.
	/// </summary>
	[JsonPropertyName("video")]
	public DiscordEmbedVideo? Video { get; init; }

	/// <summary>
	/// The embed provider.
	/// </summary>
	[JsonPropertyName("provider")]
	public DiscordEmbedProvider? Provider { get; init; }

	/// <summary>
	/// The embed author.
	/// </summary>
	[JsonPropertyName("author")]
	public DiscordEmbedAuthor? Author { get; init; }

	/// <summary>
	/// The embed fields.
	/// </summary>
	[JsonPropertyName("fields")]
	public DiscordEmbedField[]? Fields { get; init; }
}

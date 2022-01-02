namespace Starnight.Internal.Gateway.Objects.User.Activity;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the assets used to display activity information.
/// </summary>
public record DiscordActivityAssets
{
	/// <summary>
	/// The ID - usually a snowflake ID - for the large asset of the activity.
	/// </summary>
	[JsonPropertyName("large_image")]
	public String? LargeImage { get; init; }

	/// <summary>
	/// Text displayed when hovering over the <see cref="LargeImage"/> of the activity.
	/// </summary>
	[JsonPropertyName("large_text")]
	public String? LargeImageHoverText { get; init; }

	/// <summary>
	/// The ID - usually a snowflake ID - for the small asset of the activity.
	/// </summary>
	[JsonPropertyName("small_image")]
	public String? SmallImage { get; init; }

	/// <summary>
	/// Text displayed when hovering over the <see cref="SmallImage"/> of the activity.
	/// </summary>
	[JsonPropertyName("small_text")]
	public String? SmallImageHoverText { get; init; }
}

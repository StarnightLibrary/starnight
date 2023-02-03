namespace Starnight.Internal.Entities.Users.Activities;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the assets used to display activity information.
/// </summary>
public sealed record DiscordActivityAssets
{
	/// <summary>
	/// The ID - usually a snowflake ID - for the large asset of the activity.
	/// </summary>
	[JsonPropertyName("large_image")]
	public Optional<String> LargeImage { get; init; }

	/// <summary>
	/// Text displayed when hovering over the <see cref="LargeImage"/> of the activity.
	/// </summary>
	[JsonPropertyName("large_text")]
	public Optional<String> LargeImageHoverText { get; init; }

	/// <summary>
	/// The ID - usually a snowflake ID - for the small asset of the activity.
	/// </summary>
	[JsonPropertyName("small_image")]
	public Optional<String> SmallImage { get; init; }

	/// <summary>
	/// Text displayed when hovering over the <see cref="SmallImage"/> of the activity.
	/// </summary>
	[JsonPropertyName("small_text")]
	public Optional<String> SmallImageHoverText { get; init; }
}

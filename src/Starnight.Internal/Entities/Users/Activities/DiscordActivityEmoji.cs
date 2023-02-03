namespace Starnight.Internal.Entities.Users.Activities;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents an emoji displayed as part of an activity.
/// </summary>
public sealed record DiscordActivityEmoji
{
	/// <summary>
	/// The name of the emoji.
	/// </summary>
	[JsonPropertyName("name")]
	public required String Name { get; init; }

	/// <summary>
	/// The ID of the emoji.
	/// </summary>
	[JsonPropertyName("id")]
	public Optional<Int64> Id { get; init; }

	/// <summary>
	/// Whether this emoji is animated.
	/// </summary>
	[JsonPropertyName("animated")]
	public Optional<Boolean> Animated { get; init; }
}

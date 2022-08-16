namespace Starnight.Internal.Entities.Users.Activities;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guilds;

/// <summary>
/// Represents a discord activity object.
/// </summary>
public sealed record DiscordActivity
{
	/// <summary>
	/// The activity name.
	/// </summary>
	[JsonPropertyName("name")]
	public required String Name { get; init; }

	/// <summary>
	/// The activity type.
	/// </summary>
	[JsonPropertyName("type")]
	public required DiscordActivityType Type { get; init; }

	/// <summary>
	/// The stream URL if <see cref="Type"/> is <see cref="DiscordActivityType.Streaming"/>.
	/// </summary>
	[JsonPropertyName("url")]
	public Optional<String?> Url { get; init; }

	/// <summary>
	/// Unix timestamp in milliseconds of when the activity was added to the user's session.
	/// </summary>
	[JsonPropertyName("created_at")]
	public required Int32 CreatedAt { get; init; }

	/// <summary>
	/// Unix timestamps in milliseconds for start and end of this activity.
	/// </summary>
	[JsonPropertyName("timestamps")]
	public Optional<DiscordActivityTimestamps> Timestamps { get; init; }

	/// <summary>
	/// Application ID for this game.
	/// </summary>
	[JsonPropertyName("application_id")]
	public Optional<Int64> ApplicationId { get; init; }

	/// <summary>
	/// Details about the current activity.
	/// </summary>
	[JsonPropertyName("details")]
	public Optional<String?> Details { get; init; }

	/// <summary>
	/// State of the current activity.
	/// </summary>
	[JsonPropertyName("state")]
	public Optional<String?> State { get; init; }

	/// <summary>
	/// The emoji used for a custom status.
	/// </summary>
	[JsonPropertyName("emoji")]
	public Optional<DiscordActivityEmoji?> Emoji { get; init; }

	/// <summary>
	/// Information about the party associated with this activity.
	/// </summary>
	[JsonPropertyName("party")]
	public Optional<DiscordActivityParty> Party { get; init; }

	/// <summary>
	/// Images for the rich presence and their hover texts.
	/// </summary>
	[JsonPropertyName("assets")]
	public Optional<DiscordActivityAssets> Assets { get; init; }

	/// <summary>
	/// Secrets for RPC joining and spectating.
	/// </summary>
	[JsonPropertyName("secrets")]
	public Optional<DiscordActivitySecrets> Secrets { get; init; }

	/// <summary>
	/// Whether the activity is an instanced game session.
	/// </summary>
	[JsonPropertyName("instance")]
	public Optional<Boolean> InstancedGameSession { get; init; }

	/// <summary>
	/// Activity flags describing what this payload includes.
	/// </summary>
	[JsonPropertyName("flags")]
	public Optional<DiscordActivityFlags> Flags { get; init; }

	/// <summary>
	/// Up to two custom buttons shown in the RPC display.
	/// </summary>
	[JsonPropertyName("buttons")]
	public Optional<IEnumerable<DiscordActivityButton>> Buttons { get; init; }
}

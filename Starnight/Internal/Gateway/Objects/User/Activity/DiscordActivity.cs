namespace Starnight.Internal.Gateway.Objects.User.Activity;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guilds;

/// <summary>
/// Represents a discord activity object.
/// </summary>
public record DiscordActivity
{
	/// <summary>
	/// The activity name.
	/// </summary>
	[JsonPropertyName("name")]
	public String Name { get; init; } = default!;

	/// <summary>
	/// The activity type.
	/// </summary>
	[JsonPropertyName("type")]
	public DiscordActivityType Type { get; init; }

	/// <summary>
	/// The stream URL if <see cref="Type"/> is <see cref="DiscordActivityType.Streaming"/>.
	/// </summary>
	[JsonPropertyName("url")]
	public String? Url { get; init; }

	/// <summary>
	/// Unix timestamp in milliseconds of when the activity was added to the user's session.
	/// </summary>
	[JsonPropertyName("created_at")]
	public Int32 CreatedAt { get; init; }

	/// <summary>
	/// Unix timestamps in milliseconds for start and end of this activity.
	/// </summary>
	[JsonPropertyName("timestamps")]
	public DiscordActivityTimestamps? Timestamps { get; init; }

	/// <summary>
	/// Application ID for this game.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("application_id")]
	public Int64? ApplicationId { get; init; }

	/// <summary>
	/// Details about the current activity.
	/// </summary>
	[JsonPropertyName("details")]
	public String? Details { get; init; }

	/// <summary>
	/// State of the current activity.
	/// </summary>
	[JsonPropertyName("state")]
	public String? State { get; init; }

	/// <summary>
	/// The emoji used for a custom status.
	/// </summary>
	[JsonPropertyName("emoji")]
	public DiscordEmoji? Emoji { get; init; }

	/// <summary>
	/// Information about the party associated with this activity.
	/// </summary>
	[JsonPropertyName("party")]
	public DiscordActivityParty? Party { get; init; }

	/// <summary>
	/// Images for the rich presence and their hover texts.
	/// </summary>
	[JsonPropertyName("assets")]
	public DiscordActivityAssets? Assets { get; init; }

	/// <summary>
	/// Secrets for RPC joining and spectating.
	/// </summary>
	[JsonPropertyName("secrets")]
	public DiscordActivitySecrets? Secrets { get; init; }

	/// <summary>
	/// Whether the activity is an instanced game session.
	/// </summary>
	[JsonPropertyName("instance")]
	public Boolean? InstancedGameSession { get; init; }

	/// <summary>
	/// Activity flags describing what this payload includes.
	/// </summary>
	[JsonPropertyName("flags")]
	public DiscordActivityFlags? Flags { get; init; }

	/// <summary>
	/// Up to two custom buttons shown in the RPC display.
	/// </summary>
	[JsonPropertyName("buttons")]
	public IEnumerable<DiscordActivityButton>? Buttons { get; init; }
}

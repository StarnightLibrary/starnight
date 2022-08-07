namespace Starnight.Internal.Entities.Guilds;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Users;

/// <summary>
/// Represents a scheduled event.
/// </summary>
public sealed record DiscordScheduledEvent : DiscordSnowflakeObject
{
	/// <summary>
	/// Snowflake ID of the guild this scheduled event belongs to.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public required Int64 GuildId { get; init; }

	/// <summary>
	/// Snowflake ID of the channel in which this scheduled event will be hosted.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public Int64? ChannelId { get; init; }

	/// <summary>
	/// Snowflake ID of the user who created this scheduled event.
	/// </summary>
	[JsonPropertyName("creator_id")]
	public Optional<Int64?> CreatorId { get; init; }

	/// <summary>
	/// Name of this scheduled event, 1 - 100 characters.
	/// </summary>
	[JsonPropertyName("name")]
	public required String Name { get; init; }

	/// <summary>
	/// Description for this scheduled event, 1 - 1000 characters.
	/// </summary>
	[JsonPropertyName("description")]
	public Optional<String?> Description { get; init; }

	/// <summary>
	/// The time at which this event is scheduled to start.
	/// </summary>
	[JsonPropertyName("scheduled_start_time")]
	public required DateTimeOffset StartTime { get; init; }

	/// <summary>
	/// The time at which this event is scheduled to end.
	/// </summary>
	[JsonPropertyName("scheduled_end_time")]
	public required Optional<DateTimeOffset> EndTime { get; init; }

	/// <summary>
	/// Privacy level for this scheduled event.
	/// </summary>
	[JsonPropertyName("privacy_level")]
	public required DiscordScheduledEventPrivacyLevel PrivacyLevel { get; init; }

	/// <summary>
	/// Status of this scheduled event.
	/// </summary>
	[JsonPropertyName("status")]
	public required DiscordScheduledEventStatus Status { get; init; }

	/// <summary>
	/// Type of this scheduled event.
	/// </summary>
	[JsonPropertyName("entity_type")]
	public required DiscordScheduledEventType Type { get; init; }

	/// <summary>
	/// Entity ID associated witha guild scheduled event.
	/// </summary>
	[JsonPropertyName("entity_id")]
	public Int64? EntityId { get; init; }

	/// <summary>
	/// Metadata associated with this scheduled event.
	/// </summary>
	[JsonPropertyName("entity_metadata")]
	public DiscordScheduledEventMetadata? Metadata { get; init; }

	/// <summary>
	/// Creator of this scheduled event.
	/// </summary>
	[JsonPropertyName("creator")]
	public Optional<DiscordUser> Creator { get; init; }

	/// <summary>
	/// The amount of users who subscribed to this scheduled event.
	/// </summary>
	[JsonPropertyName("user_count")]
	public Optional<Int32> UserCount { get; init; }

	/// <summary>
	/// The cover image hash for this scheduled event.
	/// </summary>
	[JsonPropertyName("image")]
	public Optional<String?> CoverHash { get; init; }
}

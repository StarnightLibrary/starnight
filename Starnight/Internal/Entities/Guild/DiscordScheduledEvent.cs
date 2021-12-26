namespace Starnight.Internal.Entities.Guild;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.User;

/// <summary>
/// Represents a scheduled event.
/// </summary>
public class DiscordScheduledEvent : DiscordSnowflakeObject
{
	/// <summary>
	/// Snowflake ID of the guild this scheduled event belongs to.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("guild_id")]
	public Int64 GuildId { get; init; }

	/// <summary>
	/// Snowflake ID of the channel in which this scheduled event will be hosted.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("channel_id")]
	public Int64? ChannelId { get; init; }

	/// <summary>
	/// Snowflake ID of the user who created this scheduled event.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("creator_id")]
	public Int64? CreatorId { get; init; }

	/// <summary>
	/// Name of this scheduled event, 1 - 100 characters.
	/// </summary>
	[JsonPropertyName("name")]
	public String Name { get; init; } = null!;

	/// <summary>
	/// Description for this scheduled event, 1 - 1000 characters.
	/// </summary>
	[JsonPropertyName("description")]
	public String? Description { get; init; }

	/// <summary>
	/// The time at which this event is scheduled to start.
	/// </summary>
	[JsonPropertyName("scheduled_start_time")]
	public DateTime StartTime { get; init; }

	/// <summary>
	/// The time at which this event is scheduled to end.
	/// </summary>
	[JsonPropertyName("scheduled_end_time")]
	public DateTime? EndTime { get; init; }

	/// <summary>
	/// Privacy level for this scheduled event.
	/// </summary>
	[JsonPropertyName("privacy_level")]
	public DiscordScheduledEventPrivacyLevel PrivacyLevel { get; init; }

	/// <summary>
	/// Status of this scheduled event.
	/// </summary>
	[JsonPropertyName("status")]
	public DiscordScheduledEventStatus Status { get; init; }

	/// <summary>
	/// Type of this scheduled event.
	/// </summary>
	[JsonPropertyName("entity_type")]
	public DiscordScheduledEventType Type { get; init; }

	/// <summary>
	/// Entity ID associated witha guild scheduled event.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
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
	public DiscordUser? Creator { get; init; }

	/// <summary>
	/// The amount of users who subscribed to this scheduled event.
	/// </summary>
	[JsonPropertyName("user_count")]
	public Int32? UserCount { get; init; }
}

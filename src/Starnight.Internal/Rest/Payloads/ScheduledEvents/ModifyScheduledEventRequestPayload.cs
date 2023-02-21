namespace Starnight.Internal.Rest.Payloads.ScheduledEvents;

using System;
using System.Text.Json.Serialization;

using Starnight.Entities;
using Starnight.Internal.Entities.Guilds;

/// <summary>
/// Represents a request payload to POST /guilds/:guild_id/scheduled-events
/// </summary>
public sealed record ModifyScheduledEventRequestPayload
{
	/// <summary>
	/// The channel ID of the scheduled event.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public Optional<Int64?> ChannelId { get; init; }

	/// <summary>
	/// Represents metadata about the scheduled event.
	/// </summary>
	[JsonPropertyName("entity_metadata")]
	public Optional<DiscordScheduledEventMetadata?> Metadata { get; init; }

	/// <summary>
	/// Name of the scheduled event.
	/// </summary>
	[JsonPropertyName("name")]
	public Optional<String> Name { get; init; }

	/// <summary>
	/// Privacy level for this scheduled event.
	/// </summary>
	[JsonPropertyName("privacy_level")]
	public Optional<DiscordScheduledEventPrivacyLevel> PrivacyLevel { get; init; }

	/// <summary>
	/// Indicates the time at which this event is scheduled to start.
	/// </summary>
	[JsonPropertyName("scheduled_start_time")]
	public Optional<DateTimeOffset> ScheduledStartTime { get; init; }

	/// <summary>
	/// Indicates the time at which this event is scheduled to end.
	/// </summary>
	[JsonPropertyName("scheduled_end_time")]
	public Optional<DateTimeOffset> ScheduledEndTime { get; init; }

	/// <summary>
	/// Description for this scheduled event.
	/// </summary>
	[JsonPropertyName("description")]
	public Optional<String?> Description { get; init; }

	/// <summary>
	/// The event type of this event.
	/// </summary>
	[JsonPropertyName("entity_type")]
	public Optional<DiscordScheduledEventType> EventType { get; init; }

	/// <summary>
	/// The new status of this event.
	/// </summary>
	[JsonPropertyName("status")]
	public Optional<DiscordScheduledEventStatus> Status { get; init; }

	/// <summary>
	/// Image data representing the cover image of this scheduled event.
	/// </summary>
	[JsonPropertyName("image")]
	public Optional<String> Image { get; init; }
}

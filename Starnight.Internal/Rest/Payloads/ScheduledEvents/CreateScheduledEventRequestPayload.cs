namespace Starnight.Internal.Rest.Payloads.ScheduledEvents;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guilds;

/// <summary>
/// Represents a request payload to POST /guilds/:guild_id/scheduled-events
/// </summary>
public record CreateScheduledEventRequestPayload
{
	/// <summary>
	/// The channel ID of the scheduled event.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("channel_id")]
	public Int64? ChannelId { get; init; }

	/// <summary>
	/// Represents metadata about the scheduled event.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("entity_metadata")]
	public DiscordScheduledEventMetadata? Metadata { get; init; }

	/// <summary>
	/// Name of the scheduled event.
	/// </summary>
	[JsonPropertyName("name")]
	public String Name { get; init; } = null!;

	/// <summary>
	/// Privacy level for this scheduled event.
	/// </summary>
	[JsonPropertyName("privacy_level")]
	public DiscordScheduledEventPrivacyLevel PrivacyLevel { get; init; }

	/// <summary>
	/// Indicates the time at which this event is scheduled to start.
	/// </summary>
	[JsonPropertyName("scheduled_start_time")]
	public DateTimeOffset ScheduledStartTime { get; init; }

	/// <summary>
	/// Indicates the time at which this event is scheduled to end.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("scheduled_end_time")]
	public DateTimeOffset? ScheduledEndTime { get; init; }

	/// <summary>
	/// Description for this scheduled event.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("description")]
	public String? Description { get; init; }

	/// <summary>
	/// The event type of this event.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("entity_type")]
	public DiscordScheduledEventType EventType { get; init; }

	/// <summary>
	/// Image data representing the cover image of this scheduled event.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("image")]
	public String? Image { get; init; }
}

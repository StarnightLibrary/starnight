namespace Starnight.Internal.Entities.Guilds.AutoModeration;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Stores additional metadata needed for this action.
/// </summary>
public record DiscordAutoModerationActionMetadata
{
	/// <summary>
	/// The snowflake identifer of the channel to which user content should be logged.
	/// </summary>
	/// <remarks>
	/// Only valid on <see cref="DiscordAutoModerationActionType.SendAlertMessage"/> actions.
	/// </remarks>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("channel_id")]
	public Int64? ChannelId { get; init; }

	/// <summary>
	/// Timeout duration in seconds.
	/// </summary>
	/// <remarks>
	/// Only valid on <see cref="DiscordAutoModerationActionType.Timeout"/> actions. The maximum value is
	/// 2419200 seconds, equals to four weeks.
	/// </remarks>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("duration_seconds")]
	public Int32? Duration { get; init; }
}

namespace Starnight.Internal.Gateway.Payloads.Clientbound.Dispatch;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the payload for a GuildScheduledEventUserAdded event.
/// </summary>
public sealed record ScheduledEventUserAddedPayload
{
	/// <summary>
	/// Snowflake ID of the scheduled event.
	/// </summary>
	[JsonPropertyName("guild_scheduled_event_id")]
	public required Int64 ScheduledEventId { get; init; }

	/// <summary>
	/// Snowflake ID of the user.
	/// </summary>
	[JsonPropertyName("user_id")]
	public required Int64 UserId { get; init; }

	/// <summary>
	/// Snowflake ID of the guild this took place in.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public required Int64 GuildId { get; init; }
}

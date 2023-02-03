namespace Starnight.Internal.Entities.Guilds;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Users;

/// <summary>
/// Represents a scheduled event user.
/// </summary>
public sealed record DiscordScheduledEventUser
{
	/// <summary>
	/// The snowflake identifier of the event the user subscribed to.
	/// </summary>
	[JsonPropertyName("guild_scheduled_event_id")]
	public required Int64 ScheduledEventId { get; init; }

	/// <summary>
	/// The user which subscribed to the event.
	/// </summary>
	[JsonPropertyName("user")]
	public required DiscordUser User { get; init; }

	/// <summary>
	/// Guild member object for the specified user in the guild this event belongs to.
	/// </summary>
	[JsonPropertyName("member")]
	public Optional<DiscordGuildMember> Member { get; init; }
}

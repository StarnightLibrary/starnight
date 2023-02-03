namespace Starnight.Internal.Gateway.Payloads.Inbound.Dispatch;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Users;
using Starnight.Internal.Entities.Users.Activities;

/// <summary>
/// Represents a payload for a PresenceUpdate event.
/// </summary>
public sealed record PresenceUpdatedPayload
{
	/// <summary>
	/// The user whose presence is being updated.
	/// </summary>
	[JsonPropertyName("user")]
	public required DiscordUser User { get; init; }

	/// <summary>
	/// The ID of the guild this event was dispatched from.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public required Int64 GuildId { get; init; }

	/// <summary>
	/// The status ID string of this user.
	/// </summary>
	[JsonPropertyName("status")]
	public required String Status { get; init; }

	/// <summary>
	/// The user's current activities.
	/// </summary>
	[JsonPropertyName("activities")]
	public required IEnumerable<DiscordActivity> Activities { get; init; }

	/// <summary>
	/// This user's client statuses.
	/// </summary>
	[JsonPropertyName("client_status")]
	public required DiscordClientStatus ClientStatus { get; init; }
}

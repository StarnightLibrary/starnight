namespace Starnight.Internal.Gateway.Objects.User;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Users;
using Starnight.Internal.Gateway.Objects.User.Activity;

/// <summary>
/// Represents a user presence update object via real-time gateway.
/// </summary>
public record DiscordPresenceUpdate
{
	/// <summary>
	/// The user this object affects.
	/// </summary>
	[JsonPropertyName("user")]
	public DiscordUser User { get; init; } = default!;

	/// <summary>
	/// The guild ID where this presence is being updated.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("guild_id")]
	public Int64 GuildId { get; init; }

	/// <summary>
	/// Presence status. Either "idle", "dnd", "online" or "offline".
	/// </summary>
	[JsonPropertyName("status")]
	public String Status { get; init; } = default!;

	/// <summary>
	/// Current activities of this user.
	/// </summary>
	[JsonPropertyName("activities")]
	public DiscordActivity[] CurrentActivities { get; init; } = default!;
}

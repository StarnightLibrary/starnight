namespace Starnight.Internal.Entities.Guild;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.User;

/// <summary>
/// Represents a discord guild member.
/// </summary>
public class DiscordGuildMember
{
	/// <summary>
	/// The user this guild member is associated with. Omitted in MESSAGE_CREATE and MESSAGE_UPDATE gateway events.
	/// </summary>
	[JsonPropertyName("user")]
	public DiscordUser? User { get; init; }

	/// <summary>
	/// This user's guild-specific nickname.
	/// </summary>
	[JsonPropertyName("nick")]
	public String? Nickname { get; init; }

	/// <summary>
	/// This user's guild-specific profile picture hash.
	/// </summary>
	[JsonPropertyName("avatar")]
	public String? Avatar { get; init; }

	/// <summary>
	/// Array of snowflakes of this member's guild roles.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("roles")]
	public Int64[]? Roles { get; init; }

	/// <summary>
	/// Denotes when this user joined the guild.
	/// </summary>
	[JsonPropertyName("joined_at")]
	public DateTime JoinedAt { get; init; }

	/// <summary>
	/// Denotes when this user started boosting the guild.
	/// </summary>
	[JsonPropertyName("premium_since")]
	public DateTime? BoostedAt { get; init; }

	/// <summary>
	/// Whether this user is deafened in voice channels.
	/// </summary>
	[JsonPropertyName("deaf")]
	public Boolean Deafened { get; init; }

	/// <summary>
	/// Whether this user is muted in voice channels.
	/// </summary>
	[JsonPropertyName("mute")]
	public Boolean Muted { get; init; }

	/// <summary>
	/// Whether this user has passed the guild's Membership Screening.
	/// </summary>
	[JsonPropertyName("pending")]
	public Boolean? Pending { get; init; }

	/// <summary>
	/// Total permissions of this member in the context channel; returned only as part of the interaction object.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("permissions")]
	public Int64? Permissions { get; init; }

	/// <summary>
	/// Denotes when this user's timeout will expire.
	/// </summary>
	[JsonPropertyName("communication_disabled_until")]
	public DateTime? TimeoutUntil { get; init; }
}

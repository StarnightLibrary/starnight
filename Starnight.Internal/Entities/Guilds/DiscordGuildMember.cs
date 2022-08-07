namespace Starnight.Internal.Entities.Guilds;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Users;

/// <summary>
/// Represents a discord guild member.
/// </summary>
public sealed record DiscordGuildMember
{
	/// <summary>
	/// The user this guild member is associated with. Omitted in MESSAGE_CREATE and MESSAGE_UPDATE gateway events.
	/// </summary>
	[JsonPropertyName("user")]
	public Optional<DiscordUser> User { get; init; }

	/// <summary>
	/// This user's guild-specific nickname.
	/// </summary>
	[JsonPropertyName("nick")]
	public Optional<String?> Nickname { get; init; }

	/// <summary>
	/// This user's guild-specific profile picture hash.
	/// </summary>
	[JsonPropertyName("avatar")]
	public Optional<String?> Avatar { get; init; }

	/// <summary>
	/// Array of snowflakes of this member's guild roles.
	/// </summary>
	[JsonPropertyName("roles")]
	public required IEnumerable<Int64> Roles { get; init; }

	/// <summary>
	/// Denotes when this user joined the guild.
	/// </summary>
	[JsonPropertyName("joined_at")]
	public required DateTimeOffset JoinedAt { get; init; }

	/// <summary>
	/// Denotes when this user started boosting the guild.
	/// </summary>
	[JsonPropertyName("premium_since")]
	public Optional<DateTimeOffset?> BoostedAt { get; init; }

	/// <summary>
	/// Whether this user is deafened in voice channels.
	/// </summary>
	[JsonPropertyName("deaf")]
	public required Boolean Deafened { get; init; }

	/// <summary>
	/// Whether this user is muted in voice channels.
	/// </summary>
	[JsonPropertyName("mute")]
	public required Boolean Muted { get; init; }

	/// <summary>
	/// Whether this user has passed the guild's Membership Screening.
	/// </summary>
	[JsonPropertyName("pending")]
	public Optional<Boolean> Pending { get; init; }

	/// <summary>
	/// Total permissions of this member in the context channel; returned only as part of the interaction object.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("permissions")]
	public Optional<Int64> Permissions { get; init; }

	/// <summary>
	/// Denotes when this user's timeout will expire.
	/// </summary>
	[JsonPropertyName("communication_disabled_until")]
	public Optional<DateTimeOffset?> TimeoutUntil { get; init; }
}

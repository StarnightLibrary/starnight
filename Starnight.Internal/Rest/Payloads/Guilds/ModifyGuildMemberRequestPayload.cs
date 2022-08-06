namespace Starnight.Internal.Rest.Payloads.Guilds;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a payload to PATCH /guilds/:guild_id/members/:user_id
/// </summary>
public sealed record ModifyGuildMemberRequestPayload
{
	/// <summary>
	/// The nickname to force the user to assume.
	/// </summary>
	[JsonPropertyName("nick")]
	public Optional<String?> Nickname { get; init; }

	/// <summary>
	/// An array of role IDs to assign.
	/// </summary>
	[JsonPropertyName("roles")]
	public Optional<IEnumerable<Int64>?> Roles { get; init; }

	/// <summary>
	/// Whether to mute the user.
	/// </summary>
	[JsonPropertyName("mute")]
	public Optional<Boolean?> Mute { get; init; }

	/// <summary>
	/// Whether to deafen the user.
	/// </summary>
	[JsonPropertyName("deaf")]
	public Optional<Boolean?> Deafen { get; init; }

	/// <summary>
	/// The voice channel ID to move the user into.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public Optional<Int64?> ChannelId { get; init; }

	/// <summary>
	/// The timestamp at which the user's timeout is supposed to expire. Set to null to remove the timeout.
	/// Must be no more than 28 days in the future.
	/// </summary>
	[JsonPropertyName("communication_disabled_until")]
	public Optional<DateTimeOffset?> CommunicationDisabledUntil { get; init; }
}

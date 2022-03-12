namespace Starnight.Internal.Rest.Payloads.Guilds;

using System.Text.Json.Serialization;
using System;

public record ModifyGuildMemberRequestPayload
{
	/// <summary>
	/// The nickname to force the user to assume.
	/// </summary>
	public OptionalParameter<String?> Nickname { get; init; }

	/// <summary>
	/// An array of role IDs to assign.
	/// </summary>
	public Int64[]? Roles { get; init; }

	/// <summary>
	/// Whether to mute the user.
	/// </summary>
	public Boolean? Mute { get; init; }

	/// <summary>
	/// Whether to deafen the user.
	/// </summary>
	public Boolean? Deafen { get; init; }

	/// <summary>
	/// The voice channel ID to move the user into.
	/// </summary>
	public Int64? ChannelId { get; init; }

	/// <summary>
	/// The timestamp at which the user's timeout is supposed to expire. Set to null to remove the timeout.
	/// Must be no more than 28 days in the future.
	/// </summary>
	public OptionalParameter<DateTimeOffset?> CommunicationDisabledUntil { get; init; }
}

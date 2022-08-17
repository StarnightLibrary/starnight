namespace Starnight.Internal.Gateway.Objects.Serverbound;

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a payload to <see cref="DiscordGatewayOpcode.RequestGuildMembers"/>.
/// </summary>
[StructLayout(LayoutKind.Auto)]
public record struct DiscordRequestGuildMembersCommandObject
{
	/// <summary>
	/// Snowflake ID of the guild to get members for.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public required Int64 GuildId { get; init; }

	/// <summary>
	/// Strings that usernames have to start with.
	/// </summary>
	[JsonPropertyName("query")]
	public Optional<String> Query { get; init; }

	/// <summary>
	/// The limit of members to send; a limit of 0 can be used with an empty query to return all members.
	/// </summary>
	[JsonPropertyName("limit")]
	public required Int32 Limit { get; init; }

	/// <summary>
	/// Specifies whether presences should be sent along with the member objects.
	/// </summary>
	[JsonPropertyName("presences")]
	public Optional<Boolean> Presences { get; init; }

	/// <summary>
	/// Specifies which users you wish to fetch.
	/// </summary>
	[JsonPropertyName("user_ids")]
	public Optional<IEnumerable<Int64>> UserIds { get; init; }

	/// <summary>
	/// Nonce to identify the response sent to this request.
	/// </summary>
	/// <remarks>
	/// The nonce can contain up to 32 bytes. If you send an invalid nonce, it will be ignored, and the reply will not
	/// have a nonce set.
	/// </remarks>
	[JsonPropertyName("nonce")]
	public Optional<String> Nonce { get; init; }
}

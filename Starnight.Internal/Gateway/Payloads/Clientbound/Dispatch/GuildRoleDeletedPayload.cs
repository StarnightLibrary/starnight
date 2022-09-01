namespace Starnight.Internal.Gateway.Payloads.Clientbound.Dispatch;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the payload for a GuildRoleDeleted event.
/// </summary>
public sealed record GuildRoleDeletedPayload
{
	/// <summary>
	/// The ID of the guild this role was deleted in.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public required Int64 GuildId { get; init; }

	/// <summary>
	/// The ID of the deleted role.
	/// </summary>
	[JsonPropertyName("role_id")]
	public required Int64 RoleId { get; init; }
}

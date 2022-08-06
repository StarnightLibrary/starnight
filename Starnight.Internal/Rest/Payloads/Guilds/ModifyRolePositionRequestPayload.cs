namespace Starnight.Internal.Rest.Payloads.Guilds;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a payload to PATCH /guilds/:guild_id/roles
/// </summary>
public sealed record ModifyRolePositionRequestPayload
{
	/// <summary>
	/// Snowflake identifier of the role in question.
	/// </summary>
	[JsonPropertyName("id")]
	public required Int64 RoleId { get; init; }

	/// <summary>
	/// New position of the role in question.
	/// </summary>
	[JsonPropertyName("position")]
	public Optional<Int32?> Position { get; init; }
}

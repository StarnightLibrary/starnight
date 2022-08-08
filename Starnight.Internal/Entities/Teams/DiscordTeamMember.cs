namespace Starnight.Internal.Entities.Teams;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Users;

/// <summary>
/// Represents a discord team member.
/// </summary>
public sealed record DiscordTeamMember
{
	/// <summary>
	/// Membership state on this team.
	/// </summary>
	[JsonPropertyName("membership_state")]
	public required DiscordTeamMembershipState State { get; init; }

	/// <summary>
	/// This will always be <c>["*"]</c>.
	/// </summary>
	[JsonPropertyName("permissions")]
	public required IEnumerable<String> Permissions { get; init; }

	/// <summary>
	/// Team snowflake identifier.
	/// </summary>
	[JsonPropertyName("team_id")]
	public required Int64 TeamId { get; init; }

	/// <summary>
	/// Discord user for this team member.
	/// </summary>
	[JsonPropertyName("user")]
	public required DiscordUser User { get; init; } 
}

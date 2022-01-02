namespace Starnight.Internal.Entities.Teams;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Users;

/// <summary>
/// Represents a discord team member.
/// </summary>
public record DiscordTeamMember
{
	/// <summary>
	/// Membership state on this team.
	/// </summary>
	[JsonPropertyName("membership_state")]
	public DiscordTeamMembershipState State { get; init; }

	/// <summary>
	/// User permissions. Obsolete, will always be "["*"]".
	/// </summary>
	[JsonPropertyName("permissions")]
	[Obsolete("Will always be \"[\"*\"]\"", DiagnosticId = "SE0002")]
	public String[] Permissions { get; init; } = default!;

	/// <summary>
	/// Team snowflake identifier.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("team_id")]
	public Int64 TeamId { get; init; }

	/// <summary>
	/// Discord user for this team member.
	/// </summary>
	[JsonPropertyName("user")]
	public DiscordUser User { get; init; } = default!;
}

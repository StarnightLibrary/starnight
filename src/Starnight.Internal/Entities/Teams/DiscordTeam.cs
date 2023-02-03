namespace Starnight.Internal.Entities.Teams;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a discord team.
/// </summary>
public sealed record DiscordTeam : DiscordSnowflakeObject
{
	/// <summary>
	/// The icon hash for this team's icon.
	/// </summary>
	[JsonPropertyName("icon")]
	public String? Icon { get; init; }

	/// <summary>
	/// Array of team members.
	/// </summary>
	[JsonPropertyName("members")]
	public required IEnumerable<DiscordTeamMember> Members { get; init; }

	/// <summary>
	/// Team name.
	/// </summary>
	[JsonPropertyName("name")]
	public required String Name { get; init; }

	/// <summary>
	/// Team owner snowflake identifier.
	/// </summary>
	[JsonPropertyName("owner_user_id")]
	public required Int64 TeamOwnerId { get; init; }
}

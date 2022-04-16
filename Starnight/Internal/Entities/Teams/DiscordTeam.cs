namespace Starnight.Internal.Entities.Teams;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a discord team.
/// </summary>
public record DiscordTeam : DiscordSnowflakeObject
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
	public IEnumerable<DiscordTeamMember> Members { get; init; } = default!;

	/// <summary>
	/// Team name.
	/// </summary>
	[JsonPropertyName("name")]
	public String Name { get; init; } = default!;

	/// <summary>
	/// Team owner snowflake identifier.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("owner_user_id")]
	public Int64 TeamId { get; init; }
}

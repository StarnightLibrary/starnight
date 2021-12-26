namespace Starnight.Internal.Entities.Team;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a discord team.
/// </summary>
public class DiscordTeam : DiscordSnowflakeObject
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
	public DiscordTeamMember[] Members { get; init; } = default!;

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

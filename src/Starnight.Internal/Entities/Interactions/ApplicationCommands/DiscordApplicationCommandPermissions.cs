namespace Starnight.Internal.Entities.Interactions.ApplicationCommands;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a set of permissions for an application command.
/// </summary>
public sealed record DiscordApplicationCommandPermissions : DiscordSnowflakeObject
{
	/// <summary>
	/// Snowflake identifier of the application this permission set is tied to.
	/// </summary>
	[JsonPropertyName("application_id")]
	public required Int64 ApplicationId { get; init; }

	/// <summary>
	/// Snowflake identifier of the guild this permission set is tied to.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public required Int64 GuildId { get; init; }

	/// <summary>
	/// All permission overwrites for this command in the specified guild.
	/// </summary>
	[JsonPropertyName("permissions")]
	public required IEnumerable<DiscordApplicationCommandPermission> Permissions { get; init; }
}

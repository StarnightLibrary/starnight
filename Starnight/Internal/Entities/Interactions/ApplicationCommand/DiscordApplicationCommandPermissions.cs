namespace Starnight.Internal.Entities.Interactions.ApplicationCommand;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a set of permissions for an application command.
/// </summary>
public record DiscordApplicationCommandPermissions : DiscordSnowflakeObject
{
	/// <summary>
	/// Snowflake identifier of the application this permission set is tied to.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("application_id")]
	public Int64 ApplicationId { get; init; }

	/// <summary>
	/// Snowflake identifier of the guild this permission set is tied to.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("guild_id")]
	public Int64 GuildId { get; init; }

	/// <summary>
	/// All permission overwrites for this command in the specified guild.
	/// </summary>
	[JsonPropertyName("permissions")]
	public DiscordApplicationCommandPermission[] Permissions { get; init; } = default!;
}

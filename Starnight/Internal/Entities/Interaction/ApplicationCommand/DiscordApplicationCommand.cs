namespace Starnight.Internal.Entities.Interaction.ApplicationCommand;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents an application command.
/// </summary>
public record DiscordApplicationCommand : DiscordSnowflakeObject
{
	/// <summary>
	/// The type of this application command.
	/// </summary>
	[JsonPropertyName("type")]
	public DiscordApplicationCommandType Type { get; init; }

	/// <summary>
	/// Snowflake identifier of the application this command belongs to.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("application_id")]
	public Int64 ApplicationId { get; init; }

	/// <summary>
	/// Snowflake identifier of the guild this command is registered to, if not global.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("guild_id")]
	public Int64? GuildId { get; init; }

	/// <summary>
	/// Name of this application command.
	/// </summary>
	[JsonPropertyName("name")]
	public String Name { get; init; } = default!;

	/// <summary>
	/// Description for this application command.
	/// </summary>
	[JsonPropertyName("description")]
	public String Description { get; init; } = default!;

	/// <summary>
	/// The parameters for this command, up to 25.
	/// </summary>
	[JsonPropertyName("options")]
	public DiscordApplicationCommandOption[]? Options { get; init; }

	/// <summary>
	/// Whether this command is enabled for everyone by default.
	/// </summary>
	[JsonPropertyName("default_permission")]
	public Boolean? DefaultPermission { get; init; } = true;

	/// <summary>
	/// Automatically incrementing version ID updated for substantial changes.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("version")]
	public Int64 Version { get; init; }
}

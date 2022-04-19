namespace Starnight.Internal.Entities.Interactions.ApplicationCommands;

using System;
using System.Collections.Generic;
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
	/// Localized name of this application command.
	/// </summary>
	[JsonPropertyName("name_localized")]
	public String? NameLocalized { get; init; }

	/// <summary>
	/// Localization dictionary for the <see cref="Name"/> field.
	/// </summary>
	/// <remarks>
	/// Also refer to the documentation of <seealso cref="DiscordLocale"/>.
	/// </remarks>
	[JsonPropertyName("name_localizations")]
	public IDictionary<String, String>? NameLocalizations { get; init; }

	/// <summary>
	/// Description for this application command.
	/// </summary>
	[JsonPropertyName("description")]
	public String Description { get; init; } = default!;

	/// <summary>
	/// Localized description of this application command.
	/// </summary>
	[JsonPropertyName("description_localized")]
	public String? DescriptionLocalized { get; init; }

	/// <summary>
	/// Localization dictionary for the <see cref="Description"/> field.
	/// </summary>
	/// <remarks>
	/// Also refer to the documentation of <seealso cref="DiscordLocale"/>.
	/// </remarks>
	[JsonPropertyName("description_localizations")]
	public IDictionary<String, String>? DescriptionLocalizations { get; init; }

	/// <summary>
	/// The parameters for this command, up to 25.
	/// </summary>
	[JsonPropertyName("options")]
	public IEnumerable<DiscordApplicationCommandOption>? Options { get; init; }

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

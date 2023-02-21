namespace Starnight.Internal.Entities.Interactions.ApplicationCommands;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Entities;

/// <summary>
/// Represents an application command.
/// </summary>
public sealed record DiscordApplicationCommand : DiscordSnowflakeObject
{
	/// <summary>
	/// The type of this application command.
	/// </summary>
	[JsonPropertyName("type")]
	public Optional<DiscordApplicationCommandType> Type { get; init; }

	/// <summary>
	/// Snowflake identifier of the application this command belongs to.
	/// </summary>
	[JsonPropertyName("application_id")]
	public required Int64 ApplicationId { get; init; }

	/// <summary>
	/// Snowflake identifier of the guild this command is registered to, if not global.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public Optional<Int64> GuildId { get; init; }

	/// <summary>
	/// Name of this application command.
	/// </summary>
	[JsonPropertyName("name")]
	public Optional<String> Name { get; init; }

	/// <summary>
	/// Localized name of this application command.
	/// </summary>
	[JsonPropertyName("name_localized")]
	public Optional<String> NameLocalized { get; init; }

	/// <summary>
	/// Localization dictionary for the <see cref="Name"/> field.
	/// </summary>
	/// <remarks>
	/// Also refer to the documentation of <seealso cref="DiscordLocale"/>.
	/// </remarks>
	[JsonPropertyName("name_localizations")]
	public Optional<IDictionary<String, String>?> NameLocalizations { get; init; }

	/// <summary>
	/// Description for this application command.
	/// </summary>
	[JsonPropertyName("description")]
	public required String Description { get; init; }

	/// <summary>
	/// Localized name of this application command.
	/// </summary>
	[JsonPropertyName("description_localized")]
	public Optional<String> DescriptionLocalized { get; init; }

	/// <summary>
	/// Localization dictionary for the <see cref="Description"/> field.
	/// </summary>
	/// <remarks>
	/// Also refer to the documentation of <seealso cref="DiscordLocale"/>.
	/// </remarks>
	[JsonPropertyName("description_localizations")]
	public Optional<IDictionary<String, String>?> DescriptionLocalizations { get; init; }

	/// <summary>
	/// The parameters for this command, up to 25.
	/// </summary>
	[JsonPropertyName("options")]
	public Optional<IEnumerable<DiscordApplicationCommandOption>> Options { get; init; }

	/// <summary>
	/// Default permissions required to execute this command.
	/// </summary>
	[JsonPropertyName("default_member_permission")]
	public DiscordPermissions? DefaultMemberPermission { get; init; }

	/// <summary>
	/// Whether the command is available in DMs with the bot. This is only applicable to global commands.
	/// </summary>
	[JsonPropertyName("dm_permission")]
	public Optional<Boolean> DMPermission { get; init; }

	/// <summary>
	/// Automatically incrementing version ID updated for substantial changes.
	/// </summary>
	[JsonPropertyName("version")]
	public required Int64 Version { get; init; }
}

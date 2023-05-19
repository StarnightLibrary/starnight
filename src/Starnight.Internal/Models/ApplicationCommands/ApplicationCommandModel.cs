namespace Starnight.Internal.Models.ApplicationCommands;

using System;
using System.Collections.Generic;

using Starnight.Entities;

/// <summary>
/// Represents an application command; that is, either a slash command or an user/message
/// context menu command.
/// </summary>
public sealed record ApplicationCommandModel
{
	/// <summary>
	/// The snowflake identifier of this command.
	/// </summary>
	public required Int64 Id { get; init; }

	/// <summary>
	/// The type of this command. Defaults to <see cref="DiscordApplicationCommandType.ChatInput"/>
	/// </summary>
	public Optional<DiscordApplicationCommandType> Type { get; init; }

	/// <summary>
	/// The snowflake identifier of the application which owns this command.
	/// </summary>
	public required Int64 ApplicationId { get; init; }

	/// <summary>
	/// The snowflake identifier of the guild this command is registered into, if it is a guild command.
	/// </summary>
	public Optional<Int64> GuildId { get; init; }

	/// <summary>
	/// The name of this command, 1-32 characters.
	/// </summary>
	public required String Name { get; init; }

	/// <summary>
	/// A dictionary of locale-mapped localized names for <seealso cref="Name"/>.
	/// </summary>
	public Optional<IReadOnlyDictionary<String, String>?> NameLocalizations { get; init; }

	/// <summary>
	/// The description for this command, 1-100 characters. This is an empty string if
	/// <seealso cref="Type"/> is not <seealso cref="DiscordApplicationCommandType.ChatInput"/>
	/// </summary>
	public required String Description { get; init; }

	/// <summary>
	/// A dictionary of locale-mapped localized descriptions for <seealso cref="Description"/>.
	/// </summary>
	public Optional<IReadOnlyDictionary<String, String>?> DescriptionLocalizations { get; init; }

	/// <summary>
	/// Up to 25 parameters or subcommands/subcommand groups for this command. Required options
	/// must be listed before optional options. This is only available if <seealso cref="Type"/>
	/// is <seealso cref="DiscordApplicationCommandType.ChatInput"/>
	/// </summary>
	public Optional<IReadOnlyList<ApplicationCommandOptionModel>> Options { get; init; }

	/// <summary>
	/// The default set of permissions required to execute this command.
	/// </summary>
	public DiscordPermissions? DefaultMemberPermissions { get; init; }

	/// <summary>
	/// Indicates whether this command is available in DMs with this application. This is only
	/// applicable to globally-scoped commands (i.e., <seealso cref="GuildId"/> does not have a value).
	/// </summary>
	public Optional<Boolean> DmPermission { get; init; }

	/// <summary>
	/// Indicates whether this command is age-restricted.
	/// </summary>
	public Optional<Boolean> Nsfw { get; init; }

	/// <summary>
	/// An auto-incrementing version identifier for this command, tracked by Discord.
	/// </summary>
	public required Int64 Version { get; init; }
}

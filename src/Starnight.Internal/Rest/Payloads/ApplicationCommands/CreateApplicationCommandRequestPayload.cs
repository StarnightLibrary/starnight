namespace Starnight.Internal.Rest.Payloads.ApplicationCommands;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities;
using Starnight.Internal.Entities.Interactions;
using Starnight.Internal.Entities.Interactions.ApplicationCommands;

/// <summary>
/// Represents a payload to POST /applications/:app_id/commands
/// </summary>
public sealed record CreateApplicationCommandRequestPayload
{
	/// <summary>
	/// The type of this application command.
	/// </summary>
	[JsonPropertyName("type")]
	public Optional<DiscordApplicationCommandType> Type { get; init; }

	/// <summary>
	/// Name of this application command.
	/// </summary>
	[JsonPropertyName("name")]
	public required String Name { get; init; }

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
	public Optional<DiscordPermissions?> DefaultMemberPermission { get; init; }

	/// <summary>
	/// Whether the command is available in DMs with the bot. This is only applicable to global commands.
	/// </summary>
	[JsonPropertyName("dm_permission")]
	public Optional<Boolean?> DMPermission { get; init; }
}

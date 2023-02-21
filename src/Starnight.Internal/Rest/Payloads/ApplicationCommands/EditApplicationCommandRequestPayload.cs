namespace Starnight.Internal.Rest.Payloads.ApplicationCommands;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Entities;
using Starnight.Internal.Entities.Interactions.ApplicationCommands;

/// <summary>
/// Represents a payload to PATCH /applications/:app_id/commands/:command_id
/// </summary>
public sealed record EditApplicationCommandRequestPayload
{
	/// <summary>
	/// The new name for this application command.
	/// </summary>
	[JsonPropertyName("name")]
	public Optional<String> Name { get; init; }

	/// <summary>
	/// New localizations for this application command's name.
	/// </summary>
	[JsonPropertyName("name_localizations")]
	public Optional<IDictionary<String, String>?> NameLocalizations { get; init; }

	/// <summary>
	/// The new description for this application command.
	/// </summary>
	[JsonPropertyName("description")]
	public Optional<String> Description { get; init; }

	/// <summary>
	/// New localizations for this application command's name.
	/// </summary>
	[JsonPropertyName("description_localizations")]
	public Optional<IDictionary<String, String>?> DescriptionLocalizations { get; init; }

	/// <summary>
	/// New options for this application command.
	/// </summary>
	[JsonPropertyName("options")]
	public Optional<IEnumerable<DiscordApplicationCommandOption>?> Options { get; init; }

	/// <summary>
	/// Default permissions required to execute this command.
	/// </summary>
	[JsonPropertyName("default_member_permission")]
	public Optional<DiscordPermissions> DefaultMemberPermission { get; init; }

	/// <summary>
	/// Whether the command is available in DMs with the bot. This is only applicable to global commands.
	/// </summary>
	[JsonPropertyName("dm_permission")]
	public Optional<Boolean> DMPermission { get; init; }
}

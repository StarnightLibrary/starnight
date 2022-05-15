namespace Starnight.Internal.Rest.Payloads.ApplicationCommands;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Converters;
using Starnight.Internal.Entities;
using Starnight.Internal.Entities.Interactions.ApplicationCommands;

/// <summary>
/// Represents a payload to PATCH /applications/:app_id/commands/:command_id
/// </summary>
public record EditApplicationCommandRequestPayload
{
	/// <summary>
	/// The new name for this application command.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("name")]
	public String? Name { get; init; }

	/// <summary>
	/// New localizations for this application command's name.
	/// </summary>
	[JsonConverter(typeof(OptionalParameterJsonConverterFactory))]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("name_localizations")]
	public OptionalParameter<IDictionary<String, String>>? NameLocalizations { get; init; }

	/// <summary>
	/// The new description for this application command.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("description")]
	public String? Description { get; init; }

	/// <summary>
	/// New localizations for this application command's name.
	/// </summary>
	[JsonConverter(typeof(OptionalParameterJsonConverterFactory))]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("description_localizations")]
	public OptionalParameter<IDictionary<String, String>>? DescriptionLocalizations { get; init; }

	/// <summary>
	/// New options for this application command.
	/// </summary>
	[JsonConverter(typeof(OptionalParameterJsonConverterFactory))]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("options")]
	public OptionalParameter<IEnumerable<DiscordApplicationCommandOption>>? Options { get; init; }

	/// <summary>
	/// Default permissions required to execute this command.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("default_member_permission")]
	public DiscordPermissions? DefaultMemberPermission { get; init; }

	/// <summary>
	/// Whether the command is available in DMs with the bot. This is only applicable to global commands.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("dm_permission")]
	public Boolean? DMPermission { get; init; }
}

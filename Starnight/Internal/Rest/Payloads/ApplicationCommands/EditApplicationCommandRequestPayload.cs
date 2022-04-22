namespace Starnight.Internal.Rest.Payloads.ApplicationCommands;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Converters;
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
	/// Specifies whether this command should be enabled by default
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("default_permission")]
	public Boolean? DefaultPermission { get; init; }
}

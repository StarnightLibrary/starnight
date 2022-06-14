namespace Starnight.Internal.Entities.Interactions.ApplicationCommands;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Interactions;

/// <summary>
/// Represents a slash command parameter.
/// </summary>
public record DiscordApplicationCommandOption
{
	/// <summary>
	/// The parameter type.
	/// </summary>
	[JsonPropertyName("type")]
	public DiscordApplicationCommandOptionType Type { get; init; }

	/// <summary>
	/// The parameter name.
	/// </summary>
	[JsonPropertyName("name")]
	public String Name { get; init; } = default!;

	/// <summary>
	/// Localized name of this application command option.
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
	/// The parameter description.
	/// </summary>
	[JsonPropertyName("description")]
	public String Description { get; init; } = default!;

	/// <summary>
	/// Localized description of this application command option.
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
	/// Whether this parameter must be passed to the slash command.
	/// </summary>
	[JsonPropertyName("required")]
	public Boolean? Required { get; init; } = false;

	/// <summary>
	/// All valid choices for this parameter. Not listed choices are not valid inputs.
	/// </summary>
	[JsonPropertyName("choices")]
	public IEnumerable<DiscordApplicationCommandOptionChoice>? Choices { get; init; }

	/// <summary>
	/// If this is a subcommand/subcommand group; these options are valid for the next level.
	/// </summary>
	[JsonPropertyName("options")]
	public IEnumerable<DiscordApplicationCommandOption>? Suboptions { get; init; }

	/// <summary>
	/// The minimum value for this parameter. 
	/// </summary>
	[JsonPropertyName("min_value")]
	public Object? MinValue { get; init; }

	/// <summary>
	/// The maximum value for this parameter.
	/// </summary>
	[JsonPropertyName("max_value")]
	public Object? MaxValue { get; init; }

	/// <summary>
	/// Whether this application command should use autocomplete.
	/// </summary>
	[JsonPropertyName("autocomplete")]
	public Boolean? Autocomplete { get; init; }
}

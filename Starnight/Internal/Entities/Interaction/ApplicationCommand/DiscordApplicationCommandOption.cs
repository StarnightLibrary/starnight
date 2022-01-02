namespace Starnight.Internal.Entities.Interaction.ApplicationCommand;

using System;
using System.Text.Json.Serialization;

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
	/// The parameter description.
	/// </summary>
	[JsonPropertyName("description")]
	public String Description { get; init; } = default!;

	/// <summary>
	/// Whether this parameter must be passed to the slash command.
	/// </summary>
	[JsonPropertyName("required")]
	public Boolean? Required { get; init; } = false;

	/// <summary>
	/// All valid choices for this parameter. Not listed choices are not valid inputs.
	/// </summary>
	[JsonPropertyName("choices")]
	public DiscordApplicationCommandOptionChoice[]? Choices { get; init; }

	/// <summary>
	/// If this is a subcommand/subcommand group; these options are valid for the next level.
	/// </summary>
	[JsonPropertyName("options")]
	public DiscordApplicationCommandOption[]? Suboptions { get; init; }

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

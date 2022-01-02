namespace Starnight.Internal.Entities.Interactions.ApplicationCommand;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a slash command data option.
/// </summary>
public record DiscordApplicationCommandDataOption
{
	/// <summary>
	/// The name of the invoked option.
	/// </summary>
	[JsonPropertyName("name")]
	public String Name { get; init; } = default!;

	/// <summary>
	/// The type of the invoked option.
	/// </summary>
	[JsonPropertyName("type")]
	public DiscordApplicationCommandOptionType Type { get; init; }

	/// <summary>
	/// The user input passed to this option.
	/// </summary>
	[JsonPropertyName("value")]
	public Object? Value { get; init; }

	/// <summary>
	/// Potential subcommand options if this is a group or subcommand.
	/// </summary>
	[JsonPropertyName("options")]
	public DiscordApplicationCommandDataOption[]? Options { get; init; }

	/// <summary>
	/// Whether this option is the currently focused option for autocomplete.
	/// </summary>
	[JsonPropertyName("focused")]
	public Boolean? Focused { get; init; }
}

namespace Starnight.Internal.Entities.Interactions.ApplicationCommands;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Entities;

/// <summary>
/// Represents a slash command data option.
/// </summary>
public sealed record DiscordApplicationCommandDataOption
{
	/// <summary>
	/// The name of the invoked option.
	/// </summary>
	[JsonPropertyName("name")]
	public required String Name { get; init; }

	/// <summary>
	/// The type of the invoked option.
	/// </summary>
	[JsonPropertyName("type")]
	public required DiscordApplicationCommandOptionType Type { get; init; }

	/// <summary>
	/// The user input passed to this option.
	/// </summary>
	[JsonPropertyName("value")]
	public Optional<Object> Value { get; init; }

	/// <summary>
	/// Potential subcommand options if this is a group or subcommand.
	/// </summary>
	[JsonPropertyName("options")]
	public Optional<IEnumerable<DiscordApplicationCommandDataOption>> Options { get; init; }

	/// <summary>
	/// Whether this option is the currently focused option for autocomplete.
	/// </summary>
	[JsonPropertyName("focused")]
	public Optional<Boolean> Focused { get; init; }
}

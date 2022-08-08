namespace Starnight.Internal.Entities.Interactions.Components;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guilds;

/// <summary>
/// Represents a select menu option.
/// </summary>
public sealed record DiscordSelectMenuOption
{
	/// <summary>
	/// User-facing name of the option, max. 100 characters.
	/// </summary>
	[JsonPropertyName("label")]
	public required String Label { get; init; }

	/// <summary>
	/// Dev-facing value of the option, max. 100 characters.
	/// </summary>
	[JsonPropertyName("value")]
	public required String Value { get; init; }

	/// <summary>
	/// Additional description for this option, max. 100 characters
	/// </summary>
	[JsonPropertyName("description")]
	public Optional<String> Description { get; init; }

	/// <summary>
	/// Emoji to be rendered for this option.
	/// </summary>
	[JsonPropertyName("emoji")]
	public Optional<DiscordEmoji> Emoji { get; init; }

	/// <summary>
	/// Whether this option is selected by default.
	/// </summary>
	[JsonPropertyName("default")]
	public Optional<Boolean> Default { get; init; }
}

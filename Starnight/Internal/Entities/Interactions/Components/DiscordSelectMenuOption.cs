namespace Starnight.Internal.Entities.Interactions.Components;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guilds;

/// <summary>
/// Represents a select menu option.
/// </summary>
public record DiscordSelectMenuOption
{
	/// <summary>
	/// User-facing name of the option, max. 100 characters.
	/// </summary>
	[JsonPropertyName("label")]
	public String Label { get; init; } = default!;

	/// <summary>
	/// Dev-facing value of the option, max. 100 characters.
	/// </summary>
	[JsonPropertyName("value")]
	public String Value { get; init; } = default!;

	/// <summary>
	/// Additional description for this option, max. 100 characters
	/// </summary>
	[JsonPropertyName("description")]
	public String? Description { get; init; }

	/// <summary>
	/// Emoji to be rendered for this option.
	/// </summary>
	[JsonPropertyName("emoji")]
	public DiscordEmoji? Emoji { get; init; }

	/// <summary>
	/// Whether this option is selected by default.
	/// </summary>
	[JsonPropertyName("default")]
	public Boolean? Default { get; init; }
}

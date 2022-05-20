namespace Starnight.Internal.Entities.Interactions;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guilds;

/// <summary>
/// Represents a discord message component.
/// </summary>
public record DiscordMessageComponent
{
	/// <summary>
	/// Type of this component.
	/// </summary>
	[JsonPropertyName("type")]
	public DiscordMessageComponentType Type { get; init; }

	/// <summary>
	/// A developer-defined ID for this component, max. 100 characters.
	/// </summary>
	[JsonPropertyName("custom_id")]
	public String? CustomId { get; init; }

	/// <summary>
	/// Whether this component is disabled.
	/// </summary>
	[JsonPropertyName("disabled")]
	public Boolean? Disabled { get; init; }

	/// <summary>
	/// This button's style.
	/// </summary>
	[JsonPropertyName("style")]
	public DiscordButtonStyle? Style { get; init; }

	/// <summary>
	/// Text shown on this button, max. 80 characters.
	/// </summary>
	[JsonPropertyName("label")]
	public String? Label { get; init; }

	/// <summary>
	/// Emoji to be shown on this button.
	/// </summary>
	[JsonPropertyName("emoji")]
	public DiscordEmoji? Emoji { get; init; }

	/// <summary>
	/// An url for link buttons.
	/// </summary>
	[JsonPropertyName("url")]
	public String? Url { get; init; }

	/// <summary>
	/// The choices for this select menu, max. 25.
	/// </summary>
	[JsonPropertyName("options")]
	public IEnumerable<DiscordSelectMenuOption>? Options { get; init; }

	/// <summary>
	/// Custom placeholder text for this select menu if nothing is selected.
	/// </summary>
	[JsonPropertyName("placeholder")]
	public String? Placeholder { get; init; }

	/// <summary>
	/// Minimum number of items that must be chosen, default 1, max. 25, min. 0.
	/// </summary>
	[JsonPropertyName("min_values")]
	public Int32? MinSelections { get; init; } = 1;

	/// <summary>
	/// Maximum number of items that must be chosen, default 1, max. 25.
	/// </summary>
	[JsonPropertyName("max_values")]
	public Int32? MaxSelections { get; init; } = 1;

	/// <summary>
	/// Child components of this action row.
	/// </summary>
	[JsonPropertyName("components")]
	public IEnumerable<DiscordMessageComponent>? Components { get; init; }
}

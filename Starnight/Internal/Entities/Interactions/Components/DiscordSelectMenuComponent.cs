namespace Starnight.Internal.Entities.Interactions.Components;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a select menu component.
/// </summary>
public record DiscordSelectMenuComponent : AbstractInteractiveDiscordMessageComponent
{
	/// <summary>
	/// Up to 25 choices for this select menu.
	/// </summary>
	[JsonPropertyName("options")]
	public IEnumerable<DiscordSelectMenuOption> Options { get; init; } = null!;

	/// <summary>
	/// Custom placeholder text while nothing is selected, max. 150 characters.
	/// </summary>
	[JsonPropertyName("placeholder")]
	public String? Placeholder { get; init; }

	/// <summary>
	/// The minimum number of items that must be chosen.
	/// </summary>
	[JsonPropertyName("min_values")]
	public Int32? MinValues { get; init; }

	/// <summary>
	/// The maximum number of items that must be chosen.
	/// </summary>
	[JsonPropertyName("max_values")]
	public Int32? MaxValues { get; init; }

	/// <summary>
	/// Whether this component is disabled.
	/// </summary>
	[JsonPropertyName("disabled")]
	public Boolean? Disabled { get; init; }
}

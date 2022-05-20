namespace Starnight.Internal.Entities.Interactions.Components;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a text input component; child component of a <see cref="DiscordModalComponent"/>
/// </summary>
public record DiscordTextInputComponent : AbstractInteractiveDiscordMessageComponent
{
	/// <summary>
	/// The text input style of this component.
	/// </summary>
	[JsonPropertyName("style")]
	public DiscordTextInputStyle? Style { get; init; }

	/// <summary>
	/// The label for this component, max. 45 characters.
	/// </summary>
	[JsonPropertyName("label")]
	public String Label { get; init; } = null!;

	/// <summary>
	/// The minimal length for a text input, ranging from 0 to 4000.
	/// </summary>
	[JsonPropertyName("min_length")]
	public Int32? MinLength { get; init; }

	/// <summary>
	/// The maximum length for a text input, ranging from 0 to 4000.
	/// </summary>
	[JsonPropertyName("max_length")]
	public Int32? MaxLength { get; init; }

	/// <summary>
	/// Whether this component is required.
	/// </summary>
	[JsonPropertyName("required")]
	public Boolean? Required { get; init; }

	/// <summary>
	/// A pre-filled value for this component.
	/// </summary>
	[JsonPropertyName("value")]
	public String? Value { get; init; }

	/// <summary>
	/// Custom placeholder if the text input is empty, max. 100 characters.
	/// </summary>
	[JsonPropertyName("placeholder")]
	public String? Placeholder { get; init; }
}

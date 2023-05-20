namespace Starnight.Internal.Models.MessageComponents;

using System;

using Starnight.Entities;

/// <summary>
/// Represents a text input component. This can only be sent inside a modal.
/// </summary>
public sealed record TextInputComponent : AbstractInteractiveComponent
{
	public TextInputComponent() => this.Type = DiscordMessageComponentType.TextInput;

	/// <summary>
	/// The identifier of this component, sent in INTERACTION_CREATE.
	/// </summary>
	public required String CustomId { get; init; }

	/// <summary>
	/// The style of this component.
	/// </summary>
	public required DiscordTextInputStyle Style { get; init; }

	/// <summary>
	/// The user-facing label for this input field.
	/// </summary>
	public required String Label { get; init; }

	/// <summary>
	/// The minimum input length for this field, 0-4000.
	/// </summary>
	public Optional<Int32> MinLength { get; init; }

	/// <summary>
	/// The maximum input length for this field, 1-4000.
	/// </summary>
	public Optional<Int32> MaxLength { get; init; }

	/// <summary>
	/// Indicates whether this field is required to be filled.
	/// </summary>
	public Optional<Boolean> Required { get; init; }

	/// <summary>
	/// Pre-filled value for this component, up to 4000 characters.
	/// </summary>
	public Optional<String> Value { get; init; }

	/// <summary>
	/// Custom placeholder text if this field is empty, up to 100 characters.
	/// </summary>
	public Optional<String> Placeholder { get; init; }
}

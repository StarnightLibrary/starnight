namespace Starnight.Internal.Entities.Interactions.Components;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a modal component.
/// </summary>
public sealed record DiscordModalComponent : AbstractInteractiveDiscordMessageComponent
{
	/// <summary>
	/// The title to be displayed above this modal.
	/// </summary>
	/// <remarks>
	/// Note that even though this property is nullable, it likely is not nullable. Discord's documentation is, once again,
	/// lacking and we cannot (yet) fully know whether this property is nullable or not.
	/// </remarks>
	[JsonPropertyName("title")]
	public Optional<String?> Title { get; init; }

	/// <summary>
	/// An action row holding the text input components for this modal.
	/// </summary>
	/// <remarks>
	/// Note that even though this property is of the type <see cref="IEnumerable{T}"/>, it (likely) only accepts a single
	/// element. Discord's documentation is, once again, lacking, and the only thing known for sure is that Discord expects
	/// this property to be an array, likely only with a single element.
	/// </remarks>
	[JsonPropertyName("components")]
	public required IEnumerable<DiscordActionRowComponent> Components { get; init; }
}

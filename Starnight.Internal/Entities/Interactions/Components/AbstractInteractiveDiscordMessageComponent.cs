namespace Starnight.Internal.Entities.Interactions.Components;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents an interactive message component.
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(DiscordButtonComponent), 2)]
[JsonDerivedType(typeof(DiscordSelectMenuComponent), 3)]
[JsonDerivedType(typeof(DiscordTextInputComponent), 4)]
public abstract record AbstractInteractiveDiscordMessageComponent : AbstractDiscordMessageComponent
{
	/// <summary>
	/// A developer-defined ID for this component, max. 100 characters.
	/// </summary>
	[JsonPropertyName("custom_id")]
	public Optional<String> CustomId { get; init; }
}

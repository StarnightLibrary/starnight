namespace Starnight.Internal.Entities.Interactions.Components;

using System.Text.Json.Serialization;

using Starnight.Entities;

/// <summary>
/// Represents a discord message component.
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(DiscordActionRowComponent), 1)]
[JsonDerivedType(typeof(DiscordButtonComponent), 2)]
[JsonDerivedType(typeof(DiscordSelectMenuComponent), 3)]
[JsonDerivedType(typeof(DiscordTextInputComponent), 4)]
[JsonDerivedType(typeof(DiscordSelectMenuComponent), 5)]
[JsonDerivedType(typeof(DiscordSelectMenuComponent), 6)]
[JsonDerivedType(typeof(DiscordSelectMenuComponent), 7)]
[JsonDerivedType(typeof(DiscordSelectMenuComponent), 8)]
public abstract record AbstractDiscordMessageComponent
{
	/// <summary>
	/// Type of this component.
	/// </summary>
	[JsonPropertyName("type")]
	public DiscordMessageComponentType Type { get; init; }
}

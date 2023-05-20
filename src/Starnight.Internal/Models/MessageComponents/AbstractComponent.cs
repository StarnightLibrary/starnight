namespace Starnight.Internal.Models.MessageComponents;

using System.Text.Json.Serialization;

using Starnight.Entities;

/// <summary>
/// Represents any form of message component.
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(ActionRowComponent), 1)]
public abstract record AbstractComponent
{
	/// <summary>
	/// Contains the type of this component.
	/// </summary>
	public DiscordMessageComponentType Type { get; init; }
}

namespace Starnight.Internal.Models.MessageComponents;

using System.Text.Json.Serialization;

/// <summary>
/// Represents any component that the end user can directly interact with, that is, all
/// components save <seealso cref="ActionRowComponent"/>.
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(ButtonComponent), 2)]
[JsonDerivedType(typeof(TextSelectComponent), 3)]
[JsonDerivedType(typeof(TextInputComponent), 4)]
public abstract record AbstractInteractiveComponent : AbstractComponent
{
}

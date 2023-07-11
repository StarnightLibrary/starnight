namespace Starnight.Internal.Models.MessageComponents;

using System.Collections.Generic;

using Starnight.Entities;

/// <summary>
/// Represents a container component for one or five other components, depending on their type.
/// </summary>
public sealed record ActionRowComponent : AbstractComponent
{
	public ActionRowComponent() => this.Type = DiscordMessageComponentType.ActionRow;

	/// <summary>
	/// Contains the subcomponents of this action row.
	/// </summary>
	public required IReadOnlyList<AbstractInteractiveComponent> Components { get; init; }
}

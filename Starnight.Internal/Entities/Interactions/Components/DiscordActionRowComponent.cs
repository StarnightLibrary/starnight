namespace Starnight.Internal.Entities.Interactions.Components;

using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents an action row: a non-interactive container for other components.
/// </summary>
public record DiscordActionRowComponent : AbstractDiscordMessageComponent
{
	/// <summary>
	/// Up to five sub-components for this action row.
	/// </summary>
	[JsonPropertyName("components")]
	public IEnumerable<AbstractDiscordMessageComponent> Components { get; init; } = null!;
}

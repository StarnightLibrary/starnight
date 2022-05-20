namespace Starnight.Internal.Entities.Interactions.Components;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents an interactive message component.
/// </summary>
public abstract record AbstractInteractiveDiscordMessageComponent : AbstractDiscordMessageComponent
{
	/// <summary>
	/// A developer-defined ID for this component, max. 100 characters.
	/// </summary>
	[JsonPropertyName("custom_id")]
	public String? CustomId { get; init; }
}

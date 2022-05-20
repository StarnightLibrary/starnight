namespace Starnight.Internal.Entities.Interactions.Components;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a discord message component.
/// </summary>
public abstract record AbstractDiscordMessageComponent
{
	/// <summary>
	/// Type of this component.
	/// </summary>
	[JsonPropertyName("type")]
	public DiscordMessageComponentType Type { get; init; }
}

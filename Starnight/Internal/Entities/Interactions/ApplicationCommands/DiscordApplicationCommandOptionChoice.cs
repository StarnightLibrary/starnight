namespace Starnight.Internal.Entities.Interactions.ApplicationCommand;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a discord application option choice.
/// </summary>
public record DiscordApplicationCommandOptionChoice
{
	/// <summary>
	/// The display name of this option.
	/// </summary>
	[JsonPropertyName("name")]
	public String Name { get; init; } = default!;

	/// <summary>
	/// The value of this object, either a string, integer or floating-point number.
	/// </summary>
	[JsonPropertyName("value")]
	public Object Value { get; init; } = default!;
}

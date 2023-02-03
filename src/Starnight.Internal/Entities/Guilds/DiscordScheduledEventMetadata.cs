namespace Starnight.Internal.Entities.Guilds;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents metadata about a scheduled event.
/// </summary>
public sealed record DiscordScheduledEventMetadata
{
	/// <summary>
	/// Location of this event, 1 - 100 characters.
	/// </summary>
	[JsonPropertyName("location")]
	public Optional<String> Location { get; init; }
}

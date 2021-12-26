namespace Starnight.Internal.Entities.Guild;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents metadata about a scheduled event.
/// </summary>
public class DiscordScheduledEventMetadata
{
	/// <summary>
	/// Location of this event, 1 - 100 characters.
	/// </summary>
	[JsonPropertyName("location")]
	public String? Location { get; init; }
}

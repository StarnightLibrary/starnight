namespace Starnight.Internal.Entities.Messages;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a message activity.
/// </summary>
public record DiscordMessageActivity
{
	/// <summary>
	/// Type of this message activity.
	/// </summary>
	[JsonPropertyName("type")]
	public DiscordMessageActivityType Type { get; init; }

	/// <summary>
	/// Party ID from a rich presence event.
	/// </summary>
	[JsonPropertyName("party_id")]
	public String? PartyId { get; init; }
}

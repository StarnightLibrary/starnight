namespace Starnight.Internal.Entities.Messages;

using System;
using System.Text.Json.Serialization;

using Starnight.Entities;

/// <summary>
/// Represents a message activity.
/// </summary>
public sealed record DiscordMessageActivity
{
	/// <summary>
	/// Type of this message activity.
	/// </summary>
	[JsonPropertyName("type")]
	public required DiscordMessageActivityType Type { get; init; }

	/// <summary>
	/// Party ID from a rich presence event.
	/// </summary>
	[JsonPropertyName("party_id")]
	public Optional<String> PartyId { get; init; }
}

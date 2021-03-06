namespace Starnight.Internal.Gateway.Objects.User.Activity;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents an activity party.
/// </summary>
public record DiscordActivityParty
{
	/// <summary>
	/// The party ID.
	/// </summary>
	[JsonPropertyName("id")]
	public String? Id { get; init; }

	/// <summary>
	/// size[0]: current size of the party.
	/// <para/>
	/// size[1]: maximum size of the party.
	/// </summary>
	[JsonPropertyName("size")]
	public IEnumerable<Int32>? Size { get; init; }
}

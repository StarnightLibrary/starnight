namespace Starnight.Internal.Entities.Users.Activities;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents an activity party.
/// </summary>
public sealed record DiscordActivityParty
{
	/// <summary>
	/// The party ID.
	/// </summary>
	[JsonPropertyName("id")]
	public Optional<String> Id { get; init; }

	/// <summary>
	/// size[0]: current size of the party.
	/// <para/>
	/// size[1]: maximum size of the party.
	/// </summary>
	[JsonPropertyName("size")]
	public Optional<IEnumerable<Int32>> Size { get; init; }
}

namespace Starnight.Internal.Entities.Users.Activities;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the secrets 
/// </summary>
public sealed record DiscordActivitySecrets
{
	/// <summary>
	/// The secret used to join a party.
	/// </summary>
	[JsonPropertyName("join")]
	public Optional<String> JoinSecret { get; init; }

	/// <summary>
	/// The secret used to spectate a game.
	/// </summary>
	[JsonPropertyName("spectate")]
	public Optional<String> SpectateSecret { get; init; }

	/// <summary>
	/// The secret for a specific instanced match.
	/// </summary>
	[JsonPropertyName("match")]
	public Optional<String> MatchSecret { get; init; }
}

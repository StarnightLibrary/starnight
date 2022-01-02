namespace Starnight.Internal.Gateway.Objects.User.Activity;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the secrets 
/// </summary>
public record DiscordActivitySecrets
{
	/// <summary>
	/// The secret used to join a party.
	/// </summary>
	[JsonPropertyName("join")]
	public String? JoinSecret { get; init; }

	/// <summary>
	/// The secret used to spectate a game.
	/// </summary>
	[JsonPropertyName("spectate")]
	public String? SpectateSecret { get; init; }

	/// <summary>
	/// The secret for a specific instanced match.
	/// </summary>
	[JsonPropertyName("match")]
	public String? MatchSecret { get; init; }
}

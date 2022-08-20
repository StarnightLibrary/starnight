namespace Starnight.Internal.Entities.Guilds;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents an unavailable guild, a guild which is either unavailable or not yet sent to the client.
/// </summary>
public sealed record DiscordUnavailableGuild : DiscordSnowflakeObject
{
	/// <summary>
	/// Always false.
	/// </summary>
	[JsonPropertyName("unavailable")]
	public required Boolean Unavailable { get; init; }
}

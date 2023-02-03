namespace Starnight.Internal.Entities.Guilds;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents an unavailable guild, a guild which is either unavailable or not yet sent to the client.
/// </summary>
public sealed record DiscordUnavailableGuild : DiscordSnowflakeObject
{
	/// <summary>
	/// Indicates whether this guild is unavailable.
	/// </summary>
	/// <remarks>
	/// If this is sent as part of a GuildDeleted event, this indicates whether the guild is actually unavailable or
	/// whether the user has been removed from this guild.
	/// </remarks>
	[JsonPropertyName("unavailable")]
	public required Boolean Unavailable { get; init; }
}

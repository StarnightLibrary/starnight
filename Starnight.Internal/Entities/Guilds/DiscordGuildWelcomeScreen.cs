namespace Starnight.Internal.Entities.Guilds;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a welcome screen for this guild.
/// </summary>
public sealed record DiscordGuildWelcomeScreen
{
	/// <summary>
	/// The guild description shown in this welcome screen.
	/// </summary>
	[JsonPropertyName("description")]
	public String? Description { get; init; }

	/// <summary>
	/// The channels shown in this welcome screen, up to 5.
	/// </summary>
	[JsonPropertyName("welcome_channels")]
	public required IEnumerable<DiscordGuildWelcomeChannel> WelcomeChannels { get; init; }
}

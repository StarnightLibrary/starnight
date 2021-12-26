namespace Starnight.Internal.Entities.Guild;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a welcome screen for this guild.
/// </summary>
public class DiscordGuildWelcomeScreen
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
	public DiscordGuildWelcomeChannel[] WelcomeChannels { get; init; } = default!;
}

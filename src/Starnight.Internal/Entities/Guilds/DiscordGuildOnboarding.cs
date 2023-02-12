namespace Starnight.Internal.Entities.Guilds;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a guild onboarding object.
/// </summary>
public sealed record DiscordGuildOnboarding
{
	/// <summary>
	/// The ID of the guild this onboarding belongs to.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public required Int64 GuildId { get; init; }

	/// <summary>
	/// The prompts shown during onboarding and in the Customize Community screen.
	/// </summary>
	[JsonPropertyName("prompts")]
	public required IEnumerable<DiscordGuildOnboardingPrompt> Prompts { get; init; }

	/// <summary>
	/// The channels that members get opted into by default.
	/// </summary>
	[JsonPropertyName("default_channel_ids")]
	public required IEnumerable<Int64> DefaultChannelIds { get; init; }

	/// <summary>
	/// Whether guild onboarding is enabled.
	/// </summary>
	[JsonPropertyName("enabled")]
	public required Boolean Enabled { get; init; }
}

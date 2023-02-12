namespace Starnight.Internal.Entities.Guilds;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a prompt in a <see cref="DiscordGuildOnboarding"/>.
/// </summary>
public sealed record DiscordGuildOnboardingPrompt : DiscordSnowflakeObject
{
	/// <summary>
	/// The options available to this prompt.
	/// </summary>
	[JsonPropertyName("option")]
	public required IEnumerable<DiscordGuildOnboardingOption> Options { get; init; }

	/// <summary>
	/// The title of this prompt.
	/// </summary>
	[JsonPropertyName("title")]
	public required String Title { get; init; }

	/// <summary>
	/// Whether this prompt can only have a single option selected.
	/// </summary>
	[JsonPropertyName("single_select")]
	public required Boolean SingleSelect { get; init; }

	/// <summary>
	/// Whether this prompt is required in the onboarding flow.
	/// </summary>
	[JsonPropertyName("required")]
	public required Boolean Required { get; init; }

	/// <summary>
	/// Whether this prompt is a part of the onboarding flow.
	/// </summary>
	[JsonPropertyName("in_onboarding")]
	public required Boolean InOnboarding { get; init; }

	/// <summary>
	/// The type of this prompt.
	/// </summary>
	[JsonPropertyName("type")]
	public required DiscordGuildOnboardingPromptType Type { get; init; }
}

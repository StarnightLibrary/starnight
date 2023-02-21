namespace Starnight.Internal.Entities.Guilds.AutoModeration;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Entities;

/// <summary>
/// Holds additional data to determine whether a rule should be triggered.
/// </summary>
public sealed record DiscordAutoModerationTriggerMetadata
{
	/// <summary>
	/// Substrings which will be searched for in the filtered content.
	/// </summary>
	[JsonPropertyName("keyword_filter")]
	public Optional<IEnumerable<String>> KeywordFilter { get; init; }

	/// <summary>
	/// Internally pre-defined wordsets which will be searched for in the filtered content.
	/// </summary>
	[JsonPropertyName("presets")]
	public Optional<IEnumerable<DiscordAutoModerationKeywordPresetType>> Presets { get; init; }

	/// <summary>
	/// Substrings which will be exempt from triggering the rule.
	/// </summary>
	[JsonPropertyName("allow_list")]
	public Optional<IEnumerable<String>> AllowList { get; init; }
}

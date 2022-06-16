namespace Starnight.Internal.Entities.Guilds.AutoModeration;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Holds additional data to determine whether a rule should be triggered.
/// </summary>
public record DiscordAutoModerationTriggerMetadata
{
	/// <summary>
	/// Substrings which will be searched for in the filtered content.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("keyword_filter")]
	public IEnumerable<String>? KeywordFilter { get; init; }

	/// <summary>
	/// Internally pre-defined wordsets which will be searched for in the filtered content.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("presets")]
	public IEnumerable<DiscordAutoModerationKeywordPresetType>? Presets { get; init; }
}

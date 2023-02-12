namespace Starnight.Internal.Entities.Guilds;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a single option in a <see cref="DiscordGuildOnboardingPrompt"/>.
/// </summary>
public sealed record DiscordGuildOnboardingOption : DiscordSnowflakeObject
{
	/// <summary>
	/// The channels the user opts into when selecting this option.
	/// </summary>
	[JsonPropertyName("channel_ids")]
	public required IEnumerable<Int64> ChannelIds { get; init; }

	/// <summary>
	/// The roles the user is assigned when selecting this option.
	/// </summary>
	[JsonPropertyName("role_ids")]
	public required IEnumerable<Int64> RoleIds { get; init; }

	/// <summary>
	/// The emoji ID, if the emoji used is a custom emoji.
	/// </summary>
	[JsonPropertyName("emoji_id")]
	public Int64? EmojiId { get; init; }

	/// <summary>
	/// The emoji name if custom, the unicode character if standard, or <c>null</c> if no emoji is set.
	/// </summary>
	[JsonPropertyName("emoji_name")]
	public String? EmojiName { get; init; }

	/// <summary>
	/// The title of this option.
	/// </summary>
	[JsonPropertyName("title")]
	public required String Title { get; init; }

	/// <summary>
	/// The description of this option.
	/// </summary>
	[JsonPropertyName("description")]
	public String? Description { get; init; }
}

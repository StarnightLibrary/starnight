namespace Starnight.Internal.Entities.Channels.Threads;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the default emoji reaction to a forum post. Exactly one field must be set.
/// </summary>
public sealed record DiscordDefaultForumReaction
{
	/// <summary>
	/// The snowflake ID of a custom emoji.
	/// </summary>
	[JsonPropertyName("emoji_id")]
	public Int64? EmojiId { get; init; }

	/// <summary>
	/// The unicode representation of the emoji.
	/// </summary>
	[JsonPropertyName("emoji_name")]
	public String? EmojiName { get; init; }
}

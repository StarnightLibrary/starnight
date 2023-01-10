namespace Starnight.Internal.Entities.Channels.Threads;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a forum channel tag.
/// </summary>
public sealed record DiscordForumTag : DiscordSnowflakeObject
{
	/// <summary>
	/// The name of this tag.
	/// </summary>
	[JsonPropertyName("name")]
	public required String Name { get; init; }

	/// <summary>
	/// Indicates whether this tag can only be added to or removed from threads by a member with the
	/// <seealso cref="DiscordPermissions.ManageThreads"/> permission.
	/// </summary>
	[JsonPropertyName("moderated")]
	public required Boolean Moderated { get; init; }

	/// <summary>
	/// Snowflake ID of the custom emoji associated with this tag.
	/// </summary>
	[JsonPropertyName("emoji_id")]
	public Int64? EmojiId { get; init; }

	/// <summary>
	/// The unicode character representation of the emoji associated with this tag.
	/// </summary>
	[JsonPropertyName("emoji_name")]
	public String? EmojiName { get; init; }
}

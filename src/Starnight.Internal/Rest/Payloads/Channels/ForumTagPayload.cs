namespace Starnight.Internal.Rest.Payloads.Channels;

using System;
using System.Text.Json.Serialization;

using Starnight.Entities;

/// <summary>
/// Represents a new tag object.
/// </summary>
public sealed record ForumTagPayload
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
	public Optional<Boolean> Moderated { get; init; }

	/// <summary>
	/// Snowflake ID of the custom emoji associated with this tag.
	/// </summary>
	[JsonPropertyName("emoji_id")]
	public Optional<Int64> EmojiId { get; init; }

	/// <summary>
	/// The unicode character representation of the emoji associated with this tag.
	/// </summary>
	[JsonPropertyName("emoji_name")]
	public Optional<String?> EmojiName { get; init; }
}

namespace Starnight.Internal.Entities.Guilds;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a channel in a <see cref="DiscordGuildWelcomeScreen"/>.
/// </summary>
public record DiscordGuildWelcomeChannel
{
	/// <summary>
	/// The target channel's snowflake ID.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("channel_id")]
	public Int64 ChannelId { get; init; }

	/// <summary>
	/// Description shown for this channel.
	/// </summary>
	[JsonPropertyName("description")]
	public String Description { get; init; } = default!;

	/// <summary>
	/// The emoji ID if this description uses a custom emoji.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("emoji_id")]
	public Int64? EmojiId { get; init; }

	/// <summary>
	/// The emoji name if custom; the unicode character if standard; or <c>null</c> if no emoji is set.
	/// </summary>
	[JsonPropertyName("emoji_name")]
	public String? EmojiName { get; init; }
}

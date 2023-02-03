namespace Starnight.Internal.Gateway.Payloads.Inbound.Dispatch;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guilds;

/// <summary>
/// Represents a payload for an EmojiMessageReactionsRemoved event.
/// </summary>
public sealed record EmojiMessageReactionsRemovedPayload
{
	/// <summary>
	/// The ID of the channel this occurred in.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public required Int64 ChannelId { get; init; }

	/// <summary>
	/// The ID of the guild this occurred in, if applicable.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public Optional<Int64> GuildId { get; init; }

	/// <summary>
	/// The ID of the message whose reactions were removed.
	/// </summary>
	[JsonPropertyName("message_id")]
	public required Int64 MessageId { get; init; }

	/// <summary>
	/// The emoji which was removed from the message.
	/// </summary>
	[JsonPropertyName("emoji")]
	public required DiscordEmoji Emoji { get; init; }
}

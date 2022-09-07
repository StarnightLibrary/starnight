namespace Starnight.Internal.Gateway.Payloads.Inbound.Dispatch;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guilds;

/// <summary>
/// Represents a MessageReactionRemoved event.
/// </summary>
public sealed record MessageReactionRemovedPayload
{
	/// <summary>
	/// The ID of the user whose reaction was removed.
	/// </summary>
	[JsonPropertyName("user_id")]
	public required Int64 UserId { get; init; }

	/// <summary>
	/// The ID of the channel this occurred in.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public required Int64 ChannelId { get; init; }

	/// <summary>
	/// The ID of the message in question.
	/// </summary>
	[JsonPropertyName("message_id")]
	public required Int64 MessageId { get; init; }

	/// <summary>
	/// The ID of the guild this occurred in, if applicable.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public Optional<Int64> GuildId { get; init; }

	/// <summary>
	/// The emoji this reaction used.
	/// </summary>
	[JsonPropertyName("emoji")]
	public required DiscordEmoji Emoji { get; init; }
}

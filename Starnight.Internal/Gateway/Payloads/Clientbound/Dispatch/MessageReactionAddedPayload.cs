namespace Starnight.Internal.Gateway.Payloads.Clientbound.Dispatch;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guilds;

/// <summary>
/// Represents the payload for a MessageReactionAdded event.
/// </summary>
public sealed record MessageReactionAddedPayload
{
	/// <summary>
	/// The ID of the user who reacted.
	/// </summary>
	[JsonPropertyName("user_id")]
	public required Int64 UserId { get; init; }

	/// <summary>
	/// The ID of the channel this reaction occurred in.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public required Int64 ChannelId { get; init; }

	/// <summary>
	/// The ID of the message in question.
	/// </summary>
	[JsonPropertyName("message_id")]
	public required Int64 MessageId { get; init; }

	/// <summary>
	/// The ID of the guild, if applicable.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public Optional<Int64> GuildId { get; init; }

	/// <summary>
	/// The member object associated with the user who reacted, if applicable.
	/// </summary>
	[JsonPropertyName("member")]
	public Optional<DiscordGuildMember> Member { get; init; }

	/// <summary>
	/// The emoji used to react.
	/// </summary>
	[JsonPropertyName("emoji")]
	public required DiscordEmoji Emoji { get; init; }
}

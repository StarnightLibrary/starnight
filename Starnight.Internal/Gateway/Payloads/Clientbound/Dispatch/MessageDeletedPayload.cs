namespace Starnight.Internal.Gateway.Payloads.Clientbound.Dispatch;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the payload for a MessageDeleted event.
/// </summary>
public sealed record MessageDeletedPayload
{
	/// <summary>
	/// The ID of the message.
	/// </summary>
	[JsonPropertyName("id")]
	public required Int64 MessageId { get; init; }

	/// <summary>
	/// The ID of the channel.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public required Int64 ChannelId { get; init; }

	/// <summary>
	/// The ID of the guild.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public Optional<Int64> GuildId { get; init; }
}

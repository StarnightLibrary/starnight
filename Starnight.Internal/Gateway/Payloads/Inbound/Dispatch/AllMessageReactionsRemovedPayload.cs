namespace Starnight.Internal.Gateway.Payloads.Inbound.Dispatch;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a payload for an AllMessageReactionsRemoved event.
/// </summary>
public sealed record AllMessageReactionsRemovedPayload
{
	/// <summary>
	/// The ID of the channel this occurred in.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public required Int64 ChannelId { get; init; }

	/// <summary>
	/// The ID of the message whose reactions were cleared.
	/// </summary>
	[JsonPropertyName("message_id")]
	public required Int64 MessageId { get; init; }

	/// <summary>
	/// The ID of the guild this occurred in, if applicable.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public Optional<Int64> GuildId { get; init; }
}

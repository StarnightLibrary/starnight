namespace Starnight.Internal.Gateway.Payloads.Inbound.Dispatch;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the payload for a WebhooksUpdated event.
/// </summary>
public sealed record WebhooksUpdatedPayload
{
	/// <summary>
	/// The ID of the guild this occurred in.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public required Int64 GuildId { get; init; }

	/// <summary>
	/// The channel whose webhooks were updated.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public required Int64 ChannelId { get; init; }
}

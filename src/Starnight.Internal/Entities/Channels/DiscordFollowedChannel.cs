namespace Starnight.Internal.Entities.Channels;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a followed announcement channel.
/// </summary>
public sealed record DiscordFollowedChannel
{
	/// <summary>
	/// Snowflake identifier of the source channel.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public required Int64 ChannelId { get; init; }

	/// <summary>
	/// Snowflake identifier of the created target webhook.
	/// </summary>
	[JsonPropertyName("webhook_id")]
	public required Int64 WebhookId { get; init; }
}

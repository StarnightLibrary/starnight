namespace Starnight.Internal.Entities.Channel;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a followed announcement channel.
/// </summary>
public class DiscordFollowedChannel
{
	/// <summary>
	/// Snowflake identifier of the source channel.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("channel_id")]
	public Int64 ChannelId { get; init; }

	/// <summary>
	/// Snowflake identifier of the created target webhook.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("webhook_id")]
	public Int64 WebhookId { get; init; }
}

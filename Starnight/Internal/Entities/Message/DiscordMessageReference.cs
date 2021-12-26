namespace Starnight.Internal.Entities.Message;

using System.Text.Json.Serialization;
using System;

/// <summary>
/// Represents data about a referenced message.
/// </summary>
public class DiscordMessageReference
{
	/// <summary>
	/// Snowflake identifier of the original message.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("message_id")]
	public Int64? MessageId { get; init; }

	/// <summary>
	/// Snowflake identifier of the original channel.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("channel_id")]
	public Int64? ChannelId { get; init; }

	/// <summary>
	/// Snowflake identifier of the original guild.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("guild_id")]
	public Int64? GuildId { get; init; }

	/// <summary>
	/// Fail-hard if the referenced message does not exist. Defaults to true.
	/// </summary>
	[JsonPropertyName("fail_if_not_exists")]
	public Boolean? Failhard { get; init; } = true;
}

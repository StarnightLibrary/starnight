namespace Starnight.Internal.Entities.Messages;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents data about a referenced message.
/// </summary>
public sealed record DiscordMessageReference
{
	/// <summary>
	/// Snowflake identifier of the original message.
	/// </summary>
	[JsonPropertyName("message_id")]
	public Optional<Int64> MessageId { get; init; }

	/// <summary>
	/// Snowflake identifier of the original channel.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public Optional<Int64> ChannelId { get; init; }

	/// <summary>
	/// Snowflake identifier of the original guild.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public Optional<Int64> GuildId { get; init; }

	/// <summary>
	/// Fail-hard if the referenced message does not exist. Defaults to true.
	/// </summary>
	[JsonPropertyName("fail_if_not_exists")]
	public Optional<Boolean> Failhard { get; init; }
}

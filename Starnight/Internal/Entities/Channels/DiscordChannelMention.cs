namespace Starnight.Internal.Entities.Channels;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a discord channel mention object.
/// </summary>
public record DiscordChannelMention : DiscordSnowflakeObject
{
	/// <summary>
	/// The guild containing the channel.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("guild_id")]
	public Int64 GuildId { get; init; }

	/// <summary>
	/// The type of the mentioned channel.
	/// </summary>
	[JsonPropertyName("type")]
	public DiscordChannelType ChannelType { get; init; }

	/// <summary>
	/// The name of the mentioned channel.
	/// </summary>
	[JsonPropertyName("name")]
	public String Name { get; init; } = default!;
}

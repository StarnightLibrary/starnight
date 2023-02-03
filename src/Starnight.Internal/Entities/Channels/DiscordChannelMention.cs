namespace Starnight.Internal.Entities.Channels;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a discord channel mention object.
/// </summary>
public sealed record DiscordChannelMention : DiscordSnowflakeObject
{
	/// <summary>
	/// The guild containing the channel.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public required Int64 GuildId { get; init; }

	/// <summary>
	/// The type of the mentioned channel.
	/// </summary>
	[JsonPropertyName("type")]
	public required DiscordChannelType ChannelType { get; init; }

	/// <summary>
	/// The name of the mentioned channel.
	/// </summary>
	[JsonPropertyName("name")]
	public required String Name { get; init; } = default!;
}

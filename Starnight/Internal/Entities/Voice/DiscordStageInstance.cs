namespace Starnight.Internal.Entities.Voice;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a stage channel instance.
/// </summary>
public class DiscordStageInstance : DiscordSnowflakeObject
{
	/// <summary>
	/// The guild ID of the associated stage channel.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("guild_id")]
	public Int64 GuildId { get; init; }

	/// <summary>
	/// The channel ID of the associated stage channel.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("channel_id")]
	public Int64 ChannelId { get; init; }

	/// <summary>
	/// The topic of this stage instance, 1 - 120 characters.
	/// </summary>
	[JsonPropertyName("topic")]
	public String Topic { get; init; } = default!;

	/// <summary>
	/// The privacy level of this stage instance.
	/// </summary>
	[JsonPropertyName("privacy_level")]
	public DiscordStagePrivacyLevel PrivacyLevel { get; init; }

	/// <summary>
	/// Whether this stage is hidden from stage discovery.
	/// </summary>
	[JsonPropertyName("discoverable_disabled")]
	public Boolean DiscoveryDisabled { get; init; }
}

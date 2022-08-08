namespace Starnight.Internal.Entities.Voice;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a stage channel instance.
/// </summary>
public sealed record DiscordStageInstance : DiscordSnowflakeObject
{
	/// <summary>
	/// The guild ID of the associated stage channel.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public required Int64 GuildId { get; init; }

	/// <summary>
	/// The channel ID of the associated stage channel.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public required Int64 ChannelId { get; init; }

	/// <summary>
	/// The topic of this stage instance, 1 - 120 characters.
	/// </summary>
	[JsonPropertyName("topic")]
	public required String Topic { get; init; } 

	/// <summary>
	/// The privacy level of this stage instance.
	/// </summary>
	[JsonPropertyName("privacy_level")]
	public required DiscordStagePrivacyLevel PrivacyLevel { get; init; }

	/// <summary>
	/// The ID of the scheduled event corresponding to this stage instance.
	/// </summary>
	[JsonPropertyName("guild_scheduled_event_id")]
	public Int64? GuildScheduledEventId { get; init; }
}

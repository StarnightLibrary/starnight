namespace Starnight.Internal.Rest.Payloads.Guilds;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a payload to PATCH /guilds/:guild_id/voice-states/@me.
/// </summary>
public sealed record ModifyCurrentUserVoiceStateRequestPayload
{
	/// <summary>
	/// Snowflake identifier of the channel this user is currently in.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public required Int64 ChannelId { get; init; }

	/// <summary>
	/// Toggles this user's suppression state.
	/// </summary>
	[JsonPropertyName("suppress")]
	public Optional<Boolean> SuppressUser { get; init; }

	/// <summary>
	/// Sets this user's speaking request in a stage channel.
	/// </summary>
	[JsonPropertyName("request_to_speak_timestamp")]
	public Optional<DateTimeOffset?> SpeakingRequestTimestamp { get; init; }
}

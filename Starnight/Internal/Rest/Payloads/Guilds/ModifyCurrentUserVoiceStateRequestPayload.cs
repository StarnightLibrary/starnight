namespace Starnight.Internal.Rest.Payloads.Guilds;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a payload to PATCH /guilds/:guild_id/voice-states/@me.
/// </summary>
public record ModifyCurrentUserVoiceStateRequestPayload
{
	/// <summary>
	/// Snowflake identifier of the channel this user is currently in.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("channel_id")]
	public Int64 ChannelId { get; init; }

	/// <summary>
	/// Toggles this user's suppression state.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("suppress")]
	public Boolean? SuppressUser { get; init; }

	/// <summary>
	/// Sets this user's speaking request in a stage channel.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("request_to_speak_timestamp")]
	public DateTimeOffset SpeakingRequestTimestamp { get; init; }
}

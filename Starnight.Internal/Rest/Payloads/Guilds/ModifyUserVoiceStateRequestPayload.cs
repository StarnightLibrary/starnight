namespace Starnight.Internal.Rest.Payloads.Guilds;

using System.Text.Json.Serialization;
using System;

public record ModifyUserVoiceStateRequestPayload
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
}

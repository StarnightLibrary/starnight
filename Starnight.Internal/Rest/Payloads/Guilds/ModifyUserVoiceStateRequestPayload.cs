namespace Starnight.Internal.Rest.Payloads.Guilds;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a payload to PATCH /guilds/:guild_id/voice-states/:user_id.
/// </summary>
public sealed record ModifyUserVoiceStateRequestPayload
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
}

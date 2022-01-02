namespace Starnight.Internal.Entities.Voice;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guilds;

/// <summary>
/// Represents a user's voice connection status.
/// </summary>
public class DiscordVoiceState
{
	/// <summary>
	/// The guild this user is connected to.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("guild_id")]
	public Int64? GuildId { get; init; }

	/// <summary>
	/// The channel this user is connected to.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("channel_id")]
	public Int64? ChannelId { get; init; }

	/// <summary>
	/// The user ID of this user.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("user_id")]
	public Int64 UserId { get; init; }

	/// <summary>
	/// The guild member object for this user in the specified guild.
	/// </summary>
	[JsonPropertyName("member")]
	public DiscordGuildMember? Member { get; init; }

	/// <summary>
	/// The session ID for this voice state.
	/// </summary>
	[JsonPropertyName("session_id")]
	public String SessionId { get; init; } = default!;

	/// <summary>
	/// Whether this user is server-deafened.
	/// </summary>
	[JsonPropertyName("deaf")]
	public Boolean Deafened { get; init; }

	/// <summary>
	/// Whether this user is server-muted.
	/// </summary>
	[JsonPropertyName("mute")]
	public Boolean Muted { get; init; }

	/// <summary>
	/// Whether this user has deafened themselves.
	/// </summary>
	[JsonPropertyName("self_deaf")]
	public Boolean SelfDeafened { get; init; }

	/// <summary>
	/// Whether this user has muted themselves.
	/// </summary>
	[JsonPropertyName("self_mute")]
	public Boolean SelfMuted { get; init; }

	/// <summary>
	/// Whether this user is streaming to the current voice channel.
	/// </summary>
	[JsonPropertyName("self_stream")]
	public Boolean? Streaming { get; init; }

	/// <summary>
	/// Whether this user's camera is enabled.
	/// </summary>
	[JsonPropertyName("self_video")]
	public Boolean Video { get; init; }

	/// <summary>
	/// Whether this user is muted by the current user.
	/// </summary>
	[JsonPropertyName("suppress")]
	public Boolean Suppressed { get; init; }

	/// <summary>
	/// The time at which this user requested to speak in this stage channel.
	/// </summary>
	[JsonPropertyName("request_to_speak_timestamp")]
	public DateTime? SpeakingRequestTimestamp { get; init; }
}

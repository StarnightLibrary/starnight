namespace Starnight.Internal.Gateway.Objects.Serverbound;

using System;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a payload for an Update Voice State command.
/// </summary>
[StructLayout(LayoutKind.Auto)]
public record struct DiscordUpdateVoiceStateCommandObject
{
	/// <summary>
	/// Snowflake ID of the guild you wish to change voice state in.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public required Int64 GuildId { get; init; }

	/// <summary>
	/// Snowflake ID of the voice channel you want to join, or <see langword="null"/> to disconnect.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public Int64? ChannelId { get; init; }

	/// <summary>
	/// Whether the client is muted from its side.
	/// </summary>
	[JsonPropertyName("self_mute")]
	public required Boolean Muted { get; init; }

	/// <summary>
	/// Whether the client is deafened from its side.
	/// </summary>
	[JsonPropertyName("self_defa")]
	public required Boolean Deafened { get; init; }
}

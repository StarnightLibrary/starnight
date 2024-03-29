namespace Starnight.Internal.Entities.Guilds;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Users;

/// <summary>
/// Represents a guild widget.
/// </summary>
public sealed record DiscordGuildWidget : DiscordSnowflakeObject
{
	/// <summary>
	/// Name of the guild this widget represents.
	/// </summary>
	[JsonPropertyName("name")]
	public required String Name { get; init; }

	/// <summary>
	/// Instant invite code for the guild's specified widget invite channel.
	/// </summary>
	[JsonPropertyName("instant_invite")]
	public String? InstantInvite { get; init; }

	/// <summary>
	/// Voice and stage channels accessible to everyone. 
	/// </summary>
	[JsonPropertyName("channels")]
	public required IEnumerable<DiscordChannel> VoiceChannels { get; init; }

	/// <summary>
	/// Up to 100 special widget users. IDs, discriminators and avatars are anonymized.
	/// </summary>
	[JsonPropertyName("members")]
	public required IEnumerable<DiscordUser> Users { get; init; }

	/// <summary>
	/// Approximate amount of online users in this guild.
	/// </summary>
	[JsonPropertyName("presence_count")]
	public required Int32 OnlineUserCount { get; init; }
}

namespace Starnight.Internal.Gateway.Payloads.Inbound.Dispatch;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guilds;

/// <summary>
/// Represents the payload of a GuildEmojisUpdated event.
/// </summary>
public sealed record GuildEmojisUpdatedPayload
{
	/// <summary>
	/// Snowflake ID of the guild this is taking place in.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public required Int64 GuildId { get; init; }

	/// <summary>
	/// The updated emojis.
	/// </summary>
	[JsonPropertyName("emojis")]
	public required IEnumerable<DiscordEmoji> Emojis { get; init; }
}

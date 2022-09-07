namespace Starnight.Internal.Gateway.Payloads.Inbound.Dispatch;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Users;

/// <summary>
/// Represents the payload for a GuildBanRemoved event.
/// </summary>
public sealed record GuildBanRemovedPayload
{
	/// <summary>
	/// Snowflake ID of the guild this ban was removed from.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public required Int64 GuildId { get; init; }

	/// <summary>
	/// The newly unbanned user.
	/// </summary>
	[JsonPropertyName("user")]
	public required DiscordUser User { get; init; }
}

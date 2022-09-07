namespace Starnight.Internal.Gateway.Payloads.Inbound.Dispatch;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Users;

/// <summary>
/// Represents the payload for the GuildMemberRemoved event.
/// </summary>
public sealed record GuildMemberRemovedPayload
{
	/// <summary>
	/// The ID of the guild this is taking place in.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public required Int64 GuildId { get; init; }

	/// <summary>
	/// The user who was removed from the guild.
	/// </summary>
	[JsonPropertyName("user")]
	public required DiscordUser User { get; init; }
}

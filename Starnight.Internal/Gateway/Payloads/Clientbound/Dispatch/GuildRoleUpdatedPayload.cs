namespace Starnight.Internal.Gateway.Payloads.Clientbound.Dispatch;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guilds;

/// <summary>
/// Represents the payload for the GuildRoleUpdated event.
/// </summary>
public sealed record GuildRoleUpdatedPayload
{
	/// <summary>
	/// The ID of the guild.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public required Int64 GuildId { get; init; }

	/// <summary>
	/// The updated role.
	/// </summary>
	[JsonPropertyName("role")]
	public required DiscordRole Role { get; init; }
}

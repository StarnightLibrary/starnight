namespace Starnight.Internal.Gateway.Payloads.Inbound.Dispatch;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guilds;

/// <summary>
/// Represents the payload for the GuildRoleCreated event.
/// </summary>
public sealed record GuildRoleCreatedPayload
{
	/// <summary>
	/// The ID of the guild.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public required Int64 GuildId { get; init; }

	/// <summary>
	/// The newly created role.
	/// </summary>
	[JsonPropertyName("role")]
	public required DiscordRole Role { get; init; }
}

namespace Starnight.Internal.Entities.Guilds;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Users;

/// <summary>
/// Represents a guild ban.
/// </summary>
public sealed record DiscordGuildBan
{
	/// <summary>
	/// Audit log reason for this ban.
	/// </summary>
	[JsonPropertyName("reason")]
	public String? Reason { get; init; }

	/// <summary>
	/// The user affected by this ban.
	/// </summary>
	[JsonPropertyName("user")]
	public required DiscordUser User { get; init; }
}

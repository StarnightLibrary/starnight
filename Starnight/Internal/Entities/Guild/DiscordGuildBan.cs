namespace Starnight.Internal.Entities.Guild;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.User;

/// <summary>
/// Represents a guild ban.
/// </summary>
public class DiscordGuildBan
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
	public DiscordUser User { get; init; } = default!;
}

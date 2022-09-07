namespace Starnight.Internal.Entities.Users;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the statuses of this user's current platforms.
/// </summary>
public sealed record DiscordClientStatus
{
	/// <summary>
	/// The user's status set for an active desktop session.
	/// </summary>
	[JsonPropertyName("desktop")]
	public Optional<String> Desktop { get; init; }

	/// <summary>
	/// The user's status set for an active mobile session.
	/// </summary>
	[JsonPropertyName("mobile")]
	public Optional<String> Mobile { get; init; }

	/// <summary>
	/// The user's status set for an active web session.
	/// </summary>
	[JsonPropertyName("web")]
	public Optional<String> Web { get; init; }
}

namespace Starnight.Internal.Gateway.Objects.User;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the status of each active application session.
/// </summary>
public class DiscordClientStatusObject
{
	/// <summary>
	/// User status for an active desktop session.
	/// </summary>
	[JsonPropertyName("desktop")]
	public String? Desktop { get; init; }

	/// <summary>
	/// User status for an active mobile session.
	/// </summary>
	[JsonPropertyName("mobile")]
	public String? Mobile { get; init; }

	/// <summary>
	/// User status for an active web session.
	/// </summary>
	[JsonPropertyName("web")]
	public String? Web { get; init; }
}

namespace Starnight.Internal.Entities.Users;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Users.Activities;

/// <summary>
/// Represents a payload for a Presence Update command.
/// </summary>
public sealed record DiscordPresence
{
	/// <summary>
	/// Unix time in millisecondsof when the client went idle, or null if the client is not idle.
	/// </summary>
	[JsonPropertyName("since")]
	public Int64? Since { get; init; }

	/// <summary>
	/// The user's activities.
	/// </summary>
	[JsonPropertyName("activities")]
	public required IEnumerable<DiscordActivity> Activities { get; init; }

	/// <summary>
	/// The user's new status.
	/// </summary>
	[JsonPropertyName("status")]
	public required String Status { get; init; }

	/// <summary>
	/// Whether or not the client is AFK.
	/// </summary>
	[JsonPropertyName("afk")]
	public required Boolean Afk { get; init; }
}

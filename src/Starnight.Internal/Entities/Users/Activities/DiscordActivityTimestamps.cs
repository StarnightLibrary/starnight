namespace Starnight.Internal.Entities.Users.Activities;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the timestamps associated with an activity.
/// </summary>
public sealed record DiscordActivityTimestamps
{
	/// <summary>
	/// Unix time in milliseconds of when this activity started.
	/// </summary>
	[JsonPropertyName("start")]
	public Optional<Int32> Start { get; init; }

	/// <summary>
	/// Unix time in milliseconds of when this activity will end.
	/// </summary>
	[JsonPropertyName("end")]
	public Optional<Int32> End { get; init; }
}

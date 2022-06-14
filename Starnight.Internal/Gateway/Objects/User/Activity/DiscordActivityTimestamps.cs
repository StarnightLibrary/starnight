namespace Starnight.Internal.Gateway.Objects.User.Activity;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the timestamps associated with an activity.
/// </summary>
public record DiscordActivityTimestamps
{
	/// <summary>
	/// Unix time in milliseconds of when this activity started.
	/// </summary>
	[JsonPropertyName("start")]
	public Int32? Start { get; init; }

	/// <summary>
	/// Unix time in milliseconds of when this activity will end.
	/// </summary>
	[JsonPropertyName("end")]
	public Int32? End { get; init; }
}

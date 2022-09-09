namespace Starnight.Internal.Gateway.Payloads;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents Discord's session start limit object.
/// </summary>
public sealed record DiscordSessionStartLimit
{
	/// <summary>
	/// The total number of session starts permitted to the current user.
	/// </summary>
	[JsonPropertyName("total")]
	public required Int32 Total { get; init; }

	/// <summary>
	/// The remaining number of session starts the current user is allowed.
	/// </summary>
	[JsonPropertyName("remaining")]
	public required Int32 Remaining { get; init; }

	/// <summary>
	/// The number of milliseconds after which the limit resets.
	/// </summary>
	[JsonPropertyName("reset_after")]
	public required Int32 ResetAfter { get; init; }

	/// <summary>
	/// The number of identify requests allowed per five seconds.
	/// </summary>
	[JsonPropertyName("max_concurrency")]
	public required Int32 MaxConcurrency { get; init; }
}

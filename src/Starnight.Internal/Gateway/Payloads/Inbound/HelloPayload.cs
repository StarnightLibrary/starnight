namespace Starnight.Internal.Gateway.Payloads.Inbound;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the payload to a hello event
/// </summary>
public sealed record HelloPayload
{
	/// <summary>
	/// The interval, in milliseconds, the client should heartbeat with.
	/// </summary>
	[JsonPropertyName("heartbeat_interval")]
	public required Int32 HeartbeatInterval { get; init; }
}

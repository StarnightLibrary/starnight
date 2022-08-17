namespace Starnight.Internal.Gateway.Objects.Clientbound;

using System;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the payload to a hello event
/// </summary>
[StructLayout(LayoutKind.Auto)]
public record struct DiscordHelloEventObject
{
	/// <summary>
	/// The interval, in milliseconds, the client should heartbeat with.
	/// </summary>
	[JsonPropertyName("heartbeat_interval")]
	public required Int32 HeartbeatInterval { get; init; }
}

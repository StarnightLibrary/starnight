namespace Starnight.Internal.Gateway.Events.Control;

using System;

/// <summary>
/// Represents a control event fired when the gateway considers the connection to be zombied.
/// </summary>
public sealed record ZombiedEvent : IGatewayEvent
{
	/// <summary>
	/// Gets the last sequence ID received, for debugging purposes.
	/// </summary>
	public required Int32 LastReceivedSequence { get; init; }
}

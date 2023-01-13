namespace Starnight.Internal.Gateway.Events.Outbound;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Gateway.Events;

/// <summary>
/// Represents an outbound heartbeat event.
/// </summary>
public sealed record DiscordOutboundHeartbeatEvent : IDiscordGatewayEvent<Int32>
{
	/// <summary>
	/// The last sequence number received by the client.
	/// </summary>
	[JsonPropertyName("d")]
	public required Int32 Data { get; set; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public required DiscordGatewayOpcode Opcode { get; set; }
}

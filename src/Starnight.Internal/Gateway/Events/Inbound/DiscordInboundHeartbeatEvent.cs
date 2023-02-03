namespace Starnight.Internal.Gateway.Events.Inbound;

using System.Text.Json.Serialization;

/// <summary>
/// Fired by the gateway to indicate that a heartbeat has been acknowledged.
/// </summary>
public sealed record DiscordInboundHeartbeatEvent : IDiscordGatewayEvent
{
	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public required DiscordGatewayOpcode Opcode { get; set; }
}

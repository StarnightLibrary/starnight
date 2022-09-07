namespace Starnight.Internal.Gateway.Events.Inbound;

using System.Text.Json.Serialization;

/// <summary>
/// Indicates to the client that it should reconnect.
/// </summary>
public sealed record DiscordReconnectEvent : IDiscordGatewayEvent
{
	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public required DiscordGatewayOpcode Opcode { get; init; }
}

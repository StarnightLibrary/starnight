namespace Starnight.Internal.Gateway.Events.Inbound;

using System.Text.Json.Serialization;

using Starnight.Internal.Gateway.Payloads.Inbound;

/// <summary>
/// Represents a gateway Hello event.
/// </summary>
public sealed record DiscordHelloEvent : IDiscordGatewayEvent<HelloPayload>
{
	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required HelloPayload Data { get; set; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public required DiscordGatewayOpcode Opcode { get; set; }
}

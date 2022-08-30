namespace Starnight.Internal.Gateway.Events;

using System.Text.Json.Serialization;

using Starnight.Internal.Gateway;
using Starnight.Internal.Gateway.Payloads.Clientbound;

/// <summary>
/// Represents a gateway Hello event.
/// </summary>
public sealed record DiscordHelloEvent : IDiscordGatewayPayload<HelloPayload>
{
	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required HelloPayload Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public required DiscordGatewayOpcode Opcode { get; init; }
}

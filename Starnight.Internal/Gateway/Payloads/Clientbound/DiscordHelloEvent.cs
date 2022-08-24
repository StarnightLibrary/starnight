namespace Starnight.Internal.Gateway.Payloads.Clientbound;

using System.Text.Json.Serialization;

using Starnight.Internal.Gateway.Objects.Clientbound;

/// <summary>
/// Represents a gateway Hello event.
/// </summary>
public sealed record DiscordHelloEvent : IDiscordGatewayPayload<DiscordHelloEventObject>
{
	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required DiscordHelloEventObject Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public required DiscordGatewayOpcode Opcode { get; init; }
}

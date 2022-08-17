namespace Starnight.Internal.Gateway.Payloads.Clientbound;

using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

using Starnight.Internal.Gateway.Objects.Clientbound;

/// <summary>
/// Represents a gateway Hello event.
/// </summary>
[StructLayout(LayoutKind.Auto)]
public record struct DiscordGatewayHelloEvent : IDiscordGatewayPayload<DiscordHelloEventObject>
{
	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required DiscordHelloEventObject Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public DiscordGatewayOpcode Opcode { get; init; }
}

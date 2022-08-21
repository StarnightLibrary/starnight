namespace Starnight.Internal.Gateway.Payloads.Clientbound;

using System;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

/// <summary>
/// Indicates to the client that it should reconnect.
/// </summary>
[StructLayout(LayoutKind.Auto)]
public record struct DiscordReconnectGatewayEvent : IDiscordGatewayPayload
{
	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public DiscordGatewayOpcode Opcode { get; init; }
}

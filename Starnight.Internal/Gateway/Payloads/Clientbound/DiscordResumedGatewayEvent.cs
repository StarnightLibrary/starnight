namespace Starnight.Internal.Gateway.Payloads.Clientbound;

using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

/// <summary>
/// Dispatched if resumption was successful.
/// </summary>
[StructLayout(LayoutKind.Auto)]
public record struct DiscordResumedGatewayEvent : IDiscordGatewayPayload
{
	[JsonPropertyName("op")]
	public DiscordGatewayOpcode Opcode { get; init; }
}

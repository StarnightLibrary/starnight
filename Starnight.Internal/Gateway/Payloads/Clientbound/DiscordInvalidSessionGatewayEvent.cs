namespace Starnight.Internal.Gateway.Payloads.Clientbound;

using System;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

/// <summary>
/// Indicates to the client that the session is invalid, and that it may need to reconnect.
/// </summary>
[StructLayout(LayoutKind.Auto)]
public record struct DiscordInvalidSessionGatewayEvent : IDiscordGatewayPayload<Boolean>
{
	/// <summary>
	/// Indicates whether the session is resumable.
	/// </summary>
	[JsonPropertyName("d")]
	public Boolean Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public DiscordGatewayOpcode Opcode { get; init; }
}

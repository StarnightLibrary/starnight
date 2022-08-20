namespace Starnight.Internal.Gateway.Payloads.Clientbound;

using System;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

using Starnight.Internal.Gateway.Objects.Clientbound;

/// <summary>
/// READY event.
/// </summary>
[StructLayout(LayoutKind.Auto)]
public record struct DiscordReadyGatewayEvent : IDiscordGatewayDispatchPayload<DiscordConnectedEventObject>
{
	/// <inheritdoc/>
	[JsonPropertyName("s")]
	public Int32 Sequence { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("t")]
	public String EventName { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public DiscordConnectedEventObject Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public DiscordGatewayOpcode Opcode { get; init; }
}

namespace Starnight.Internal.Gateway.Events.Inbound;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Gateway.Payloads.Inbound;

/// <summary>
/// READY event.
/// </summary>
public sealed record DiscordConnectedEvent : IDiscordGatewayDispatchEvent<ConnectedPayload>
{
	/// <inheritdoc/>
	[JsonPropertyName("s")]
	public required Int32 Sequence { get; set; }

	/// <inheritdoc/>
	[JsonPropertyName("t")]
	public required String EventName { get; set; }

	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required ConnectedPayload Data { get; set; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public required DiscordGatewayOpcode Opcode { get; set; }
}

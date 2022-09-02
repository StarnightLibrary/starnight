namespace Starnight.Internal.Gateway.Events;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Gateway;
using Starnight.Internal.Gateway.Payloads.Clientbound;

/// <summary>
/// READY event.
/// </summary>
public sealed record DiscordReadyEvent : IDiscordGatewayDispatchPayload<ConnectedPayload>
{
	/// <inheritdoc/>
	[JsonPropertyName("s")]
	public required Int32 Sequence { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("t")]
	public required String EventName { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required ConnectedPayload Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public required DiscordGatewayOpcode Opcode { get; init; }
}
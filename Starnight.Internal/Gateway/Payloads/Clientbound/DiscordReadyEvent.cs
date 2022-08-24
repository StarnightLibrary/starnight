namespace Starnight.Internal.Gateway.Payloads.Clientbound;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Gateway.Objects.Clientbound;

/// <summary>
/// READY event.
/// </summary>
public sealed record DiscordReadyEvent : IDiscordGatewayDispatchPayload<DiscordConnectedEventObject>
{
	/// <inheritdoc/>
	[JsonPropertyName("s")]
	public required Int32 Sequence { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("t")]
	public required String EventName { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required DiscordConnectedEventObject Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public required DiscordGatewayOpcode Opcode { get; init; }
}

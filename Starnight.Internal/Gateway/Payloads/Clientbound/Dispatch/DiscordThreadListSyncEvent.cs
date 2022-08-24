namespace Starnight.Internal.Gateway.Payloads.Clientbound.Dispatch;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Gateway.Objects.Clientbound.Dispatch;

/// <summary>
/// Fired when the user gains access to a channel.
/// </summary>
public sealed record DiscordThreadListSyncEvent : IDiscordGatewayDispatchPayload<DiscordThreadListSyncEventObject>
{
	/// <inheritdoc/>
	[JsonPropertyName("s")]
	public required Int32 Sequence { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("t")]
	public required String EventName { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required DiscordThreadListSyncEventObject Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public required DiscordGatewayOpcode Opcode { get; init; }
}

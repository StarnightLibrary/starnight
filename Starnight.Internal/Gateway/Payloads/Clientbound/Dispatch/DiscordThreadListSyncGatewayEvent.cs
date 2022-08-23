namespace Starnight.Internal.Gateway.Payloads.Clientbound.Dispatch;

using System;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

using Starnight.Internal.Gateway.Objects.Clientbound.Dispatch;

/// <summary>
/// Fired when the user gains access to a channel.
/// </summary>
[StructLayout(LayoutKind.Auto)]
public record struct DiscordThreadListSyncGatewayEvent : IDiscordGatewayDispatchPayload<DiscordThreadListSyncEventObject>
{
	/// <inheritdoc/>
	[JsonPropertyName("s")]
	public Int32 Sequence { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("t")]
	public String EventName { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public DiscordThreadListSyncEventObject Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public DiscordGatewayOpcode Opcode { get; init; }
}

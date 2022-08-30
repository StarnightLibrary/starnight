namespace Starnight.Internal.Gateway.Events.Dispatch.Channels;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Gateway;
using Starnight.Internal.Gateway.Payloads.Clientbound.Dispatch;

/// <summary>
/// Fired when the user gains access to a channel.
/// </summary>
public sealed record DiscordThreadListSyncEvent : IDiscordGatewayDispatchPayload<ThreadListSyncPayload>
{
	/// <inheritdoc/>
	[JsonPropertyName("s")]
	public required Int32 Sequence { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("t")]
	public required String EventName { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required ThreadListSyncPayload Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public required DiscordGatewayOpcode Opcode { get; init; }
}

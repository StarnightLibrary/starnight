namespace Starnight.Internal.Gateway.Events.Dispatch;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Gateway.Payloads.Clientbound.Dispatch;

public sealed record DiscordMessagesBulkDeletedEvent : IDiscordGatewayDispatchPayload<MessagesBulkDeletedPayload>
{
	/// <inheritdoc/>
	[JsonPropertyName("s")]
	public required Int32 Sequence { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("t")]
	public required String EventName { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required MessagesBulkDeletedPayload Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public required DiscordGatewayOpcode Opcode { get; init; }
}

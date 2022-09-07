namespace Starnight.Internal.Gateway.Events.Inbound.Dispatch;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Gateway.Payloads.Inbound.Dispatch;

/// <summary>
/// Represents an EmojiMessageReactionsRemoved event.
/// </summary>
public sealed record DiscordEmojiMessageReactionsRemovedEvent : IDiscordGatewayDispatchPayload<EmojiMessageReactionsRemovedPayload>
{
	/// <inheritdoc/>
	[JsonPropertyName("s")]
	public required Int32 Sequence { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("t")]
	public required String EventName { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required EmojiMessageReactionsRemovedPayload Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public required DiscordGatewayOpcode Opcode { get; init; }
}

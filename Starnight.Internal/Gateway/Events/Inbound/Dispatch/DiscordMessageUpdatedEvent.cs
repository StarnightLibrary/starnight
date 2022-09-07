namespace Starnight.Internal.Gateway.Events.Dispatch;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Messages;

public sealed record DiscordMessageUpdatedEvent : IDiscordGatewayDispatchPayload<DiscordMessage>
{
	/// <inheritdoc/>
	[JsonPropertyName("s")]
	public required Int32 Sequence { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("t")]
	public required String EventName { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required DiscordMessage Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public required DiscordGatewayOpcode Opcode { get; init; }
}
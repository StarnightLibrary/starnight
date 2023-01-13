namespace Starnight.Internal.Gateway.Events.Inbound.Dispatch;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Messages;
using Starnight.Internal.Gateway.Events;

/// <summary>
/// Represents a MessageCreated event.
/// </summary>
public sealed record DiscordMessageCreatedEvent : IDiscordGatewayDispatchEvent<DiscordMessage>
{
	/// <inheritdoc/>
	[JsonPropertyName("s")]
	public required Int32 Sequence { get; set; }

	/// <inheritdoc/>
	[JsonPropertyName("t")]
	public required String EventName { get; set; }

	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required DiscordMessage Data { get; set; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public required DiscordGatewayOpcode Opcode { get; set; }
}

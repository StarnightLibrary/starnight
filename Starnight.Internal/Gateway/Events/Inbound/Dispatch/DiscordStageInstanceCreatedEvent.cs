namespace Starnight.Internal.Gateway.Events.Inbound.Dispatch;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Voice;
using Starnight.Internal.Gateway.Events;

/// <summary>
/// Represents a StageInstanceCreated event.
/// </summary>
public sealed record DiscordStageInstanceCreatedEvent : IDiscordGatewayDispatchEvent<DiscordStageInstance>
{
	/// <inheritdoc/>
	[JsonPropertyName("s")]
	public required Int32 Sequence { get; set; }

	/// <inheritdoc/>
	[JsonPropertyName("t")]
	public required String EventName { get; set; }

	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required DiscordStageInstance Data { get; set; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public required DiscordGatewayOpcode Opcode { get; set; }
}

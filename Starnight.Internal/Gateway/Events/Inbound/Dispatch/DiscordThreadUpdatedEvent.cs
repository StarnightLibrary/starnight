namespace Starnight.Internal.Gateway.Events.Inbound.Dispatch;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Gateway.Events;

/// <summary>
/// Fired when a thread is updated.
/// </summary>
public sealed record DiscordThreadUpdatedEvent : IDiscordGatewayDispatchEvent<DiscordChannel>
{
	/// <inheritdoc/>
	[JsonPropertyName("s")]
	public required Int32 Sequence { get; set; }

	/// <inheritdoc/>
	[JsonPropertyName("t")]
	public required String EventName { get; set; }

	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required DiscordChannel Data { get; set; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public required DiscordGatewayOpcode Opcode { get; set; }
}

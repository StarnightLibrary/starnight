namespace Starnight.Internal.Gateway.Events.Inbound.Dispatch;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Gateway.Events;

/// <summary>
/// Represents a GuildScheduledEventCreated event.
/// </summary>
public sealed record DiscordScheduledEventCreatedEvent : IDiscordGatewayDispatchEvent<DiscordScheduledEvent>
{
	/// <inheritdoc/>
	[JsonPropertyName("s")]
	public required Int32 Sequence { get; set; }

	/// <inheritdoc/>
	[JsonPropertyName("t")]
	public required String EventName { get; set; }

	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required DiscordScheduledEvent Data { get; set; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public required DiscordGatewayOpcode Opcode { get; set; }
}

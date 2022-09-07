namespace Starnight.Internal.Gateway.Events.Inbound.Dispatch;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guilds;

/// <summary>
/// Represents a GuildScheduledEventCreated event.
/// </summary>
public sealed record DiscordScheduledEventCreatedEvent : IDiscordGatewayDispatchEvent<DiscordScheduledEvent>
{
	/// <inheritdoc/>
	[JsonPropertyName("s")]
	public required Int32 Sequence { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("t")]
	public required String EventName { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required DiscordScheduledEvent Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public required DiscordGatewayOpcode Opcode { get; init; }
}

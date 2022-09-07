namespace Starnight.Internal.Gateway.Events.Inbound.Dispatch;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Channels.Threads;

/// <summary>
/// ThreadMemberUpdated event.
/// </summary>
public sealed record DiscordThreadMemberUpdatedEvent : IDiscordGatewayDispatchEvent<DiscordThreadMember>
{
	/// <inheritdoc/>
	[JsonPropertyName("s")]
	public required Int32 Sequence { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("t")]
	public required String EventName { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required DiscordThreadMember Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public required DiscordGatewayOpcode Opcode { get; init; }
}

namespace Starnight.Internal.Gateway.Events.Dispatch;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Channels.Threads;
using Starnight.Internal.Gateway;

/// <summary>
/// ThreadMemberUpdated event.
/// </summary>
public sealed record DiscordThreadMemberUpdatedEvent : IDiscordGatewayDispatchPayload<DiscordThreadMember>
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

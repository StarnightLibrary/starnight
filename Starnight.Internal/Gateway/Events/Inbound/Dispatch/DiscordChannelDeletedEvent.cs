namespace Starnight.Internal.Gateway.Events.Dispatch;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Channels;

/// <summary>
/// Fired when a channel is deleted.
/// </summary>
public sealed record DiscordChannelDeletedEvent : IDiscordGatewayDispatchPayload<DiscordChannel>
{
	/// <inheritdoc/>
	[JsonPropertyName("s")]
	public required Int32 Sequence { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("t")]
	public required String EventName { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required DiscordChannel Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public required DiscordGatewayOpcode Opcode { get; init; }
}

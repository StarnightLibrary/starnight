namespace Starnight.Internal.Gateway.Payloads.Clientbound.Dispatch;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Gateway.Objects.Clientbound.Dispatch;

/// <summary>
/// Fired when a new thread is created or if the current user is added to an existing thread.
/// </summary>
public sealed record DiscordThreadCreatedEvent : IDiscordGatewayDispatchPayload<DiscordThreadCreatedEventObject>
{
	/// <inheritdoc/>
	[JsonPropertyName("s")]
	public required Int32 Sequence { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("t")]
	public required String EventName { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required DiscordThreadCreatedEventObject Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public required DiscordGatewayOpcode Opcode { get; init; }
}

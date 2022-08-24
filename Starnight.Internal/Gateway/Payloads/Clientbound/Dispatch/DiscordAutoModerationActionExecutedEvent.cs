namespace Starnight.Internal.Gateway.Payloads.Clientbound.Dispatch;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Gateway.Objects.Clientbound.Dispatch;

/// <summary>
/// Fired when an auto-moderation rule is triggered and an action is executed.
/// </summary>
public sealed record DiscordAutoModerationActionExecutedEvent
	: IDiscordGatewayDispatchPayload<DiscordAutoModerationActionExecutedEventObject>
{
	/// <inheritdoc/>
	[JsonPropertyName("s")]
	public required Int32 Sequence { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("t")]
	public required String EventName { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required DiscordAutoModerationActionExecutedEventObject Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public required DiscordGatewayOpcode Opcode { get; init; }
}

namespace Starnight.Internal.Gateway.Payloads.Clientbound.Dispatch;

using System;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

using Starnight.Internal.Gateway.Objects.Clientbound.Dispatch;

/// <summary>
/// Fired when an auto-moderation rule is triggered and an action is executed.
/// </summary>
[StructLayout(LayoutKind.Auto)]
public record struct DiscordAutoModerationActionExecutedGatewayEvent
	: IDiscordGatewayDispatchPayload<DiscordAutoModerationActionExecutedEventObject>
{
	/// <inheritdoc/>
	[JsonPropertyName("s")]
	public Int32 Sequence { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("t")]
	public String EventName { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public DiscordAutoModerationActionExecutedEventObject Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public DiscordGatewayOpcode Opcode { get; init; }
}

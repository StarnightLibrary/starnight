namespace Starnight.Internal.Gateway.Payloads.Clientbound.Dispatch;

using System;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guilds.AutoModeration;

/// <summary>
/// Fired when an auto moderation rule is deleted.
/// </summary>
[StructLayout(LayoutKind.Auto)]
public record struct DiscordAutoModerationRuleDeletedGatewayEvent : IDiscordGatewayDispatchPayload<DiscordAutoModerationRule>
{
	/// <inheritdoc/>
	[JsonPropertyName("s")]
	public Int32 Sequence { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("t")]
	public String EventName { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public DiscordAutoModerationRule Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public DiscordGatewayOpcode Opcode { get; init; }
}

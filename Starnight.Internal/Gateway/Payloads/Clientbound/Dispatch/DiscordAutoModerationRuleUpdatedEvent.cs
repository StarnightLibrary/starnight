namespace Starnight.Internal.Gateway.Payloads.Clientbound.Dispatch;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guilds.AutoModeration;

/// <summary>
/// Sent when an auto moderation rule is updated.
/// </summary>
public sealed record DiscordAutoModerationRuleUpdatedEvent : IDiscordGatewayDispatchPayload<DiscordAutoModerationRule>
{
	/// <inheritdoc/>
	[JsonPropertyName("s")]
	public required Int32 Sequence { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("t")]
	public required String EventName { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required DiscordAutoModerationRule Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public required DiscordGatewayOpcode Opcode { get; init; }
}

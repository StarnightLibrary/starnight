namespace Starnight.Internal.Gateway.Events.Dispatch.Guilds;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Gateway.Payloads.Clientbound.Dispatch;

/// <summary>
/// Represents a GuildRoleDeleted event.
/// </summary>
public sealed record DiscordGuildRoleDeletedEvent : IDiscordGatewayDispatchPayload<GuildRoleDeletedPayload>
{
	/// <inheritdoc/>
	[JsonPropertyName("s")]
	public required Int32 Sequence { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("t")]
	public required String EventName { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required GuildRoleDeletedPayload Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public required DiscordGatewayOpcode Opcode { get; init; }
}

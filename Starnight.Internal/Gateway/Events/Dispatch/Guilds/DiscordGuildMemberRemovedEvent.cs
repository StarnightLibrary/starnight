namespace Starnight.Internal.Gateway.Events.Dispatch.Guilds;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Gateway.Payloads.Clientbound.Dispatch;

/// <summary>
/// Represents a GuildMemberRemoved event.
/// </summary>
public sealed record DiscordGuildMemberRemovedEvent : IDiscordGatewayDispatchPayload<GuildMemberRemovedPayload>
{
	/// <inheritdoc/>
	[JsonPropertyName("s")]
	public required Int32 Sequence { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("t")]
	public required String EventName { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required GuildMemberRemovedPayload Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public required DiscordGatewayOpcode Opcode { get; init; }
}

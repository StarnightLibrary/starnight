namespace Starnight.Internal.Gateway.Events.Dispatch.Guilds;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guilds;

/// <summary>
/// Represents a GuildMemberUpdated event.
/// </summary>
public sealed record DiscordGuildMemberUpdatedEvent : IDiscordGatewayDispatchPayload<DiscordGuildMember>
{
	/// <inheritdoc/>
	[JsonPropertyName("s")]
	public required Int32 Sequence { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("t")]
	public required String EventName { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required DiscordGuildMember Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public required DiscordGatewayOpcode Opcode { get; init; }
}

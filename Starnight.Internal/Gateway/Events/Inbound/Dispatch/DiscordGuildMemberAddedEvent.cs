namespace Starnight.Internal.Gateway.Events.Inbound.Dispatch;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Gateway.Events;

/// <summary>
/// Represents a GuildMemberAdded event.
/// </summary>
public sealed record DiscordGuildMemberAddedEvent : IDiscordGatewayDispatchEvent<DiscordGuildMember>
{
	/// <inheritdoc/>
	[JsonPropertyName("s")]
	public required Int32 Sequence { get; set; }

	/// <inheritdoc/>
	[JsonPropertyName("t")]
	public required String EventName { get; set; }

	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required DiscordGuildMember Data { get; set; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public required DiscordGatewayOpcode Opcode { get; set; }
}

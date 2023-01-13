namespace Starnight.Internal.Gateway.Events.Outbound;

using System.Text.Json.Serialization;

using Starnight.Internal.Gateway.Events;
using Starnight.Internal.Gateway.Payloads.Outbound;

/// <summary>
/// Gateway command to request guild members through the gateway.
/// </summary>
public sealed record DiscordRequestGuildMembersEvent : IDiscordGatewayEvent<RequestGuildMembersPayload>
{
	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required RequestGuildMembersPayload Data { get; set; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public DiscordGatewayOpcode Opcode { get; set; } = DiscordGatewayOpcode.RequestGuildMembers;
}

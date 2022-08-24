namespace Starnight.Internal.Gateway.Payloads.Serverbound;

using System.Text.Json.Serialization;

using Starnight.Internal.Gateway.Objects.Serverbound;

/// <summary>
/// Gateway command to request guild members through the gateway.
/// </summary>
public sealed record DiscordRequestGuildMembersCommand : IDiscordGatewayPayload<DiscordRequestGuildMembersCommandObject>
{
	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required DiscordRequestGuildMembersCommandObject Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public DiscordGatewayOpcode Opcode { get; init; } = DiscordGatewayOpcode.RequestGuildMembers;
}

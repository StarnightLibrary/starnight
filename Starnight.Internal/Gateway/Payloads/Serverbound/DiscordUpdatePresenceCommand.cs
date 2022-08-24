namespace Starnight.Internal.Gateway.Payloads.Serverbound;

using System.Text.Json.Serialization;

using Starnight.Internal.Gateway.Objects.Serverbound;

/// <summary>
/// Represents an Update Presence gateway command
/// </summary>
public sealed record DiscordUpdatePresenceCommand : IDiscordGatewayPayload<DiscordPresenceUpdateCommandObject>
{
	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required DiscordPresenceUpdateCommandObject Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public DiscordGatewayOpcode Opcode { get; init; } = DiscordGatewayOpcode.PresenceUpdate;
}

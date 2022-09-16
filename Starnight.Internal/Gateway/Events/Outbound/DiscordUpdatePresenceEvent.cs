namespace Starnight.Internal.Gateway.Events.Outbound;

using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Users;

/// <summary>
/// Represents an Update Presence gateway command
/// </summary>
public sealed record DiscordUpdatePresenceEvent : IDiscordGatewayEvent<DiscordPresence>
{
	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required DiscordPresence Data { get; set; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public DiscordGatewayOpcode Opcode { get; set; } = DiscordGatewayOpcode.PresenceUpdate;
}

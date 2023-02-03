namespace Starnight.Internal.Gateway.Events.Outbound;

using System.Text.Json.Serialization;

using Starnight.Internal.Gateway.Events;
using Starnight.Internal.Gateway.Payloads.Outbound;

/// <summary>
/// Represents an Update Voice State gateway command.
/// </summary>
public sealed record DiscordUpdateVoiceStateEvent : IDiscordGatewayEvent<UpdateVoiceStatePayload>
{
	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required UpdateVoiceStatePayload Data { get; set; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public DiscordGatewayOpcode Opcode { get; set; } = DiscordGatewayOpcode.VoiceStateUpdate;
}

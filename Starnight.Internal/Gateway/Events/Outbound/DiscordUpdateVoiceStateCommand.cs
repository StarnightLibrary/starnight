namespace Starnight.Internal.Gateway.Events.Outbound;

using System.Text.Json.Serialization;

using Starnight.Internal.Gateway.Payloads.Outbound;

/// <summary>
/// Represents an Update Voice State gateway command.
/// </summary>
public sealed record DiscordUpdateVoiceStateCommand : IDiscordGatewayPayload<UpdateVoiceStatePayload>
{
	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required UpdateVoiceStatePayload Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public DiscordGatewayOpcode Opcode { get; init; } = DiscordGatewayOpcode.VoiceStateUpdate;
}

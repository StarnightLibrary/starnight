namespace Starnight.Internal.Gateway.Payloads.Serverbound;

using System.Text.Json.Serialization;

using Starnight.Internal.Gateway.Objects.Serverbound;

/// <summary>
/// Represents an Update Voice State gateway command.
/// </summary>
public sealed record DiscordUpdateVoiceStateCommand : IDiscordGatewayPayload<DiscordUpdateVoiceStateCommandObject>
{
	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required DiscordUpdateVoiceStateCommandObject Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public DiscordGatewayOpcode Opcode { get; init; } = DiscordGatewayOpcode.VoiceStateUpdate;
}

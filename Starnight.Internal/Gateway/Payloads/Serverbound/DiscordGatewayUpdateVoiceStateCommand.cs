namespace Starnight.Internal.Gateway.Payloads.Serverbound;

using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

using Starnight.Internal.Gateway.Objects.Serverbound;

/// <summary>
/// Represents an Update Voice State gateway command.
/// </summary>
[StructLayout(LayoutKind.Auto)]
public record struct DiscordGatewayUpdateVoiceStateCommand : IDiscordGatewayPayload<DiscordUpdateVoiceStateCommandObject>
{
	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required DiscordUpdateVoiceStateCommandObject Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public DiscordGatewayOpcode Opcode { get; init; } = DiscordGatewayOpcode.VoiceStateUpdate;

	[EditorBrowsable(EditorBrowsableState.Never)]
	public DiscordGatewayUpdateVoiceStateCommand() { }
}

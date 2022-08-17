namespace Starnight.Internal.Gateway.Payloads.Serverbound;

using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

using Starnight.Internal.Gateway.Objects.Serverbound;

/// <summary>
/// Represents an Update Presence gateway command
/// </summary>
[StructLayout(LayoutKind.Auto)]
public record struct DiscordGatewayUpdatePresenceCommand : IDiscordGatewayPayload<DiscordPresenceUpdateCommandObject>
{
	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required DiscordPresenceUpdateCommandObject Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public DiscordGatewayOpcode Opcode { get; init; } = DiscordGatewayOpcode.PresenceUpdate;

	[EditorBrowsable(EditorBrowsableState.Never)]
	public DiscordGatewayUpdatePresenceCommand() { }
}

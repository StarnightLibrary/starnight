namespace Starnight.Internal.Gateway.Payloads.Serverbound;

using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

using Starnight.Internal.Gateway.Objects.Serverbound;

/// <summary>
/// Gateway command to request guild members through the gateway.
/// </summary>
[StructLayout(LayoutKind.Auto)]
public record struct DiscordRequestGuildMembersGatewayCommand : IDiscordGatewayPayload<DiscordRequestGuildMembersCommandObject>
{
	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required DiscordRequestGuildMembersCommandObject Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public DiscordGatewayOpcode Opcode { get; init; } = DiscordGatewayOpcode.RequestGuildMembers;

	[EditorBrowsable(EditorBrowsableState.Never)]
	public DiscordRequestGuildMembersGatewayCommand() { }
}

namespace Starnight.Internal.Gateway.Payloads.Serverbound;

using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

using Starnight.Internal.Gateway.Objects.Serverbound;

/// <summary>
/// Serverbound, <see cref="DiscordGatewayOpcode.Identify"/> command.
/// </summary>
[StructLayout(LayoutKind.Auto)]
public record struct DiscordIdentifyGatewayCommand : IDiscordGatewayPayload<DiscordIdentifyCommandObject>
{
	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required DiscordIdentifyCommandObject Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public DiscordGatewayOpcode Opcode { get; init; } = DiscordGatewayOpcode.Identify;

	[EditorBrowsable(EditorBrowsableState.Never)]
	public DiscordIdentifyGatewayCommand() { }
}

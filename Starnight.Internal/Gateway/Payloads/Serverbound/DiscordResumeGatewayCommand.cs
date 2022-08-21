namespace Starnight.Internal.Gateway.Payloads.Serverbound;

using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

using Starnight.Internal.Gateway.Objects.Serverbound;

/// <summary>
/// Serverbound, <see cref="DiscordGatewayOpcode.Resume"/> command.
/// </summary>
[StructLayout(LayoutKind.Auto)]
public record struct DiscordResumeGatewayCommand : IDiscordGatewayPayload<DiscordResumeCommandObject>
{
	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required DiscordResumeCommandObject Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public DiscordGatewayOpcode Opcode { get; init; } = DiscordGatewayOpcode.Resume;

	[EditorBrowsable(EditorBrowsableState.Never)]
	public DiscordResumeGatewayCommand() { }
}

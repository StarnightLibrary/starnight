namespace Starnight.Internal.Gateway.Payloads.Serverbound;

using System.ComponentModel;
using System.Text.Json.Serialization;

using Starnight.Internal.Gateway.Objects.Serverbound;

/// <summary>
/// Serverbound, <see cref="DiscordGatewayOpcode.Resume"/> command.
/// </summary>
public record struct DiscordGatewayResumeCommand : IDiscordGatewayPayload<DiscordResumeCommandObject>
{
	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required DiscordResumeCommandObject Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public DiscordGatewayOpcode Opcode { get; init; } = DiscordGatewayOpcode.Resume;

	[EditorBrowsable(EditorBrowsableState.Never)]
	public DiscordGatewayResumeCommand() { }
}

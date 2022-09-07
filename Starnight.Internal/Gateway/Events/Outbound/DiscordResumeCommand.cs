namespace Starnight.Internal.Gateway.Events.Outbound;

using System.Text.Json.Serialization;

using Starnight.Internal.Gateway;
using Starnight.Internal.Gateway.Payloads.Outbound;

/// <summary>
/// Serverbound, <see cref="DiscordGatewayOpcode.Resume"/> command.
/// </summary>
public sealed record DiscordResumeCommand : IDiscordGatewayPayload<ResumePayload>
{
	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required ResumePayload Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public DiscordGatewayOpcode Opcode { get; init; } = DiscordGatewayOpcode.Resume;
}

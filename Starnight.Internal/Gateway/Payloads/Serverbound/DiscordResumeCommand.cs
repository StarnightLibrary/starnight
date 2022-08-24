namespace Starnight.Internal.Gateway.Payloads.Serverbound;

using System.Text.Json.Serialization;

using Starnight.Internal.Gateway.Objects.Serverbound;

/// <summary>
/// Serverbound, <see cref="DiscordGatewayOpcode.Resume"/> command.
/// </summary>
public sealed record DiscordResumeCommand : IDiscordGatewayPayload<DiscordResumeCommandObject>
{
	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required DiscordResumeCommandObject Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public DiscordGatewayOpcode Opcode { get; init; } = DiscordGatewayOpcode.Resume;
}

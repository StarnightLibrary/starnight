namespace Starnight.Internal.Gateway.Payloads.Serverbound;

using System.Text.Json.Serialization;

using Starnight.Internal.Gateway.Objects.Serverbound;

/// <summary>
/// Serverbound, <see cref="DiscordGatewayOpcode.Identify"/> command.
/// </summary>
public sealed record DiscordGatewayIdentifyCommand : IDiscordGatewayPayload<DiscordIdentifyCommandObject>
{
	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required DiscordIdentifyCommandObject Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public required DiscordGatewayOpcode Opcode { get; init; } = DiscordGatewayOpcode.Identify;
}

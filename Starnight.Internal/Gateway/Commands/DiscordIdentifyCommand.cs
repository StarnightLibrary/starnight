namespace Starnight.Internal.Gateway.Commands;

using System.Text.Json.Serialization;

using Starnight.Internal.Gateway;
using Starnight.Internal.Gateway.Payloads.Serverbound;

/// <summary>
/// Serverbound, <see cref="DiscordGatewayOpcode.Identify"/> command.
/// </summary>
public sealed record DiscordIdentifyCommand : IDiscordGatewayPayload<IdentifyPayload>
{
	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required IdentifyPayload Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public DiscordGatewayOpcode Opcode { get; init; } = DiscordGatewayOpcode.Identify;
}

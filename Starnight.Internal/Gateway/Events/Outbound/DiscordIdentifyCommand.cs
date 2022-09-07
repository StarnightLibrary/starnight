namespace Starnight.Internal.Gateway.Events.Outbound;

using System.Text.Json.Serialization;

using Starnight.Internal.Gateway.Payloads.Outbound;

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
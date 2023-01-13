namespace Starnight.Internal.Gateway.Events.Outbound;

using System.Text.Json.Serialization;

using Starnight.Internal.Gateway.Events;
using Starnight.Internal.Gateway.Payloads.Outbound;

/// <summary>
/// Serverbound, <see cref="DiscordGatewayOpcode.Identify"/> command.
/// </summary>
public sealed record DiscordIdentifyEvent : IDiscordGatewayEvent<IdentifyPayload>
{
	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required IdentifyPayload Data { get; set; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public DiscordGatewayOpcode Opcode { get; set; } = DiscordGatewayOpcode.Identify;
}

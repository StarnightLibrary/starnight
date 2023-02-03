namespace Starnight.Internal.Gateway.Events.Inbound;

using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// Represents an inbound event with an unknown/unexpected opcode.
/// </summary>
public sealed record DiscordUnknownInboundEvent : IDiscordGatewayEvent
{
	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public DiscordGatewayOpcode Opcode { get; set; }

	/// <summary>
	/// The full payload of this event.
	/// </summary>
	[JsonPropertyName("event")]
	public JsonElement Event { get; set; }
}

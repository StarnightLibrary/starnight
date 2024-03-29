namespace Starnight.Internal.Gateway.Events.Inbound;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Gateway.Events;

/// <summary>
/// Indicates to the client that the session is invalid, and that it may need to reconnect.
/// </summary>
public sealed record DiscordInvalidSessionEvent : IDiscordGatewayEvent<Boolean>
{
	/// <summary>
	/// Indicates whether the session is resumable.
	/// </summary>
	[JsonPropertyName("d")]
	public required Boolean Data { get; set; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public required DiscordGatewayOpcode Opcode { get; set; }
}

namespace Starnight.Internal.Gateway.Events.Inbound;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Indicates to the client that the session is invalid, and that it may need to reconnect.
/// </summary>
public sealed record DiscordInvalidSessionEvent : IDiscordGatewayPayload<Boolean>
{
	/// <summary>
	/// Indicates whether the session is resumable.
	/// </summary>
	[JsonPropertyName("d")]
	public required Boolean Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public required DiscordGatewayOpcode Opcode { get; init; }
}

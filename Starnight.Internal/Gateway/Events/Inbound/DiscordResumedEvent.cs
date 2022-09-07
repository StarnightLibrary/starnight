namespace Starnight.Internal.Gateway.Events.Inbound;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Dispatched if resumption was successful.
/// </summary>
public sealed record DiscordResumedEvent : IDiscordGatewayEvent
{
	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public required DiscordGatewayOpcode Opcode { get; set; }

	/// <inheritdoc/>
	[JsonPropertyName("s")]
	public required Int32 Sequence { get; set; }

	/// <inheritdoc/>
	[JsonPropertyName("t")]
	public required String EventName { get; set; }
}

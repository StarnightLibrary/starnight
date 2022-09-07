namespace Starnight.Internal.Gateway.Events.Inbound;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Gateway;

/// <summary>
/// Dispatched if resumption was successful.
/// </summary>
public sealed record DiscordResumedEvent : IDiscordGatewayPayload
{
	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public required DiscordGatewayOpcode Opcode { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("s")]
	public required Int32 Sequence { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("t")]
	public required String EventName { get; init; }
}

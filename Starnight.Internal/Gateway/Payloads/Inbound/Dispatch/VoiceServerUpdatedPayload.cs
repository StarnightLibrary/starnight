namespace Starnight.Internal.Gateway.Payloads.Inbound.Dispatch;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the payload for a VoiceServerUpdated event.
/// </summary>
public sealed record VoiceServerUpdatedPayload
{
	/// <summary>
	/// Voice connection token.
	/// </summary>
	[JsonPropertyName("token")]
	public required String Token { get; init; }

	/// <summary>
	/// The guild this voice server updated event is for.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public required Int64 GuildId { get; init; }

	/// <summary>
	/// The voice server host.
	/// </summary>
	[JsonPropertyName("endpoint")]
	public String? Endpoint { get; init; }
}

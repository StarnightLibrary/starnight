namespace Starnight.Internal.Gateway.Payloads;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the response payload to GET /gateway/bot.
/// </summary>
public sealed record GetGatewayBotResponsePayload
{
	/// <summary>
	/// The WSS URL to be used for connecting to the gateway.
	/// </summary>
	[JsonPropertyName("url")]
	public required String Url { get; init; }

	/// <summary>
	/// The recommended number of shards to use when connecting.
	/// </summary>
	[JsonPropertyName("shards")]
	public required Int32 Shards { get; init; }

	/// <summary>
	/// Indicates session start limits for this bot.
	/// </summary>
	[JsonPropertyName("session_start_limit")]
	public required DiscordSessionStartLimit SessionStartLimit { get; init; }
}

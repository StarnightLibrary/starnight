namespace Starnight.Internal.Gateway.Payloads.Inbound.Dispatch;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a payload to a ChannelPinsUpdated event.
/// </summary>
public sealed record ChannelPinsUpdatedPayload
{
	/// <summary>
	/// The ID of the guild.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public Optional<Int64> GuildId { get; init; }

	/// <summary>
	/// The ID of the channel.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public required Int64 ChannelId { get; init; }

	/// <summary>
	/// The time at which the most recent pin was pinned.
	/// </summary>
	[JsonPropertyName("last_pin_timestamp")]
	public Optional<DateTimeOffset?> LastPinTimestamp { get; init; }
}

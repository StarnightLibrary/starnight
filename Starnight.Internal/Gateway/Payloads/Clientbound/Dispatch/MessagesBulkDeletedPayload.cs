namespace Starnight.Internal.Gateway.Payloads.Clientbound.Dispatch;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the payload for a MessagesBulkDeleted event.
/// </summary>
public sealed record MessagesBulkDeletedPayload
{
	/// <summary>
	/// The IDs of the deleted messages.
	/// </summary>
	[JsonPropertyName("ids")]
	public required IEnumerable<Int64> MessageIds { get; init; }

	/// <summary>
	/// The ID of the channel.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public required Int64 ChannelId { get; init; }

	/// <summary>
	/// The ID of the guild.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public Optional<Int64> GuildId { get; init; }
}

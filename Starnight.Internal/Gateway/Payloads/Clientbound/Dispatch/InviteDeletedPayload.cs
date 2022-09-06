namespace Starnight.Internal.Gateway.Payloads.Clientbound.Dispatch;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a payload for an InviteDeleted event.
/// </summary>
public sealed record InviteDeletedPayload
{
	/// <summary>
	/// The channel this invite pointed to.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public required Int64 ChannelId { get; init; }

	/// <summary>
	/// The guild this invite pointed to.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public Optional<Int64> GuildId { get; init; }

	/// <summary>
	/// The invite code.
	/// </summary>
	[JsonPropertyName("code")]
	public required String Code { get; init; }
}

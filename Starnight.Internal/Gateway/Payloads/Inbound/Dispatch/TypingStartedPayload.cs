namespace Starnight.Internal.Gateway.Payloads.Inbound.Dispatch;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guilds;

/// <summary>
/// Represents the payload for a TypingStarted event.
/// </summary>
public sealed record TypingStartedPayload
{
	/// <summary>
	/// The ID of the channel this occurred in.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public required Int64 ChannelId { get; init; }

	/// <summary>
	/// The ID of the guild this occurred in, if applicable.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public Optional<Int64> GuildId { get; init; }

	/// <summary>
	/// The ID of the user who started typing.
	/// </summary>
	[JsonPropertyName("user_id")]
	public required Int64 UserId { get; init; }

	/// <summary>
	/// Unix time in seconds of when the user started typing.
	/// </summary>
	[JsonPropertyName("timestamp")]
	public required Int32 Timestamp { get; init; }

	/// <summary>
	/// The member who started typing, if applicable.
	/// </summary>
	[JsonPropertyName("member")]
	public Optional<DiscordGuildMember> Member { get; init; }
}

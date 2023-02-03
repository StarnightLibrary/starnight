namespace Starnight.Internal.Entities.Channels.Threads;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Entities.Users;

/// <summary>
/// Additional data for a guild member that joined a thread.
/// </summary>
public sealed record DiscordThreadMember
{
	/// <summary>
	/// Snowflake identifier of the thread.
	/// </summary>
	[JsonPropertyName("id")]
	public Optional<Int64> ThreadId { get; init; }

	/// <summary>
	/// Snowflake identifier of the user.
	/// </summary>
	[JsonPropertyName("user_id")]
	public Optional<Int64> UserId { get; init; }

	/// <summary>
	/// Join time of this user.
	/// </summary>
	[JsonPropertyName("join_timestamp")]
	public required DateTimeOffset JoinTimestamp { get; init; }

	/// <summary>
	/// User-thread settings.
	/// </summary>
	[JsonPropertyName("flags")]
	public required Int32 Flags { get; init; }


	/// <summary>
	/// The ID of the guild this thread member belongs to.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public Optional<Int64> GuildId { get; init; }

	/// <summary>
	/// The associated guild member object.
	/// </summary>
	[JsonPropertyName("member")]
	public Optional<DiscordGuildMember> Member { get; init; }

	/// <summary>
	/// The current presence of this guild member.
	/// </summary>
	[JsonPropertyName("presence")]
	public Optional<DiscordPresence> Presence { get; init; }
}

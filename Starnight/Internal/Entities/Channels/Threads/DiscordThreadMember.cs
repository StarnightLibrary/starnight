namespace Starnight.Internal.Entities.Channels.Threads;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Additional data for a guild member that joined a thread.
/// </summary>
public record DiscordThreadMember
{
	/// <summary>
	/// Snowflake identifier of the thread.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("id")]
	public Int64? ThreadId { get; init; }

	/// <summary>
	/// Snowflake identifier of the user.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("user_id")]
	public Int64? UserId { get; init; }

	/// <summary>
	/// Join time of this user.
	/// </summary>
	[JsonPropertyName("join_timestamp")]
	public DateTime JoinTimestamp { get; init; }

	/// <summary>
	/// User-thread settings.
	/// </summary>
	[JsonPropertyName("flags")]
	public Int32 Flags { get; init; }
}

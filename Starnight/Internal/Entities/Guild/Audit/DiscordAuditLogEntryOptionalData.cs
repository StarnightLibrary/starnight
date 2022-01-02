namespace Starnight.Internal.Entities.Guild.Audit;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Holds additional data for <see cref="DiscordAuditLogEntry"/>.
/// </summary>
public class DiscordAuditLogEntryOptionalData : DiscordSnowflakeObject
{
	/// <summary>
	/// The channel in which entities were targeted by this audit log action.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("channel_id")]
	public Int64? ChannelId { get; init; }

	/// <summary>
	/// The number of entities that were targeted.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("count")]
	public Int32? Count { get; init; }

	/// <summary>
	/// The number of days after which inactive members were kicked.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("delete_member_days")]
	public Int32? MemberPruneDays { get; init; }

	/// <summary>
	/// The number of members that were pruned.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("members_removed")]
	public Int32? MembersPruned { get; init; }

	/// <summary>
	/// The ID of the message that was pinned/unpinned.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("message_id")]
	public Int64? MessageId { get; init; }

	/// <summary>
	/// The name of the role.
	/// </summary>
	[JsonPropertyName("role_name")]
	public String? RoleName { get; init; }

	/// <summary>
	/// The type of this object.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("type")]
	public DiscordAuditLogEntryOptionalDataType? Type { get; init; }
}

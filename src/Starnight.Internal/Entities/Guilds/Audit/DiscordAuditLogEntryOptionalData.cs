namespace Starnight.Internal.Entities.Guilds.Audit;

using System;
using System.Text.Json.Serialization;

using Starnight.Entities;

/// <summary>
/// Holds additional data for <see cref="DiscordAuditLogEntry"/>.
/// </summary>
public sealed record DiscordAuditLogEntryOptionalData : DiscordSnowflakeObject
{
	/// <summary>
	/// The application whose permissions were updated.
	/// </summary>
	[JsonPropertyName("application_id")]
	public Optional<Int64> ApplicationId { get; init; }

	/// <summary>
	/// The channel in which entities were targeted by this audit log action.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public Optional<Int64> ChannelId { get; init; }

	/// <summary>
	/// The number of entities that were targeted.
	/// </summary>
	[JsonPropertyName("count")]
	public Optional<Int32> Count { get; init; }

	/// <summary>
	/// The number of days after which inactive members were kicked.
	/// </summary>
	[JsonPropertyName("delete_member_days")]
	public Optional<Int32> MemberPruneDays { get; init; }

	/// <summary>
	/// The number of members that were pruned.
	/// </summary>
	[JsonPropertyName("members_removed")]
	public Optional<Int32> MembersPruned { get; init; }

	/// <summary>
	/// The ID of the message that was pinned/unpinned.
	/// </summary>
	[JsonPropertyName("message_id")]
	public Optional<Int64> MessageId { get; init; }

	/// <summary>
	/// The name of the role.
	/// </summary>
	[JsonPropertyName("role_name")]
	public Optional<String> RoleName { get; init; }

	/// <summary>
	/// The type of this object.
	/// </summary>
	[JsonPropertyName("type")]
	public Optional<DiscordAuditLogEntryOptionalDataType> Type { get; init; }
}

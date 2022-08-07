namespace Starnight.Internal.Entities.Guilds.Audit;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents an audit log entry.
/// </summary>
public sealed record DiscordAuditLogEntry : DiscordSnowflakeObject
{
	/// <summary>
	/// The snowflake identifier of the target.
	/// </summary>
	[JsonPropertyName("target_id")]
	public Int64? TargetId { get; init; }

	/// <summary>
	/// Stores a list of all changes to the object in question.
	/// </summary>
	[JsonPropertyName("changes")]
	public Optional<IEnumerable<DiscordAuditLogChange>> Changes { get; init; }

	/// <summary>
	/// The snowflake identifier of the user who made these changes.
	/// </summary>
	[JsonPropertyName("user_id")]
	public Int64? UserId { get; init; }

	/// <summary>
	/// The type of this action.
	/// </summary>
	[JsonPropertyName("action_type")]
	public required DiscordAuditLogEvent Event { get; init; }

	/// <summary>
	/// Optional data for this audit log entry.
	/// </summary>
	[JsonPropertyName("options")]
	public Optional<DiscordAuditLogEntryOptionalData> OptionalData { get; init; }

	/// <summary>
	/// The reason for these changes. Specified via the X-Audit-Log-Reason header.
	/// </summary>
	[JsonPropertyName("reason")]
	public Optional<String> Reason { get; init; }
}

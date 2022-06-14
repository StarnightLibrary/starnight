namespace Starnight.Internal.Entities.Guilds.Audit;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Guilds;

/// <summary>
/// Represents an audit log reflected change.
/// </summary>
public record DiscordAuditLogChange
{
	/// <summary>
	/// The new value of the object in question. The type must be inferred from the action key.
	/// </summary>
	[JsonPropertyName("new_value")]
	public Object NewValue { get; init; } = default!;

	/// <summary>
	/// The old value of the object in question. The type must be inferred from the action key.
	/// </summary>
	[JsonPropertyName("old_value")]
	public Object OldValue { get; init; } = default!;

	/// <summary>
	/// The action key for this change.
	/// </summary>
	[JsonPropertyName("key")]
	public Object ActionKey { get; init; } = default!;

	/// <summary>
	/// Additional key passed to invite and invite metadata changes.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public Object? ChannelId { get; init; }

	/// <summary>
	/// Additional key passed to role changes.
	/// </summary>
	[JsonPropertyName("$add")]
	public Object? Add { get; init; }

	/// <summary>
	/// Additional key passed to role changes
	/// </summary>
	[JsonPropertyName("$remove")]
	public Object? Remove { get; init; }
}

namespace Starnight.Internal.Entities.Guilds.Audit;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents an audit log reflected change.
/// </summary>
public sealed record DiscordAuditLogChange
{
	/// <summary>
	/// The new value of the object in question. The type must be inferred from the action key.
	/// </summary>
	[JsonPropertyName("new_value")]
	public Optional<Object> NewValue { get; init; }

	/// <summary>
	/// The old value of the object in question. The type must be inferred from the action key.
	/// </summary>
	[JsonPropertyName("old_value")]
	public Optional<Object> OldValue { get; init; }

	/// <summary>
	/// The action key for this change.
	/// </summary>
	[JsonPropertyName("key")]
	public required String ActionKey { get; init; }

	/// <summary>
	/// Additional key passed to invite and invite metadata changes.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public Optional<Int64> ChannelId { get; init; }

	/// <summary>
	/// Additional key passed to role changes.
	/// </summary>
	[JsonPropertyName("$add")]
	public Optional<Object> Add { get; init; }

	/// <summary>
	/// Additional key passed to role changes
	/// </summary>
	[JsonPropertyName("$remove")]
	public Optional<Object> Remove { get; init; }

	/// <summary>
	/// Additional key passed to webhook changes.
	/// </summary>
	[JsonPropertyName("avatar_hash")]
	public Optional<String> AvatarHash { get; init; }
}

namespace Starnight.Internal.Rest.Payloads.Channels;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Channels;

/// <summary>
/// Represents a payload to PATCH /channels/:channel_id, where the channel ID points to a thread channel.
/// </summary>
public sealed record ModifyThreadChannelRequestPayload
{
	/// <summary>
	/// New name for this thread channel.
	/// </summary>
	[JsonPropertyName("name")]
	public Optional<String> Name { get; init; }

	/// <summary>
	/// The new archive status for this thread.
	/// </summary>
	[JsonPropertyName("archived")]
	public Optional<Boolean> IsArchived { get; init; }

	/// <summary>
	/// New auto archive duration for this thread.
	/// </summary>
	[JsonPropertyName("auto_archive_duration")]
	public Optional<Int32> AutoArchiveDuration { get; init; }

	/// <summary>
	/// The new lock status for this thread.
	/// </summary>
	[JsonPropertyName("locked")]
	public Optional<Boolean> IsLocked { get; init; }

	/// <summary>
	/// Toggles whether non-moderators can add other non-moderators to this private thread.
	/// </summary>
	[JsonPropertyName("invitable")]
	public Optional<Boolean> Invitable { get; init; }

	/// <summary>
	/// Slowmode duration for this thread, in seconds.
	/// </summary>
	[JsonPropertyName("rate_limit_per_user")]
	public Optional<Int32?> Slowmode { get; init; }

	/// <summary>
	/// Flags for this thread.
	/// </summary>
	[JsonPropertyName("flags")]
	public Optional<DiscordChannelFlags> Flags { get; init; }

	/// <summary>
	/// The snowflake IDs of the tags that have been applied to this thread.
	/// </summary>
	[JsonPropertyName("applied_tags")]
	public Optional<IEnumerable<Int64>> AppliedTags { get; init; }
}

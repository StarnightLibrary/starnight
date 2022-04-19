namespace Starnight.Internal.Rest.Payloads.Channels;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a payload to PATCH /channels/:channel_id, where the channel ID points to a thread channel.
/// </summary>
public record ModifyThreadChannelRequestPayload
{
	/// <summary>
	/// New name for this thread channel.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("name")]
	public String? Name { get; init; }

	/// <summary>
	/// The new archive status for this thread.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("archived")]
	public Boolean? IsArchived { get; init; }

	/// <summary>
	/// New auto archive duration for this thread.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("auto_archive_duration")]
	public Int32? AutoArchiveDuration { get; init; }

	/// <summary>
	/// The new lock status for this thread.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("locked")]
	public Boolean? IsLocked { get; init; }

	/// <summary>
	/// Toggles whether non-moderators can add other non-moderators to this private thread.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("invitable")]
	public Boolean? Invitable { get; init; }

	/// <summary>
	/// Slowmode duration for this thread, in seconds.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("rate_limit_per_user")]
	public Int32? Slowmode { get; init; }
}

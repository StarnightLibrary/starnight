namespace Starnight.Internal.Entities.Channels.Threads;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Thread channel metadata object.
/// </summary>
public record DiscordThreadMetadata
{
	/// <summary>
	/// Gets whether this thread is archived.
	/// </summary>
	[JsonPropertyName("archived")]
	public Boolean Archived { get; init; }

	/// <summary>
	/// Duration in minutes when to automatically archive the thread. Can be set to 60, 1440, 4320 and 10080.
	/// </summary>
	[JsonPropertyName("auto_archive_duration")]
	public Int32 AutoArchiveDuration { get; init; }

	/// <summary>
	/// Last activity change in this thread, used to calculate auto archive.
	/// </summary>
	[JsonPropertyName("archive_timestamp")]
	public DateTime ArchiveTimestamp { get; init; }

	/// <summary>
	/// Gets whether this thread is locked.
	/// </summary>
	[JsonPropertyName("locked")]
	public Boolean Locked { get; init; }

	/// <summary>
	/// Gets whether non-moderators can add other non-moderators to the thread. Only available on private threads.
	/// </summary>
	[JsonPropertyName("invitable")]
	public Boolean? Invitable { get; init; }
}

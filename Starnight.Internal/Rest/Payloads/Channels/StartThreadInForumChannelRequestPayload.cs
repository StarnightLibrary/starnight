namespace Starnight.Internal.Rest.Payloads.Channels;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a payload to POST /channels/:channel_id/threads on a forum channel.
/// </summary>
public record StartThreadInForumChannelRequestPayload
{
	/// <summary>
	/// Contents of the first message in this forum thread.
	/// </summary>
	[JsonPropertyName("message")]
	public ForumThreadMessageParameters Message { get; init; } = null!;

	/// <summary>
	/// 1-100 characters, channel name for this thread.
	/// </summary>
	[JsonPropertyName("name")]
	public String Name { get; init; } = null!;

	/// <summary>
	/// Auto archive duration for this thread in minutes.
	/// </summary>
	[JsonPropertyName("auto_archive_duration")]
	public Optional<Int32> AutoArchiveDuration { get; init; }

	/// <summary>
	/// Slowmode for users in seconds.
	/// </summary>
	[JsonPropertyName("rate_limit_per_user")]
	public Optional<Int32?> Slowmode { get; init; }

	/// <summary>
	/// Files to be attached to this message.
	/// </summary>
	[JsonIgnore]
	public IEnumerable<DiscordAttachmentFile>? Files { get; init; }
}

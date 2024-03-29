namespace Starnight.Internal.Rest.Payloads.Channels;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a payload to POST /channels/:channel_id/messages/:message_id/threads
/// </summary>
public sealed record StartThreadFromMessageRequestPayload
{
	/// <summary>
	/// 1-100 characters, channel name for this thread.
	/// </summary>
	[JsonPropertyName("name")]
	public required String Name { get; init; }

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
}

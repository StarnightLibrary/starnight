namespace Starnight.Internal.Rest.Payloads.Channels;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Channels;

/// <summary>
/// Represents a payload to POST /channels/:channel_id/threads
/// </summary>
public sealed record StartThreadWithoutMessageRequestPayload
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

	/// <summary>
	/// The type of thread to be created.
	/// </summary>
	// This field is currently technically optional as per API spec, but this behaviour is slated for removal in the future.
	// Therefore, it is kept as a required field here.
	[JsonPropertyName("type")]
	public required DiscordChannelType ThreadType { get; init; }

	/// <summary>
	/// Indicates whether non-moderators can add members to this private thread.
	/// </summary>
	[JsonPropertyName("invitable")]
	public Optional<Boolean> Invitable { get; init; }
}

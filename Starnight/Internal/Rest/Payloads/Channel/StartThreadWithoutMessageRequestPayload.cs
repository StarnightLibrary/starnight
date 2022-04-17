namespace Starnight.Internal.Rest.Payloads.Channel;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Channels;

/// <summary>
/// Represents a payload to POST /channels/:channel_id/threads
/// </summary>
public record StartThreadWithoutMessageRequestPayload
{
	/// <summary>
	/// 1-100 characters, channel name for this thread.
	/// </summary>
	[JsonPropertyName("name")]
	public String Name { get; init; } = null!;

	/// <summary>
	/// Auto archive duration for this thread in minutes.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("auto_archive_duration")]
	public Int32? AutoArchiveDuration { get; init; }

	/// <summary>
	/// Slowmode for users in seconds.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("rate_limit_per_user")]
	public Int32? Slowmode { get; init; }

	/// <summary>
	/// The type of thread to be created.
	/// </summary>
	[JsonPropertyName("type")]
	public DiscordChannelType ThreadType { get; init; }

	/// <summary>
	/// Indicates whether non-moderators can add members to this private thread.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("invitable")]
	public Boolean? Invitable { get; init; }
}

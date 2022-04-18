namespace Starnight.Internal.Rest.Payloads.Channel;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Channels.Threads;

/// <summary>
/// Represents a response payload received from all endpoints which list archived threads: <br/>
/// GET /channels/:channel_id/threads/archived/public, <br/>
/// GET /channels/:channel_id/threads/archived/private and <br/>
/// GET /channels/:channel_id/users/@me/threads/archived/private.
/// </summary>
public record ListArchivedThreadsResponsePayload
{
	/// <summary>
	/// Thread channels included in this response.
	/// </summary>
	[JsonPropertyName("threads")]
	public IEnumerable<DiscordChannel> Threads { get; init; } = null!;

	/// <summary>
	/// Thread member objects for these respective threads.
	/// </summary>
	[JsonPropertyName("members")]
	public IEnumerable<DiscordThreadMember> ThreadMembers { get; init; } = null!;

	/// <summary>
	/// Indicates whether the returned list is exhaustive; whether subsequent calls could return more data.
	/// </summary>
	/// <remarks>
	/// This being set to true does not guarantee additional data may be included.
	/// </remarks>
	[JsonPropertyName("has_more")]
	public Boolean HasMore { get; init; }
}

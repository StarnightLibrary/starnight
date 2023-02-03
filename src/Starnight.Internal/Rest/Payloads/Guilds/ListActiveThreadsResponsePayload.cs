namespace Starnight.Internal.Rest.Payloads.Guilds;

using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Channels.Threads;

/// <summary>
/// Represents a response payload from GET guilds/:guild_id/threads/active
/// </summary>
public sealed record ListActiveThreadsResponsePayload
{
	/// <summary>
	/// All active threads in this guild the current user can access.
	/// </summary>
	[JsonPropertyName("threads")]
	public required IEnumerable<DiscordChannel> Threads { get; init; }

	/// <summary>
	/// An array of thread member objects for each active thread the current user has joined.
	/// </summary>
	[JsonPropertyName("members")]
	public required IEnumerable<DiscordThreadMember> MemberObjects { get; init; }
}

namespace Starnight.Internal.Rest.Payloads.Guilds;

using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Channels.Threads;

/// <summary>
/// Represents a response payload from GET guilds/:guild_id/threads/active
/// </summary>
public record ListActiveThreadsResponsePayload
{
	/// <summary>
	/// All active threads in this guild the current user can access.
	/// </summary>
	[JsonPropertyName("threads")]
	public IEnumerable<DiscordChannel> Threads { get; init; } = default!;

	/// <summary>
	/// An array of thread member objects for each active thread the current user has joined.
	/// </summary>
	[JsonPropertyName("members")]
	public IEnumerable<DiscordThreadMember> MemberObjects { get; init; } = default!;
}

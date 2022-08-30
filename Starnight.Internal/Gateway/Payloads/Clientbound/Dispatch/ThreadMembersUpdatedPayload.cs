namespace Starnight.Internal.Gateway.Payloads.Clientbound.Dispatch;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Channels.Threads;

/// <summary>
/// Represents a ThreadMembersUpdated event
/// </summary>
public sealed record ThreadMembersUpdatedPayload
{
	/// <summary>
	/// The ID of the thread.
	/// </summary>
	[JsonPropertyName("id")]
	public required Int64 ThreadId { get; init; }

	/// <summary>
	/// The ID of the guild this thread belongs to.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public required Int64 GuildId { get; init; }

	/// <summary>
	/// The approximate number of members in the thread, capped at 50.
	/// </summary>
	[JsonPropertyName("member_count")]
	public required Int32 MemberCount { get; init; }

	/// <summary>
	/// The users who were added to the thread.
	/// </summary>
	[JsonPropertyName("added_members")]
	public Optional<IEnumerable<DiscordThreadMember>> AddedMembers { get; init; }

	/// <summary>
	/// The users who were removed from the thread.
	/// </summary>
	[JsonPropertyName("removed_member_ids")]
	public Optional<IEnumerable<Int64>> RemovedMemberIds { get; init; }
}

namespace Starnight.Internal.Gateway.Objects.Clientbound.Dispatch;

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Channels.Threads;

/// <summary>
/// Inner payload for a ThreadListSync event.
/// </summary>
[StructLayout(LayoutKind.Auto)]
public record struct DiscordThreadListSyncEventObject
{
	/// <summary>
	/// The snowflake ID of the guild in question.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public required Int64 GuildId { get; init; }

	/// <summary>
	/// The parent channel IDs whose threads are being synchronized. If omitted, threads were synced for the entire guild.
	/// This may also contain channels with no active threads.
	/// </summary>
	[JsonPropertyName("channel_ids")]
	public Optional<IEnumerable<Int64>> ChannelIds { get; init; }

	/// <summary>
	/// All active threads in the given channels the current user can access.
	/// </summary>
	[JsonPropertyName("threads")]
	public required IEnumerable<DiscordChannel> Threads { get; init; }

	/// <summary>
	/// All member objects for the current user.
	/// </summary>
	[JsonPropertyName("members")]
	public IEnumerable<DiscordThreadMember> ThreadMemberObjects { get; init; }
}

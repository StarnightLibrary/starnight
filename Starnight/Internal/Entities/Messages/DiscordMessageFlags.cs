namespace Starnight.Internal.Entities.Messages;

using System;

/// <summary>
/// Bitfield flags representing message data.
/// </summary>
[Flags]
public enum DiscordMessageFlags
{
	/// <summary>
	/// This message was cross-posted to all subscribing channels.
	/// </summary>
	Crossposted = 1 << 0,

	/// <summary>
	/// This message was cross-posted from a channel.
	/// </summary>
	IsCrosspost = 1 << 1,

	/// <summary>
	/// Do not include embeds when serializing this message.
	/// </summary>
	SuppressEmbeds = 1 << 2,

	/// <summary>
	/// This crosspost's source message has been deleted.
	/// </summary>
	SourceMessageDeleted = 1 << 3,

	/// <summary>
	/// This message was sent via the urgent message system.
	/// </summary>
	Urgent = 1 << 4,

	/// <summary>
	/// This message has an associated thread, with the same ID as the message
	/// </summary>
	AssociatedThread = 1 << 5,

	/// <summary>
	/// This message is ephemeral.
	/// </summary>
	Ephemeral = 1 << 6,

	/// <summary>
	/// This message is a deferred interaction response.
	/// </summary>
	Loading = 1 << 7,

	/// <summary>
	/// This message failed to mention some roles and failed to add their members to the thread.
	/// </summary>
	FailedToMentionRolesInThread = 1 << 8
}

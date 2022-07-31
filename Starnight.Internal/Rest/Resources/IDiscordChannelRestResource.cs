namespace Starnight.Internal.Rest.Resources;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Channels.Threads;
using Starnight.Internal.Entities.Guilds.Invites;
using Starnight.Internal.Entities.Messages;
using Starnight.Internal.Entities.Users;
using Starnight.Internal.Exceptions;
using Starnight.Internal.Rest.Payloads.Channels;

/// <summary>
/// Represents a request wrapper for all requests to discords channel rest resource.
/// </summary>
public interface IDiscordChannelRestResource
{
	/// <summary>
	/// Returns a channel object for the given ID. If the channel is a thread channel, a
	/// <see cref="DiscordThreadMember"/> object is included in the returned channel.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the channel in question.</param>
	public ValueTask<DiscordChannel> GetChannelAsync
	(
		Int64 channelId
	);

	/// <summary>
	/// Modifies a group DM channel with the given parameters.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the group DM in question.</param>
	/// <param name="payload">Payload object containing the modification parameters.</param>
	/// <returns>The modified channel object.</returns>
	public ValueTask<DiscordChannel> ModifyChannelAsync
	(
		Int64 channelId,
		ModifyGroupDMRequestPayload payload
	);

	/// <summary>
	/// Modifies a guild channel with the given parameters.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the channel in question.</param>
	/// <param name="payload">Payload object containing the modification parameters.</param>
	/// <param name="reason">Optional audit log reason for the edit.</param>
	/// <returns>The modified channel object.</returns>
	public ValueTask<DiscordChannel> ModifyChannelAsync
	(
		Int64 channelId,
		ModifyGuildChannelRequestPayload payload,
		String? reason
	);

	/// <summary>
	/// Modifies a thread channel with the given parameters.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the channel in question.</param>
	/// <param name="payload">Payload object containing the modification parameters.</param>
	/// <param name="reason">Optional audit log reason for the edit.</param>
	/// <returns>The modified channel object.</returns>
	public ValueTask<DiscordChannel> ModifyChannelAsync
	(
		Int64 channelId,
		ModifyThreadChannelRequestPayload payload,
		String? reason
	);

	/// <summary>
	/// Deletes a channel. Deleting guild channels cannot be undone. DM channels, however, cannot be deleted
	/// and are restored by opening a direct message channel again.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the channel in question.</param>
	/// <param name="reason">Optional audit log reason if this is a guild channel.</param>
	/// <returns>The associated channel object.</returns>
	public ValueTask<DiscordChannel> DeleteChannelAsync
	(
		Int64 channelId,
		String? reason
	);

	/// <summary>
	/// Returns a set amount of messages, optionally before, after or around a certain message.
	/// </summary>
	/// <remarks>
	/// <c>around</c>, <c>before</c> and <c>after</c> are mutually exclusive. Only one may be passed. If multiple are passed,
	/// only the first one in the parameter list is respected, independent of the order they are passed in client code.
	/// </remarks>
	/// <param name="channelId">Snowflake identifier of the channel in question.</param>
	/// <param name="count">Maximum amount of messages to return.</param>
	/// <param name="around">Snowflake identifier of the center message of the requested block.</param>
	/// <param name="before">Snowflake identifier of the first older message than the requested block.</param>
	/// <param name="after">Snowflake identifier of the first newer message than the requested block.</param>
	public ValueTask<IEnumerable<DiscordMessage>> GetChannelMessagesAsync
	(
		Int64 channelId,
		Int32 count,
		Int64? around,
		Int64? before,
		Int64? after
	);

	/// <summary>
	/// Gets a message by snowflake identifier.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the message's parent channel.</param>
	/// <param name="messageId">Snowflake identifier of the message in question.</param>
	public ValueTask<DiscordMessage> GetChannelMessageAsync
	(
		Int64 channelId,
		Int64 messageId
	);

	/// <summary>
	/// Creates a new message in a channel.
	/// </summary>
	/// <param name="channelId">snowflake identifier of the message's target channel.</param>
	/// <param name="payload">Message creation payload including potential attachment files.</param>
	/// <returns>The newly created message object.</returns>
	public ValueTask<DiscordMessage> CreateMessageAsync
	(
		Int64 channelId,
		CreateMessageRequestPayload payload
	);

	/// <summary>
	/// Publishes a message in an announcement channel to following channels.
	/// </summary>
	/// <param name="channelId">Source announcement channel for this message.</param>
	/// <param name="messageId">Snowflake identifier of the message.</param>
	public ValueTask<DiscordMessage> CrosspostMessageAsync
	(
		Int64 channelId,
		Int64 messageId
	);

	/// <summary>
	/// Creates a reaction with the given emoji on the specified message.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the message's parent channel.</param>
	/// <param name="messageId">Snowflake identifier of the message in question.</param>
	/// <param name="emoji">String representation of the emoji.</param>
	/// <returns>Whether the reaction was added successfully.</returns>
	public ValueTask<Boolean> CreateReactionAsync
	(
		Int64 channelId,
		Int64 messageId,
		String emoji
	);

	/// <summary>
	/// Deletes your own reaction with the specified emoji on the specified message.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the message's parent channel.</param>
	/// <param name="messageId">Snowflake identifier of the message in question.</param>
	/// <param name="emoji">String representation of the emoji.</param>
	/// <returns>Whether the reaction was removed successfully.</returns>
	public ValueTask<Boolean> DeleteOwnReactionAsync
	(
		Int64 channelId,
		Int64 messageId,
		String emoji
	);

	/// <summary>
	/// Deletes the specified user's reaction with the specified emoji on the specified message.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the message's parent channel.</param>
	/// <param name="messageId">Snowflake identifier of the message in question.</param>
	/// <param name="userId">Snowflake identifier of the user in question.</param>
	/// <param name="emoji">String representation of the emoji.</param>
	/// <returns>Whether the reaction was removed successfully.</returns>
	public ValueTask<Boolean> DeleteUserReactionAsync
	(
		Int64 channelId,
		Int64 messageId,
		Int64 userId,
		String emoji
	);

	/// <summary>
	/// Gets a list of users that reacted with the given emoji.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the message's parent channel.</param>
	/// <param name="messageId">Snowflake identifier of the message in question.</param>
	/// <param name="emoji">String representation of the queried emoji.</param>
	/// <param name="after">Specifies a minimum user ID to return from, to paginate queries.</param>
	/// <param name="limit">Maximum amount of users to return. Defaults to 25.</param>
	public ValueTask<IEnumerable<DiscordUser>> GetReactionsAsync
	(
		Int64 channelId,
		Int64 messageId,
		String emoji,
		Int64? after,
		Int32? limit
	);

	/// <summary>
	/// Deletes all reactions on the given message.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the message's parent channel.</param>
	/// <param name="messageId">Snowflake identifier of the message in question.</param>
	public ValueTask DeleteAllReactionsAsync
	(
		Int64 channelId,
		Int64 messageId
	);

	/// <summary>
	/// Deletes all reactions with a specific emoji from the specified message.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the message's parent channel.</param>
	/// <param name="messageId">Snowflake identifier of the message in question.</param>
	/// <param name="emoji">String representation of the emoji in question.</param>
	public ValueTask DeleteEmojiReactionsAsync
	(
		Int64 channelId,
		Int64 messageId,
		String emoji
	);

	/// <summary>
	/// Edits the given message.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the message's parent channel.</param>
	/// <param name="messageId">Snowflake identifier of the message in question.</param>
	/// <param name="payload">Edit payload.</param>
	public ValueTask<DiscordMessage> EditMessageAsync
	(
		Int64 channelId,
		Int64 messageId,
		EditMessageRequestPayload payload
	);

	/// <summary>
	/// Deletes a message, potentially passing an audit log reason.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the message's parent channel.</param>
	/// <param name="messageId">Snowflake identifier of the message.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <returns>Whether the message was successfully deleted.</returns>
	public ValueTask<Boolean> DeleteMessageAsync
	(
		Int64 channelId,
		Int64 messageId,
		String? reason
	);

	/// <summary>
	/// Bulk deletes the given messages.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the message's parent channel.</param>
	/// <param name="messageIds">Up to 100 message IDs to delete. If any messages older than two weeks are included,
	/// or any of the IDs are duplicated, the entire request will fail.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <returns>Whether the messages were deleted successfully.</returns>
	public ValueTask<Boolean> BulkDeleteMessagesAsync
	(
		Int64 channelId,
		IEnumerable<Int64> messageIds,
		String? reason
	);

	/// <summary>
	/// Edits a permission overwrite for a guild channel.
	/// </summary>
	/// <param name="channelId">Snowflake identifier for the channel in question.</param>
	/// <param name="overwriteId">Snowflake identifier of the entity (role/user) this overwrite targets.</param>
	/// <param name="payload">Edit payload.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <returns>Whether the overwrite was successfully edited.</returns>
	public ValueTask<Boolean> EditChannelPermissionsAsync
	(
		Int64 channelId,
		Int64 overwriteId,
		EditChannelPermissionsRequestPayload payload,
		String? reason
	);

	/// <summary>
	/// Returns a list of invite objects with invite metadata pointing to this channel.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the channel in question.</param>
	public ValueTask<IEnumerable<DiscordInvite>> GetChannelInvitesAsync
	(
		Int64 channelId
	);

	/// <summary>
	/// Creates an invite on the specified channel.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the channel in question.</param>
	/// <param name="payload">Additional invite metadata.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <returns>The newly created invite object.</returns>
	public ValueTask<DiscordInvite> CreateChannelInviteAsync
	(
		Int64 channelId,
		CreateChannelInviteRequestPayload payload,
		String? reason
	);

	/// <summary>
	/// Deletes a channel permission overwrite.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the channel in question.</param>
	/// <param name="overwriteId">Snowflake identifier of the object this overwrite points to.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <returns>Whether the deletion was successful.</returns>
	public ValueTask<Boolean> DeleteChannelPermissionOverwriteAsync
	(
		Int64 channelId,
		Int64 overwriteId,
		String? reason
	);

	/// <summary>
	/// Follows a news channel.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the news channel to follow.</param>
	/// <param name="targetChannelId">Snowflake identifier of the channel you want messages to be cross-posted into.</param>
	/// <returns></returns>
	public ValueTask<DiscordFollowedChannel> FollowNewsChannelAsync
	(
		Int64 channelId,
		Int64 targetChannelId
	);

	/// <summary>
	/// Triggers the typing indicator for the current user in the given channel.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the channel in question.</param>
	public ValueTask TriggerTypingIndicatorAsync
	(
		Int64 channelId
	);

	/// <summary>
	/// Returns all pinned messages as message objects.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the messages' parent channel.</param>
	public ValueTask<IEnumerable<DiscordMessage>> GetPinnedMessagesAsync
	(
		Int64 channelId
	);

	/// <summary>
	/// Pins a message.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the message's parent channel.</param>
	/// <param name="messageId">Snowflake identifier of the message in question.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <returns>Whether the message was successfully pinned.</returns>
	public ValueTask<Boolean> PinMessageAsync
	(
		Int64 channelId,
		Int64 messageId,
		String? reason
	);

	/// <summary>
	/// Unpins a message.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the message's parent channel.</param>
	/// <param name="messageId">Snowflake identifier of the message in question.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <returns>Whether the message was successfully unpinned.</returns>
	public ValueTask<Boolean> UnpinMessageAsync
	(
		Int64 channelId,
		Int64 messageId,
		String? reason
	);

	/// <summary>
	/// Adds the given user to a specified group DM channel.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the group DM channel in question.</param>
	/// <param name="userId">Snowflake identifier of the user in question.</param>
	/// <param name="payload">Request payload, containing the access token needed.</param>
	public ValueTask AddGroupDMRecipientAsync
	(
		Int64 channelId,
		Int64 userId,
		AddGroupDMRecipientRequestPayload payload
	);

	/// <summary>
	/// Removes the given user from the given group DM channel.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the group DM channel in question.</param>
	/// <param name="userId">Snowflake identifier of the user in question.</param>
	public ValueTask RemoveGroupDMRecipientAsync
	(
		Int64 channelId,
		Int64 userId
	);

	/// <summary>
	/// Creates a new thread channel from the given message.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the thread's parent channel.</param>
	/// <param name="messageId">Snowflake identifier of the thread's parent message.</param>
	/// <param name="payload">Request payload for this request.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <returns>The newly created thread channel.</returns>
	public ValueTask<DiscordChannel> StartThreadFromMessageAsync
	(
		Int64 channelId,
		Int64 messageId,
		StartThreadFromMessageRequestPayload payload,
		String? reason
	);

	/// <summary>
	/// Creates a new thread channel without a message.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the thread's parent channel.</param>
	/// <param name="payload">Request payload for this request.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <returns>The newly created thread channel.</returns>
	public ValueTask<DiscordChannel> StartThreadWithoutMessageAsync
	(
		Int64 channelId,
		StartThreadWithoutMessageRequestPayload payload,
		String? reason
	);

	/// <summary>
	/// Creates a new thread with a starting message in a forum channel.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the parent forum channel.</param>
	/// <param name="payload">A <see cref="CreateMessageRequestPayload"/> combined with a
	/// <see cref="StartThreadFromMessageRequestPayload"/>. A new message is created, then a thread is started from it.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <returns>The newly created thread channel.</returns>
	public ValueTask<DiscordChannel> StartThreadInForumChannelAsync
	(
		Int64 channelId,
		StartThreadInForumChannelRequestPayload payload,
		String? reason
	);

	/// <summary>
	/// Joins the current user into a thread.
	/// </summary>
	/// <param name="threadId">Snowflake identifier of the thread channel to be joined.</param>
	/// <returns>Whether the operation was successful.</returns>
	public ValueTask<Boolean> JoinThreadAsync
	(
		Int64 threadId
	);

	/// <summary>
	/// Adds another member into a thread.
	/// </summary>
	/// <param name="threadId">Snowflake identifier of the thread to be joined.</param>
	/// <param name="userId">Snowflake identifier of the user to join into the thread.</param>
	/// <returns>Whether the operation was successful.</returns>
	public ValueTask<Boolean> AddToThreadAsync
	(
		Int64 threadId,
		Int64 userId
	);

	/// <summary>
	/// Leaves a thread as the current bot.
	/// </summary>
	/// <param name="threadId">Snowflake identifier of the thread to be left.</param>
	/// <returns>Whether the operation was successful.</returns>
	public ValueTask<Boolean> LeaveThreadAsync
	(
		Int64 threadId
	);

	/// <summary>
	/// Removes another user from a thread.
	/// </summary>
	/// <param name="threadId">Snowflake identifier of the thread to be left.</param>
	/// <param name="userId">Snowflake identifier of the user to be removed.</param>
	/// <returns>Whether the operation was successful.</returns>
	public ValueTask<Boolean> RemoveFromThreadAsync
	(
		Int64 threadId,
		Int64 userId
	);

	/// <summary>
	/// Returns a thread member object for the specified user.
	/// </summary>
	/// <param name="threadId">Snowflake identifier of the thread to obtain data from.</param>
	/// <param name="userId">Snowflake identifier of the user to obtain data for.</param>
	public ValueTask<DiscordThreadMember> GetThreadMemberAsync
	(
		Int64 threadId,
		Int64 userId
	);

	/// <summary>
	/// Returns a list of all thread members for the specified thread.
	/// </summary>
	/// <param name="threadId">Snowflake identifier fo the thread to obtain data from.</param>
	public ValueTask<IEnumerable<DiscordThreadMember>> ListThreadMembersAsync
	(
		Int64 threadId
	);

	/// <summary>
	/// Returns all public, archived threads for this channel including respective thread member objects.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the thread's parent channel.</param>
	/// <param name="before">Timestamp to filter threads by: only threads archived before this timestamp will be returned.</param>
	/// <param name="limit">Maximum amount of threads to return.</param>
	public ValueTask<ListArchivedThreadsResponsePayload> ListPublicArchivedThreadsAsync
	(
		Int64 channelId,
		DateTimeOffset? before,
		Int32? limit
	);

	/// <summary>
	/// Returns all private, accessible, archived threads for this channel including respective thread member objects.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the thread's parent channel.</param>
	/// <param name="before">Timestamp to filter threads by: only threads archived before this timestamp will be returned.</param>
	/// <param name="limit">Maximum amount of threads to return.</param>
	public ValueTask<ListArchivedThreadsResponsePayload> ListPrivateArchivedThreadsAsync
	(
		Int64 channelId,
		DateTimeOffset? before,
		Int32? limit
	);

	/// <summary>
	/// Returns a list of joined, private, archived threads.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of their parent channel.</param>
	/// <param name="before">Timestamp to act as upper boundary for archival dates.</param>
	/// <param name="limit">Maximum amount of threads to return from this request.</param>
	public ValueTask<ListArchivedThreadsResponsePayload> ListJoinedPrivateArchivedThreadsAsync
	(
		Int64 channelId,
		DateTimeOffset? before,
		Int32? limit
	);
}

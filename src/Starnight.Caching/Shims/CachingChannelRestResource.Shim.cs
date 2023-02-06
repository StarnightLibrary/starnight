namespace Starnight.Caching.Shims;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Starnight.Caching;
using Starnight.Caching.Services;
using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Channels.Threads;
using Starnight.Internal.Entities.Guilds.Invites;
using Starnight.Internal.Entities.Messages;
using Starnight.Internal.Entities.Users;
using Starnight.Internal.Rest.Payloads.Channels;
using Starnight.Internal.Rest.Resources;
using Starnight.SourceGenerators.Shims;

/// <summary>
/// Represents a shim over the present channel commands rest resource, updating and corroborating from
/// cache where possible.
/// </summary>
[Shim<IDiscordChannelRestResource>]
public partial class CachingChannelRestResource : IDiscordChannelRestResource
{
	public readonly IDiscordChannelRestResource underlying;
	public readonly IStarnightCacheService cache;

	public CachingChannelRestResource
	(
		IDiscordChannelRestResource underlying,
		IStarnightCacheService cache
	)
	{
		this.underlying = underlying;
		this.cache = cache;
	}
	/// <inheritdoc/>
	public async ValueTask<DiscordChannel> GetChannelAsync
	(
		Int64 channelId,
		CancellationToken ct = default
	)
	{
		DiscordChannel channel = await this.underlying.GetChannelAsync
		(
			channelId,
			ct
		);

		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetChannelKey
			(
				channelId
			),
			channel
		);

		return channel;
	}

	/// <inheritdoc/>
	public async ValueTask<IEnumerable<DiscordInvite>> GetChannelInvitesAsync
	(
		Int64 channelId,
		CancellationToken ct = default
	)
	{
		IEnumerable<DiscordInvite> invites = await this.underlying.GetChannelInvitesAsync
		(
			channelId,
			ct
		);

		await Parallel.ForEachAsync
		(
			invites,
			async (xm, _) => await this.cache.CacheObjectAsync
			(
				KeyHelper.GetInviteKey
				(
					xm.Code
				),
				xm
			)
		);

		return invites;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordMessage> GetChannelMessageAsync
	(
		Int64 channelId,
		Int64 messageId,
		CancellationToken ct = default
	)
	{
		DiscordMessage message = await this.underlying.GetChannelMessageAsync
		(
			channelId,
			messageId,
			ct
		);

		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetMessageKey
			(
				messageId
			),
			message
		);

		return message;
	}

	/// <inheritdoc/>
	public async ValueTask<IEnumerable<DiscordMessage>> GetChannelMessagesAsync
	(
		Int64 channelId,
		Int32 count,
		Int64? around = null,
		Int64? before = null,
		Int64? after = null,
		CancellationToken ct = default
	)
	{
		IEnumerable<DiscordMessage> messages = await this.underlying.GetChannelMessagesAsync
		(
			channelId,
			count,
			around,
			before,
			after,
			ct
		);

		await Parallel.ForEachAsync
		(
			messages,
			async (xm, _) => await this.cache.CacheObjectAsync
			(
				KeyHelper.GetMessageKey
				(
					xm.Id
				),
				xm
			)
		);

		return messages;
	}

	/// <inheritdoc/>
	public async ValueTask<IEnumerable<DiscordMessage>> GetPinnedMessagesAsync
	(
		Int64 channelId,
		CancellationToken ct = default
	)
	{
		IEnumerable<DiscordMessage> messages = await this.underlying.GetPinnedMessagesAsync
		(
			channelId,
			ct
		);

		await Parallel.ForEachAsync
		(
			messages,
			async (xm, _) => await this.cache.CacheObjectAsync
			(
				KeyHelper.GetMessageKey
				(
					xm.Id
				),
				xm
			)
		);

		return messages;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordThreadMember> GetThreadMemberAsync
	(
		Int64 threadId,
		Int64 userId,
		Boolean? withMember = null,
		CancellationToken ct = default
	)
	{
		DiscordThreadMember threadMember = await this.underlying.GetThreadMemberAsync
		(
			threadId,
			userId,
			withMember,
			ct
		);

		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetThreadMemberKey
			(
				threadId,
				userId
			),
			threadMember
		);

		return threadMember;
	}

	/// <inheritdoc/>
	public async ValueTask<ListArchivedThreadsResponsePayload> ListJoinedPrivateArchivedThreadsAsync
	(
		Int64 channelId,
		DateTimeOffset? before,
		Int32? limit = null,
		CancellationToken ct = default
	)
	{
		ListArchivedThreadsResponsePayload response = await this.underlying.ListJoinedPrivateArchivedThreadsAsync
		(
			channelId,
			before,
			limit,
			ct
		);

		await Parallel.ForEachAsync
		(
			response.Threads,
			async (xm, _) => await this.cache.CacheObjectAsync
			(
				KeyHelper.GetChannelKey
				(
					xm.Id
				),
				xm
			)
		);

		await Parallel.ForEachAsync
		(
			response.ThreadMembers,
			async (xm, _) => await this.cache.CacheObjectAsync
			(
				KeyHelper.GetThreadMemberKey
				(
					xm.ThreadId,
					xm.UserId
				),
				xm
			)
		);

		return response;
	}

	/// <inheritdoc/>
	public async ValueTask<ListArchivedThreadsResponsePayload> ListPrivateArchivedThreadsAsync
	(
		Int64 channelId,
		DateTimeOffset? before,
		Int32? limit = null,
		CancellationToken ct = default
	)
	{
		ListArchivedThreadsResponsePayload response = await this.underlying.ListPrivateArchivedThreadsAsync
		(
			channelId,
			before,
			null,
			ct
		);

		await Parallel.ForEachAsync
		(
			response.Threads,
			async (xm, _) => await this.cache.CacheObjectAsync
			(
				KeyHelper.GetChannelKey
				(
					xm.Id
				),
				xm
			)
		);

		await Parallel.ForEachAsync
		(
			response.ThreadMembers,
			async (xm, _) => await this.cache.CacheObjectAsync
			(
				KeyHelper.GetThreadMemberKey
				(
					xm.ThreadId,
					xm.UserId
				),
				xm
			)
		);

		return response;
	}

	/// <inheritdoc/>
	public async ValueTask<ListArchivedThreadsResponsePayload> ListPublicArchivedThreadsAsync
	(
		Int64 channelId,
		DateTimeOffset? before,
		Int32? limit = null,
		CancellationToken ct = default
	)
	{
		ListArchivedThreadsResponsePayload response = await this.underlying.ListPublicArchivedThreadsAsync
		(
			channelId,
			before,
			limit,
			ct
		);

		await Parallel.ForEachAsync
		(
			response.Threads,
			async (xm, _) => await this.cache.CacheObjectAsync
			(
				KeyHelper.GetChannelKey
				(
					xm.Id
				),
				xm
			)
		);

		await Parallel.ForEachAsync
		(
			response.ThreadMembers,
			async (xm, _) => await this.cache.CacheObjectAsync
			(
				KeyHelper.GetThreadMemberKey
				(
					xm.ThreadId,
					xm.UserId
				),
				xm
			)
		);

		return response;
	}

	/// <inheritdoc/>
	public async ValueTask<IEnumerable<DiscordThreadMember>> ListThreadMembersAsync
	(
		Int64 threadId,
		Boolean? withMember = null,
		Int64? after = null,
		Int32? limit = null,
		CancellationToken ct = default
	)
	{
		IEnumerable<DiscordThreadMember> members = await this.underlying.ListThreadMembersAsync
		(
			threadId,
			withMember,
			after,
			limit,
			ct
		);

		await Parallel.ForEachAsync
		(
			members,
			async (xm, _) => await this.cache.CacheObjectAsync
			(
				KeyHelper.GetThreadMemberKey
				(
					xm.ThreadId,
					xm.UserId
				),
				xm
			)
		);

		return members;
	}

	public partial ValueTask AddGroupDMRecipientAsync(Int64 channelId, Int64 userId, AddGroupDMRecipientRequestPayload payload, CancellationToken ct = default);
	public partial ValueTask<Boolean> AddToThreadAsync(Int64 threadId, Int64 userId, CancellationToken ct = default);
	public partial ValueTask<Boolean> CreateReactionAsync(Int64 channelId, Int64 messageId, String emoji, CancellationToken ct = default);
	public partial ValueTask DeleteAllReactionsAsync(Int64 channelId, Int64 messageId, CancellationToken ct = default);
	public partial ValueTask<Boolean> DeleteChannelPermissionOverwriteAsync(Int64 channelId, Int64 overwriteId, String? reason = null, CancellationToken ct = default);
	public partial ValueTask DeleteEmojiReactionsAsync(Int64 channelId, Int64 messageId, String emoji, CancellationToken ct = default);
	public partial ValueTask<Boolean> DeleteOwnReactionAsync(Int64 channelId, Int64 messageId, String emoji, CancellationToken ct = default);
	public partial ValueTask<Boolean> DeleteUserReactionAsync(Int64 channelId, Int64 messageId, Int64 userId, String emoji, CancellationToken ct = default);
	public partial ValueTask<Boolean> EditChannelPermissionsAsync(Int64 channelId, Int64 overwriteId, EditChannelPermissionsRequestPayload payload, String? reason = null, CancellationToken ct = default);
	public partial ValueTask<DiscordFollowedChannel> FollowNewsChannelAsync(Int64 channelId, Int64 targetChannelId, CancellationToken ct = default);
	public partial ValueTask<Boolean> JoinThreadAsync(Int64 threadId, CancellationToken ct = default);
	public partial ValueTask<Boolean> LeaveThreadAsync(Int64 threadId, CancellationToken ct = default);
	public partial ValueTask<Boolean> PinMessageAsync(Int64 channelId, Int64 messageId, String? reason = null, CancellationToken ct = default);
	public partial ValueTask<Boolean> RemoveFromThreadAsync(Int64 threadId, Int64 userId, CancellationToken ct = default);
	public partial ValueTask RemoveGroupDMRecipientAsync(Int64 channelId, Int64 userId, CancellationToken ct = default);
	public partial ValueTask TriggerTypingIndicatorAsync(Int64 channelId, CancellationToken ct = default);
	public partial ValueTask<Boolean> UnpinMessageAsync(Int64 channelId, Int64 messageId, String? reason = null, CancellationToken ct = default);
	public partial ValueTask<DiscordChannel> ModifyChannelAsync(Int64 channelId, ModifyGroupDMRequestPayload payload, CancellationToken ct = default);
	public partial ValueTask<DiscordChannel> ModifyChannelAsync(Int64 channelId, ModifyGuildChannelRequestPayload payload, String? reason = null, CancellationToken ct = default);
	public partial ValueTask<DiscordChannel> ModifyChannelAsync(Int64 channelId, ModifyThreadChannelRequestPayload payload, String? reason = null, CancellationToken ct = default);
	public partial ValueTask<DiscordChannel> DeleteChannelAsync(Int64 channelId, String? reason = null, CancellationToken ct = default);
	public partial ValueTask<DiscordMessage> CreateMessageAsync(Int64 channelId, CreateMessageRequestPayload payload, CancellationToken ct = default);
	public partial ValueTask<DiscordMessage> CrosspostMessageAsync(Int64 channelId, Int64 messageId, CancellationToken ct = default);
	public partial ValueTask<IEnumerable<DiscordUser>> GetReactionsAsync(Int64 channelId, Int64 messageId, String emoji, Int64? after = null, Int32? limit = null, CancellationToken ct = default);
	public partial ValueTask<DiscordMessage> EditMessageAsync(Int64 channelId, Int64 messageId, EditMessageRequestPayload payload, CancellationToken ct = default);
	public partial ValueTask<Boolean> DeleteMessageAsync(Int64 channelId, Int64 messageId, String? reason = null, CancellationToken ct = default);
	public partial ValueTask<Boolean> BulkDeleteMessagesAsync(Int64 channelId, IEnumerable<Int64> messageIds, String? reason = null, CancellationToken ct = default);
	public partial ValueTask<DiscordInvite> CreateChannelInviteAsync(Int64 channelId, CreateChannelInviteRequestPayload payload, String? reason = null, CancellationToken ct = default);
	public partial ValueTask<DiscordChannel> StartThreadFromMessageAsync(Int64 channelId, Int64 messageId, StartThreadFromMessageRequestPayload payload, String? reason = null, CancellationToken ct = default);
	public partial ValueTask<DiscordChannel> StartThreadWithoutMessageAsync(Int64 channelId, StartThreadWithoutMessageRequestPayload payload, String? reason = null, CancellationToken ct = default);
	public partial ValueTask<DiscordChannel> StartThreadInForumChannelAsync(Int64 channelId, StartThreadInForumChannelRequestPayload payload, String? reason = null, CancellationToken ct = default);
}

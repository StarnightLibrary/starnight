namespace Starnight.Caching.Shims;

using System;
using System.Collections.Generic;
using System.Linq;
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

/// <summary>
/// Represents a shim over the present application commands rest resource, updating and corroborating from
/// cache where possible.
/// </summary>
public partial class CachingChannelRestResource : IDiscordChannelRestResource
{
	public readonly IDiscordChannelRestResource __underlying;
	public readonly IStarnightCacheService __cache;

	public CachingChannelRestResource
	(
		IDiscordChannelRestResource underlying,
		IStarnightCacheService cache
	)
	{
		this.__underlying = underlying;
		this.__cache = cache;
	}

	/// <inheritdoc/>
	public async ValueTask<Boolean> BulkDeleteMessagesAsync
	(
		Int64 channelId,
		IEnumerable<Int64> messageIds,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		Boolean value = await this.__underlying.BulkDeleteMessagesAsync
		(
			channelId,
			messageIds,
			reason,
			ct
		);

		foreach(Int64 id in messageIds)
		{
			_ = await this.__cache.EvictObjectAsync<DiscordMessage>
			(
				KeyHelper.GetMessageKey
				(
					id
				)
			);
		}

		return value;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordInvite> CreateChannelInviteAsync
	(
		Int64 channelId,
		CreateChannelInviteRequestPayload payload,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		DiscordInvite invite = await this.__underlying.CreateChannelInviteAsync
		(
			channelId,
			payload,
			reason,
			ct
		);

		await this.__cache.CacheObjectAsync
		(
			KeyHelper.GetInviteKey
			(
				invite.Code
			),
			invite
		);

		return invite;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordMessage> CreateMessageAsync
	(
		Int64 channelId,
		CreateMessageRequestPayload payload,
		CancellationToken ct = default
	)
	{
		DiscordMessage message = await this.__underlying.CreateMessageAsync
		(
			channelId,
			payload,
			ct
		);

		await this.__cache.CacheObjectAsync
		(
			KeyHelper.GetMessageKey
			(
				message.Id
			),
			message
		);

		return message;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordMessage> CrosspostMessageAsync
	(
		Int64 channelId,
		Int64 messageId,
		CancellationToken ct = default
	)
	{
		DiscordMessage message = await this.__underlying.CrosspostMessageAsync
		(
			channelId,
			messageId,
			ct
		);

		await this.__cache.CacheObjectAsync
		(
			KeyHelper.GetMessageKey
			(
				message.Id
			),
			message
		);

		return message;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordChannel> DeleteChannelAsync
	(
		Int64 channelId,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		_ = await this.__cache.EvictObjectAsync<DiscordChannel>
		(
			KeyHelper.GetChannelKey
			(
				channelId
			)
		);

		return await this.__underlying.DeleteChannelAsync
		(
			channelId,
			reason,
			ct
		);
	}

	/// <inheritdoc/>
	public async ValueTask<Boolean> DeleteChannelPermissionOverwriteAsync
	(
		Int64 channelId,
		Int64 overwriteId,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		Boolean value = await this.__underlying.DeleteChannelPermissionOverwriteAsync
		(
			channelId,
			overwriteId,
			reason,
			ct
		);

		if(!value)
		{
			return value;
		}

		String key = KeyHelper.GetChannelKey
		(
			channelId
		);

		DiscordChannel? parent = await this.__cache.RetrieveObjectAsync<DiscordChannel>
		(
			key
		);

		if(parent is null || !parent.Overwrites.IsDefined)
		{
			return value;
		}

		parent = parent with
		{
			Overwrites = new
			(
				parent.Overwrites.Value.Where
				(
					xm => xm.Id != overwriteId
				)
			)
		};

		await this.__cache.CacheObjectAsync
		(
			key,
			parent!
		);

		return value;
	}

	public ValueTask DeleteEmojiReactionsAsync(Int64 channelId, Int64 messageId, String emoji, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<Boolean> DeleteMessageAsync(Int64 channelId, Int64 messageId, String? reason = null, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<Boolean> EditChannelPermissionsAsync(Int64 channelId, Int64 overwriteId, EditChannelPermissionsRequestPayload payload, String? reason = null, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<DiscordMessage> EditMessageAsync(Int64 channelId, Int64 messageId, EditMessageRequestPayload payload, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<DiscordFollowedChannel> FollowNewsChannelAsync(Int64 channelId, Int64 targetChannelId, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<DiscordChannel> GetChannelAsync(Int64 channelId, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<IEnumerable<DiscordInvite>> GetChannelInvitesAsync(Int64 channelId, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<DiscordMessage> GetChannelMessageAsync(Int64 channelId, Int64 messageId, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<IEnumerable<DiscordMessage>> GetChannelMessagesAsync(Int64 channelId, Int32 count, Int64? around = null, Int64? before = null, Int64? after = null, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<IEnumerable<DiscordMessage>> GetPinnedMessagesAsync(Int64 channelId, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<IEnumerable<DiscordUser>> GetReactionsAsync(Int64 channelId, Int64 messageId, String emoji, Int64? after = null, Int32? limit = null, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<DiscordThreadMember> GetThreadMemberAsync(Int64 threadId, Int64 userId, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<ListArchivedThreadsResponsePayload> ListJoinedPrivateArchivedThreadsAsync(Int64 channelId, DateTimeOffset? before, Int32? limit = null, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<ListArchivedThreadsResponsePayload> ListPrivateArchivedThreadsAsync(Int64 channelId, DateTimeOffset? before, Int32? limit = null, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<ListArchivedThreadsResponsePayload> ListPublicArchivedThreadsAsync(Int64 channelId, DateTimeOffset? before, Int32? limit = null, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<IEnumerable<DiscordThreadMember>> ListThreadMembersAsync(Int64 threadId, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<DiscordChannel> ModifyChannelAsync(Int64 channelId, ModifyGroupDMRequestPayload payload, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<DiscordChannel> ModifyChannelAsync(Int64 channelId, ModifyGuildChannelRequestPayload payload, String? reason = null, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<DiscordChannel> ModifyChannelAsync(Int64 channelId, ModifyThreadChannelRequestPayload payload, String? reason = null, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<Boolean> PinMessageAsync(Int64 channelId, Int64 messageId, String? reason = null, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<Boolean> RemoveFromThreadAsync(Int64 threadId, Int64 userId, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<DiscordChannel> StartThreadFromMessageAsync(Int64 channelId, Int64 messageId, StartThreadFromMessageRequestPayload payload, String? reason = null, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<DiscordChannel> StartThreadInForumChannelAsync(Int64 channelId, StartThreadInForumChannelRequestPayload payload, String? reason = null, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<DiscordChannel> StartThreadWithoutMessageAsync(Int64 channelId, StartThreadWithoutMessageRequestPayload payload, String? reason = null, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<Boolean> UnpinMessageAsync(Int64 channelId, Int64 messageId, String? reason = null, CancellationToken ct = default) => throw new NotImplementedException();
}

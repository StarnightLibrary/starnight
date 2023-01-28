namespace Starnight.Caching.Listeners;

using System.Threading.Tasks;

using Starnight.Caching.Services;
using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Channels.Threads;
using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Entities.Stickers;
using Starnight.Internal.Gateway.Events.Inbound.Dispatch;
using Starnight.Internal.Gateway.Listeners;

/// <summary>
/// Represents a listener handling incoming create/update events.
/// </summary>
internal class PreEventListener :
	IListener<DiscordChannelCreatedEvent>,
	IListener<DiscordChannelUpdatedEvent>,
	IListener<DiscordThreadCreatedEvent>,
	IListener<DiscordThreadUpdatedEvent>,
	IListener<DiscordThreadListSyncEvent>,
	IListener<DiscordThreadMemberUpdatedEvent>,
	IListener<DiscordThreadMembersUpdatedEvent>,
	IListener<DiscordGuildCreatedEvent>,
	IListener<DiscordGuildUpdatedEvent>,
	IListener<DiscordGuildEmojisUpdatedEvent>,
	IListener<DiscordGuildStickersUpdatedEvent>
{
	private readonly IStarnightCacheService cache;

	public PreEventListener
	(
		IStarnightCacheService cache
	)
		=> this.cache = cache;

	public async ValueTask ListenAsync
	(
		DiscordChannelCreatedEvent @event
	)
	{
		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetChannelKey
			(
				@event.Data.Id
			),
			@event.Data
		);
	}

	public async ValueTask ListenAsync
	(
		DiscordChannelUpdatedEvent @event
	)
	{
		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetChannelKey
			(
				@event.Data.Id
			),
			@event.Data
		);
	}

	public async ValueTask ListenAsync
	(
		DiscordThreadCreatedEvent @event
	)
	{
		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetChannelKey
			(
				@event.Data.Id
			),
			@event.Data
		);
	}

	public async ValueTask ListenAsync
	(
		DiscordThreadUpdatedEvent @event
	)
	{
		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetChannelKey
			(
				@event.Data.Id
			),
			@event.Data
		);
	}

	public async ValueTask ListenAsync
	(
		DiscordThreadListSyncEvent @event
	)
	{
		foreach(DiscordChannel thread in @event.Data.Threads)
		{
			await this.cache.CacheObjectAsync
			(
				KeyHelper.GetChannelKey
				(
					thread.Id
				),
				thread
			);
		}

		foreach(DiscordThreadMember threadMember in @event.Data.ThreadMemberObjects)
		{
			await this.cache.CacheObjectAsync
			(
				KeyHelper.GetThreadMemberKey
				(
					threadMember.ThreadId,
					threadMember.UserId
				),
				threadMember
			);
		}
	}

	public async ValueTask ListenAsync
	(
		DiscordThreadMemberUpdatedEvent @event
	)
	{
		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetThreadMemberKey
			(
				@event.Data.ThreadId,
				@event.Data.UserId
			),
			@event.Data
		);
	}

	public async ValueTask ListenAsync
	(
		DiscordThreadMembersUpdatedEvent @event
	)
	{
		// only deal with added members here, we deal with removed members later
		if(!@event.Data.AddedMembers.HasValue)
		{
			return;
		}

		foreach(DiscordThreadMember member in @event.Data.AddedMembers.Value)
		{
			await this.cache.CacheObjectAsync
			(
				KeyHelper.GetThreadMemberKey
				(
					@event.Data.ThreadId,
					member.UserId
				),
				member
			);
		}
	}

	public async ValueTask ListenAsync
	(
		DiscordGuildCreatedEvent @event
	)
	{
		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetGuildKey
			(
				@event.Data.Id
			),
			@event.Data
		);
	}

	public async ValueTask ListenAsync
	(
		DiscordGuildUpdatedEvent @event
	)
	{
		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetGuildKey
			(
				@event.Data.Id
			),
			@event.Data
		);
	}

	public async ValueTask ListenAsync
	(
		DiscordGuildEmojisUpdatedEvent @event
	)
	{
		foreach(DiscordEmoji emoji in @event.Data.Emojis)
		{
			await this.cache.CacheObjectAsync
			(
				KeyHelper.GetEmojiKey
				(
					emoji.Id!.Value
				),
				emoji
			);
		}
	}

	public async ValueTask ListenAsync
	(
		DiscordGuildStickersUpdatedEvent @event
	)
	{
		foreach(DiscordSticker sticker in @event.Data.Stickers)
		{
			await this.cache.CacheObjectAsync
			(
				KeyHelper.GetStickerKey
				(
					sticker.Id
				),
				sticker
			);
		}
	}
}

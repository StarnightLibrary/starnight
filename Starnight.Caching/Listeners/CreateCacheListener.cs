namespace Starnight.Caching.Listeners;

using System.Threading.Tasks;

using Starnight.Caching.Services;
using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Channels.Threads;
using Starnight.Internal.Gateway.Events.Inbound.Dispatch;
using Starnight.Internal.Gateway.Listeners;

/// <summary>
/// Represents a listener handling incoming create/update events.
/// </summary>
internal class CreateCacheListener :
	IListener<DiscordChannelCreatedEvent>,
	IListener<DiscordThreadCreatedEvent>,
	IListener<DiscordThreadListSyncEvent>,
	IListener<DiscordGuildCreatedEvent>,
	IListener<DiscordGuildMemberAddedEvent>,
	IListener<DiscordGuildRoleCreatedEvent>,
	IListener<DiscordScheduledEventCreatedEvent>,
	IListener<DiscordMessageCreatedEvent>
{
	private readonly IStarnightCacheService cache;

	public CreateCacheListener
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
		DiscordGuildMemberAddedEvent @event
	)
	{
		if(!@event.Data.User.HasValue)
		{
			return;
		}

		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetGuildMemberKey
			(
				@event.Data.GuildId.Value,
				@event.Data.User.Value.Id
			),
			@event.Data
		);
	}

	public async ValueTask ListenAsync
	(
		DiscordGuildRoleCreatedEvent @event
	)
	{
		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetRoleKey
			(
				@event.Data.Role.Id
			),
			@event.Data.Role
		);
	}

	public async ValueTask ListenAsync
	(
		DiscordScheduledEventCreatedEvent @event
	)
	{
		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetScheduledEventKey
			(
				@event.Data.Id
			),
			@event.Data
		);
	}

	public async ValueTask ListenAsync
	(
		DiscordMessageCreatedEvent @event
	)
	{
		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetMessageKey
			(
				@event.Data.Id
			),
			@event.Data
		);
	}
}

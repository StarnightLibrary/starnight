namespace Starnight.Caching.Listeners;

using System.Threading.Tasks;

using Starnight.Caching.Services;
using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Gateway.Events.Inbound.Dispatch;
using Starnight.Internal.Gateway.Listeners;

internal class DeleteCacheListener :
	IListener<DiscordChannelDeletedEvent>,
	IListener<DiscordThreadDeletedEvent>,
	IListener<DiscordGuildDeletedEvent>,
	IListener<DiscordGuildBanAddedEvent>,
	IListener<DiscordGuildMemberRemovedEvent>,
	IListener<DiscordGuildRoleDeletedEvent>,
	IListener<DiscordScheduledEventDeletedEvent>
{
	private readonly IStarnightCacheService cache;

	public DeleteCacheListener
	(
		IStarnightCacheService cache
	)
		=> this.cache = cache;

	public async ValueTask ListenAsync
	(
		DiscordChannelDeletedEvent @event
	)
	{
		_ = await this.cache.EvictObjectAsync<DiscordChannel>
		(
			KeyHelper.GetChannelKey
			(
				@event.Data.Id
			)
		);
	}

	public async ValueTask ListenAsync
	(
		DiscordThreadDeletedEvent @event
	)
	{
		_ = await this.cache.EvictObjectAsync<DiscordChannel>
		(
			KeyHelper.GetChannelKey
			(
				@event.Data.Id
			)
		);
	}

	public async ValueTask ListenAsync
	(
		DiscordGuildDeletedEvent @event
	)
	{
		_ = await this.cache.EvictObjectAsync<DiscordGuild>
		(
			KeyHelper.GetGuildKey
			(
				@event.Data.Id
			)
		);
	}

	public async ValueTask ListenAsync
	(
		DiscordGuildBanAddedEvent @event
	)
	{
		_ = await this.cache.EvictObjectAsync<DiscordGuildMember>
		(
			KeyHelper.GetGuildMemberKey
			(
				@event.Data.GuildId,
				@event.Data.User.Id
			)
		);
	}

	public async ValueTask ListenAsync
	(
		DiscordGuildMemberRemovedEvent @event
	)
	{
		_ = await this.cache.EvictObjectAsync<DiscordGuildMember>
		(
			KeyHelper.GetGuildMemberKey
			(
				@event.Data.GuildId,
				@event.Data.User.Id
			)
		);
	}

	public async ValueTask ListenAsync
	(
		DiscordGuildRoleDeletedEvent @event
	)
	{
		_ = await this.cache.EvictObjectAsync<DiscordRole>
		(
			KeyHelper.GetRoleKey
			(
				@event.Data.RoleId
			)
		);
	}

	public async ValueTask ListenAsync
	(
		DiscordScheduledEventDeletedEvent @event
	)
	{
		_ = await this.cache.EvictObjectAsync<DiscordScheduledEvent>
		(
			KeyHelper.GetScheduledEventKey
			(
				@event.Data.Id
			)
		);
	}
}

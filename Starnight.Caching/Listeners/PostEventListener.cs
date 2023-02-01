namespace Starnight.Caching.Listeners;

using System.Threading.Tasks;

using Starnight.Caching.Services;
using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Gateway.Events.Inbound.Dispatch;
using Starnight.Internal.Gateway.Listeners;

internal class PostEventListener
	: IListener<DiscordChannelDeletedEvent>
{
	private readonly IStarnightCacheService cache;

	public PostEventListener
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
}

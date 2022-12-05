namespace Starnight.Caching.Responders;

using System.Threading.Tasks;

using Starnight.Caching.Services;
using Starnight.Internal.Gateway.Events.Inbound.Dispatch;
using Starnight.Internal.Gateway.Responders;

/// <summary>
/// Represents a responder handling incoming channel create/update events.
/// </summary>
internal class PreChannelResponder :
	IResponder<DiscordChannelCreatedEvent>,
	IResponder<DiscordChannelUpdatedEvent>,
	IResponder<DiscordThreadCreatedEvent>,
	IResponder<DiscordThreadUpdatedEvent>
{
	private readonly IStarnightCacheService cache;

	public PreChannelResponder
	(
		IStarnightCacheService cache
	)
		=> this.cache = cache;

	public async ValueTask RespondAsync
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

	public async ValueTask RespondAsync
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

	public async ValueTask RespondAsync
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

	public async ValueTask RespondAsync
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
}

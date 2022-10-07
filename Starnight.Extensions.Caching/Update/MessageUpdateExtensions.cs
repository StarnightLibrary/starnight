namespace Starnight.Extensions.Caching.Update;

using System;
using System.Threading.Tasks;

using Starnight.Caching.Abstractions;
using Starnight.Extensions.Caching.Update.Internal;
using Starnight.Internal.Entities.Messages;

internal static class MessageUpdateExtensions
{
	public static async ValueTask<DiscordMessage> CacheMessageAsync
	(
		this ICacheService cache,
		DiscordMessage message
	)
	{
		String key = message.GenerateCacheKey();

		DiscordMessage? old = await cache.GetAsync<DiscordMessage>(key);

		if(old is null)
		{
			await cache.SetAsync(key, message);
		}
		else
		{
			message = UpdateMessageWrapper.UpdateDiscordMessage(old, message);

			await cache.SetAsync(key, message);
		}

		return message;
	}
}

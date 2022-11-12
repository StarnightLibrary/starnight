namespace Starnight.Extensions.Caching.Update;

using System;
using System.Threading.Tasks;

using Starnight.Caching.Abstractions;
using Starnight.Extensions.Caching.Update.Internal;
using Starnight.Internal.Entities.Guilds.Invites;

internal static class InviteUpdateExtensions
{
	public static async ValueTask<DiscordInvite> CacheInviteAsync
	(
		this ICacheService cache,
		DiscordInvite invite
	)
	{
		String key = KeyHelper.GetInviteKey
		(
			invite.Code
		);

		DiscordInvite? old = await cache.GetAsync<DiscordInvite>
		(
			key
		);

		if(old is null)
		{
			await cache.SetAsync
			(
				key,
				invite
			);
		}
		else
		{
			invite = UpdateGuildWrapper.UpdateDiscordInvite
			(
				old,
				invite
			);

			await cache.SetAsync
			(
				key,
				invite
			);
		}

		return invite;
	}
}

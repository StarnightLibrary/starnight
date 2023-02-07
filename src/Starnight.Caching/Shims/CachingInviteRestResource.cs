namespace Starnight.Caching.Shims;

using System;
using System.Threading;
using System.Threading.Tasks;

using Starnight.Caching.Services;
using Starnight.Internal.Entities.Guilds.Invites;
using Starnight.Internal.Rest.Resources;
using Starnight.SourceGenerators.Shims;

[Shim<IDiscordInviteRestResource>]
public partial class CachingInviteRestResource : IDiscordInviteRestResource
{
	private readonly IDiscordInviteRestResource underlying;
	private readonly IStarnightCacheService cache;

	public CachingInviteRestResource
	(
		IDiscordInviteRestResource underlying,
		IStarnightCacheService cache
	)
	{
		this.underlying = underlying;
		this.cache = cache;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordInvite> GetInviteAsync
	(
		String inviteCode,
		Boolean? withCounts = null,
		Boolean? withExpiration = null,
		Int64? scheduledEventId = null,
		CancellationToken ct = default
	)
	{
		DiscordInvite invite = await this.underlying.GetInviteAsync
		(
			inviteCode,
			withCounts,
			withExpiration,
			scheduledEventId,
			ct
		);

		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetInviteKey
			(
				inviteCode
			),
			invite
		);

		return invite;
	}

	// redirects
	public partial ValueTask<DiscordInvite> DeleteInviteAsync(String inviteCode, String? reason = null, CancellationToken ct = default);
}

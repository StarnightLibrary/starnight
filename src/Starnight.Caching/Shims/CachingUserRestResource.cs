namespace Starnight.Caching.Shims;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Starnight.Caching.Services;
using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Entities.Users;
using Starnight.Internal.Rest.Payloads.Users;
using Starnight.Internal.Rest.Resources;
using Starnight.SourceGenerators.Shims;

[Shim<IDiscordUserRestResource>]
public partial class CachingUserRestResource : IDiscordUserRestResource
{
	private readonly IDiscordUserRestResource underlying;
	private readonly IStarnightCacheService cache;

	public CachingUserRestResource
	(
		IDiscordUserRestResource underlying,
		IStarnightCacheService cache
	)
	{
		this.underlying = underlying;
		this.cache = cache;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordUser> GetCurrentUserAsync
	(
		CancellationToken ct = default
	)
	{
		DiscordUser user = await this.underlying.GetCurrentUserAsync
		(
			ct
		);

		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetUserKey
			(
				user.Id
			),
			user
		);

		return user;
	}

	public async ValueTask<IEnumerable<DiscordGuild>> GetCurrentUserGuildsAsync
	(
		Int64? before = null,
		Int64? after = null,
		Int32? limit = null,
		CancellationToken ct = default
	)
	{
		IEnumerable<DiscordGuild> guilds = await this.underlying.GetCurrentUserGuildsAsync
		(
			before,
			after,
			limit,
			ct
		);

		await Parallel.ForEachAsync
		(
			guilds,
			async (guild, _) =>
				await this.cache.CacheObjectAsync
				(
					KeyHelper.GetGuildKey
					(
						guild.Id
					),
					guild
				)
		);

		return guilds;
	}
	public ValueTask<DiscordUser> GetUserAsync(Int64 userId, CancellationToken ct = default) => throw new NotImplementedException();


	// redirects
	public partial ValueTask<DiscordGuildMember> GetCurrentUserGuildMemberAsync(Int64 guildId, CancellationToken ct = default);
	public partial ValueTask<DiscordChannel> CreateDMAsync(Int64 recipientId, CancellationToken ct = default);
	public partial ValueTask<IEnumerable<DiscordUserConnection>> GetUserConnectionsAsync(CancellationToken ct = default);
	public partial ValueTask<Boolean> LeaveGuildAsync(Int64 guildId, CancellationToken ct = default);
	public partial ValueTask<DiscordUser> ModifyCurrentUserAsync(ModifyCurrentUserRequestPayload payload, CancellationToken ct = default);
}

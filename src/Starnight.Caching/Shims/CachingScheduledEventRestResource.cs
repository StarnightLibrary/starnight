namespace Starnight.Caching.Shims;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Starnight.Caching.Services;
using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Rest.Payloads.ScheduledEvents;
using Starnight.Internal.Rest.Resources;
using Starnight.SourceGenerators.Shims;

[Shim<IDiscordScheduledEventRestResource>]
public partial class CachingScheduledEventRestResource : IDiscordScheduledEventRestResource
{
	private readonly IDiscordScheduledEventRestResource underlying;
	private readonly IStarnightCacheService cache;

	public CachingScheduledEventRestResource
	(
		IDiscordScheduledEventRestResource underlying,
		IStarnightCacheService cache
	)
	{
		this.underlying = underlying;
		this.cache = cache;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordScheduledEvent> GetScheduledEventAsync
	(
		Int64 guildId,
		Int64 eventId,
		Boolean? withUserCount = null,
		CancellationToken ct = default
	)
	{
		DiscordScheduledEvent scheduledEvent = await this.underlying.GetScheduledEventAsync
		(
			guildId,
			eventId,
			withUserCount,
			ct
		);

		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetScheduledEventKey
			(
				eventId
			),
			scheduledEvent
		);

		return scheduledEvent;
	}

	public ValueTask<IEnumerable<DiscordScheduledEventUser>> GetScheduledEventUsersAsync(Int64 guildId, Int64 eventId, Int32? limit = null, Boolean? withMemberObject = null, Int64? before = null, Int64? after = null, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<IEnumerable<DiscordScheduledEvent>> ListScheduledEventsAsync(Int64 guildId, Boolean? withUserCount = null, CancellationToken ct = default) => throw new NotImplementedException();


	// redirects
	public partial ValueTask<DiscordScheduledEvent> CreateScheduledEventAsync(Int64 guildId, CreateScheduledEventRequestPayload payload, String? reason = null, CancellationToken ct = default);
	public partial ValueTask<System.Boolean> DeleteScheduledEventAsync(Int64 guildId, Int64 eventId, CancellationToken ct = default);
	public partial ValueTask<DiscordScheduledEvent> ModifyScheduledEventAsync(Int64 guildId, Int64 eventId, ModifyScheduledEventRequestPayload payload, String? reason = null, CancellationToken ct = default);
}

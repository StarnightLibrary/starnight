namespace Starnight.Caching.Shims;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Starnight.Caching.Services;
using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Rest.Payloads.Emojis;
using Starnight.Internal.Rest.Resources;
using Starnight.SourceGenerators.Shims;

/// <summary>
/// Represents a shim over the present emoji rest resource, updating and corroborating from
/// cache where possible.
/// </summary>
[Shim<IDiscordEmojiRestResource>]
public partial class CachingEmojiRestResource : IDiscordEmojiRestResource
{
	private readonly IDiscordEmojiRestResource underlying;
	private readonly IStarnightCacheService cache;

	public CachingEmojiRestResource
	(
		IDiscordEmojiRestResource underlying,
		IStarnightCacheService cache
	)
	{
		this.underlying = underlying;
		this.cache = cache;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordEmoji> GetGuildEmojiAsync
	(
		Int64 guildId,
		Int64 emojiId,
		CancellationToken ct = default
	)
	{
		DiscordEmoji emoji = await this.underlying.GetGuildEmojiAsync
		(
			guildId,
			emojiId,
			ct
		);

		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetEmojiKey
			(
				emojiId
			),
			emoji
		);

		return emoji;
	}

	/// <inheritdoc/>
	public async ValueTask<IEnumerable<DiscordEmoji>> ListGuildEmojisAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	)
	{
		IEnumerable<DiscordEmoji> emojis = await this.underlying.ListGuildEmojisAsync
		(
			guildId,
			ct
		);

		await Parallel.ForEachAsync
		(
			emojis,
			async (xm, _) => await this.cache.CacheObjectAsync
			(
				KeyHelper.GetEmojiKey
				(
					xm.Id!.Value
				),
				xm
			)
		);

		return emojis;
	}

	// redirects
	public partial ValueTask<DiscordEmoji> CreateGuildEmojiAsync(Int64 guildId,CreateGuildEmojiRequestPayload payload,String? reason = null,CancellationToken ct = default);
	public partial ValueTask<Boolean> DeleteGuildEmojiAsync(Int64 guildId,Int64 emojiId,String? reason = null,CancellationToken ct = default);
	public partial ValueTask<DiscordEmoji> ModifyGuildEmojiAsync(Int64 guildId, Int64 emojiId, ModifyGuildEmojiRequestPayload payload, String? reason = null, CancellationToken ct = default);
}

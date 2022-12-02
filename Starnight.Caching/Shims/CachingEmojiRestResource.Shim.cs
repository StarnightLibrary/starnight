namespace Starnight.Caching.Shims;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Starnight.Caching.Services;
using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Rest.Payloads.Emojis;
using Starnight.Internal.Rest.Resources;

/// <summary>
/// Represents a shim over the present emoji rest resource, updating and corroborating from
/// cache where possible.
/// </summary>
public partial class CachingEmojiRestResource : IDiscordEmojiRestResource
{
	private readonly IDiscordEmojiRestResource __underlying;
	private readonly IStarnightCacheService __cache;

	public CachingEmojiRestResource
	(
		IDiscordEmojiRestResource underlying,
		IStarnightCacheService cache
	)
	{
		this.__underlying = underlying;
		this.__cache = cache;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordEmoji> CreateGuildEmojiAsync
	(
		Int64 guildId,
		CreateGuildEmojiRequestPayload payload,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		DiscordEmoji emoji = await this.__underlying.CreateGuildEmojiAsync
		(
			guildId,
			payload,
			reason,
			ct
		);

		await this.__cache.CacheObjectAsync
		(
			KeyHelper.GetEmojiKey
			(
				emoji.Id!.Value
			),
			emoji
		);

		return emoji;
	}
	public ValueTask<Boolean> DeleteGuildEmojiAsync(Int64 guildId, Int64 emojiId, String? reason = null, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<DiscordEmoji> GetGuildEmojiAsync(Int64 guildId, Int64 emojiId, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<IEnumerable<DiscordEmoji>> ListGuildEmojisAsync(Int64 guildId, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<DiscordEmoji> ModifyGuildEmojiAsync(Int64 guildId, Int64 emojiId, ModifyGuildEmojiRequestPayload payload, String? reason = null, CancellationToken ct = default) => throw new NotImplementedException();
}

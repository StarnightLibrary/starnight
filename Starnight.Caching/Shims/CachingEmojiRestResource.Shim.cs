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

	/// <inheritdoc/>
	public async ValueTask<Boolean> DeleteGuildEmojiAsync
	(
		Int64 guildId,
		Int64 emojiId,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		Boolean result = await this.__underlying.DeleteGuildEmojiAsync
		(
			guildId,
			emojiId,
			reason,
			ct
		);

		if(result)
		{
			_ = await this.__cache.EvictObjectAsync<DiscordEmoji>
			(
				KeyHelper.GetEmojiKey
				(
					emojiId
				)
			);
		}

		return result;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordEmoji> GetGuildEmojiAsync
	(
		Int64 guildId,
		Int64 emojiId,
		CancellationToken ct = default
	)
	{
		DiscordEmoji emoji = await this.__underlying.GetGuildEmojiAsync
		(
			guildId,
			emojiId,
			ct
		);

		await this.__cache.CacheObjectAsync
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
		IEnumerable<DiscordEmoji> emojis = await this.__underlying.ListGuildEmojisAsync
		(
			guildId,
			ct
		);

		await Parallel.ForEachAsync
		(
			emojis,
			async (xm, _) => await this.__cache.CacheObjectAsync
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

	/// <inheritdoc/>
	public async ValueTask<DiscordEmoji> ModifyGuildEmojiAsync
	(
		Int64 guildId,
		Int64 emojiId,
		ModifyGuildEmojiRequestPayload payload,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		DiscordEmoji emoji = await this.__underlying.ModifyGuildEmojiAsync
		(
			guildId,
			emojiId,
			payload,
			reason,
			ct
		);

		await this.__cache.CacheObjectAsync
		(
			KeyHelper.GetEmojiKey
			(
				emojiId
			),
			emoji
		);

		return emoji;
	}
}

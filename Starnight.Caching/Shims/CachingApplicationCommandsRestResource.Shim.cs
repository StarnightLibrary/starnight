namespace Starnight.Caching.Shims;

using System;
using System.Threading;
using System.Threading.Tasks;

using Starnight.Caching;
using Starnight.Caching.Services;
using Starnight.Internal.Entities.Messages;
using Starnight.Internal.Rest.Payloads.ApplicationCommands;
using Starnight.Internal.Rest.Resources;

/// <summary>
/// Represents a shim over the present application commands rest resource caching and corroborating from cache return
/// values where applicable.
/// </summary>
public partial class CachingApplicationCommandsRestResource : IDiscordApplicationCommandsRestResource
{
	private readonly IDiscordApplicationCommandsRestResource underlying;
	private readonly IStarnightCacheService cache;

	public CachingApplicationCommandsRestResource
	(
		IDiscordApplicationCommandsRestResource underlying,
		IStarnightCacheService cache
	)
	{
		this.underlying = underlying;
		this.cache = cache;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordMessage> CreateFollowupMessageAsync
	(
		Int64 applicationId,
		String interactionToken,
		CreateFollowupMessageRequestPayload payload,
		CancellationToken ct = default
	)
	{
		DiscordMessage message = await this.underlying.CreateFollowupMessageAsync
		(
			applicationId,
			interactionToken,
			payload,
			ct
		);

		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetMessageKey
			(
				message.Id
			),
			message
		);

		return message;
	}

	/// <inheritdoc/>
	public async ValueTask<Boolean> DeleteFollowupMessageAsync
	(
		Int64 applicationId,
		String interactionToken,
		Int64 messageId,
		CancellationToken ct = default
	)
	{
		_ = await this.cache.EvictObjectAsync<DiscordMessage>
		(
			KeyHelper.GetMessageKey
			(
				messageId
			)
		);

		return await this.underlying.DeleteFollowupMessageAsync
		(
			applicationId,
			interactionToken,
			messageId,
			ct
		);
	}

	/// <inheritdoc/>
	public async ValueTask<Boolean> DeleteOriginalInteractionResponseAsync
	(
		Int64 applicationId,
		String interactionToken,
		CancellationToken ct = default
	)
	{
		Int64? messageId = await this.cache.EvictObjectAsync<Int64?>
		(
			KeyHelper.GetOriginalInteractionResponseKey
			(
				interactionToken
			)
		);

		if(messageId is not null)
		{
			_ = await this.cache.EvictObjectAsync<DiscordMessage>
			(
				KeyHelper.GetMessageKey
				(
					(Int64)messageId
				)
			);
		}

		return await this.underlying.DeleteOriginalInteractionResponseAsync
		(
			applicationId,
			interactionToken,
			ct
		);
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordMessage> EditFollowupMessageAsync
	(
		Int64 applicationId,
		String interactionToken,
		Int64 messageId,
		EditFollowupMessageRequestPayload payload,
		CancellationToken ct = default
	)
	{
		DiscordMessage message = await this.underlying.EditFollowupMessageAsync
		(
			applicationId,
			interactionToken,
			messageId,
			payload,
			ct
		);

		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetMessageKey
			(
				message.Id
			),
			message
		);

		return message;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordMessage> EditOriginalResponseAsync
	(
		Int64 applicationId,
		String interactionToken,
		EditOriginalResponseRequestPayload payload,
		CancellationToken ct = default
	)
	{
		DiscordMessage message = await this.underlying.EditOriginalResponseAsync
		(
			applicationId,
			interactionToken,
			payload,
			ct
		);

		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetOriginalInteractionResponseKey
			(
				interactionToken
			),
			message.Id
		);

		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetMessageKey
			(
				message.Id
			),
			message
		);

		return message;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordMessage> GetFollowupMessageAsync
	(
		Int64 applicationId,
		String interactionToken,
		Int64 messageId,
		CancellationToken ct = default
	)
	{
		DiscordMessage message = await this.underlying.GetFollowupMessageAsync
		(
			applicationId,
			interactionToken,
			messageId,
			ct
		);

		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetMessageKey
			(
				message.Id
			),
			message
		);

		return message;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordMessage> GetOriginalResponseAsync
	(
		Int64 applicationId,
		String interactionToken,
		CancellationToken ct = default
	)
	{
		DiscordMessage message = await this.underlying.GetOriginalResponseAsync
		(
			applicationId,
			interactionToken,
			ct
		);

		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetOriginalInteractionResponseKey
			(
				interactionToken
			),
			message.Id
		);

		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetMessageKey
			(
				message.Id
			),
			message
		);

		return message;
	}
}

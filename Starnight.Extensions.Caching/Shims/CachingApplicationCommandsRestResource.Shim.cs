namespace Starnight.Extensions.Caching.Shims;

using System;
using System.Threading;
using System.Threading.Tasks;

using Starnight.Caching.Abstractions;
using Starnight.Extensions.Caching.Extensions;
using Starnight.Internal.Entities.Messages;
using Starnight.Internal.Rest.Payloads.ApplicationCommands;
using Starnight.Internal.Rest.Resources;

/// <summary>
/// Represents a shim over the present application commands rest resource caching and corroborating from cache return
/// values where applicable.
/// </summary>
public partial class CachingApplicationCommandsRestResource : IDiscordApplicationCommandsRestResource
{
	private readonly IDiscordApplicationCommandsRestResource __underlying;
	private readonly ICacheProvider __cache;

	public CachingApplicationCommandsRestResource
	(
		IDiscordApplicationCommandsRestResource underlying,
		ICacheProvider cache
	)
	{
		this.__underlying = underlying;
		this.__cache = cache;
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
		DiscordMessage message = await this.__underlying.CreateFollowupMessageAsync
		(
			applicationId,
			interactionToken,
			payload,
			ct
		);

		await this.__cache.CacheObjectAsync
		(
			message,
			KeyHelper.GetMessageKey
			(
				message.Id
			)
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
		_ = await this.__cache.RemoveAsync<DiscordMessage>
		(
			KeyHelper.GetMessageKey
			(
				messageId
			)
		);

		return await this.__underlying.DeleteFollowupMessageAsync
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
		Int64? messageId = await this.__cache.RemoveAsync<Int64?>
		(
			KeyHelper.GetOriginalInteractionResponseKey
			(
				interactionToken
			)
		);

		if(messageId is not null)
		{
			_ = await this.__cache.RemoveAsync<DiscordMessage>
			(
				KeyHelper.GetMessageKey
				(
					(Int64)messageId
				)
			);
		}

		return await this.__underlying.DeleteOriginalInteractionResponseAsync
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
		DiscordMessage message = await this.__underlying.EditFollowupMessageAsync
		(
			applicationId,
			interactionToken,
			messageId,
			payload,
			ct
		);

		await this.__cache.CacheObjectAsync
		(
			message,
			KeyHelper.GetMessageKey
			(
				message.Id
			)
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
		DiscordMessage message = await this.__underlying.EditOriginalResponseAsync
		(
			applicationId,
			interactionToken,
			payload,
			ct
		);

		await this.__cache.CacheAsync
		(
			KeyHelper.GetOriginalInteractionResponseKey
			(
				interactionToken
			),
			message.Id
		);

		await this.__cache.CacheObjectAsync
		(
			message,
			KeyHelper.GetMessageKey
			(
				message.Id
			)
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
		DiscordMessage message = await this.__underlying.GetFollowupMessageAsync
		(
			applicationId,
			interactionToken,
			messageId,
			ct
		);

		await this.__cache.CacheObjectAsync
		(
			message,
			KeyHelper.GetMessageKey
			(
				message.Id
			)
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
		DiscordMessage message = await this.__underlying.GetOriginalResponseAsync
		(
			applicationId,
			interactionToken,
			ct
		);

		await this.__cache.CacheAsync
		(
			KeyHelper.GetOriginalInteractionResponseKey
			(
				interactionToken
			),
			message.Id
		);

		await this.__cache.CacheObjectAsync
		(
			message,
			KeyHelper.GetMessageKey
			(
				message.Id
			)
		);

		return message;
	}
}

namespace Starnight.Extensions.Caching.Shims;

using System;
using System.Threading;
using System.Threading.Tasks;

using Starnight.Caching.Abstractions;
using Starnight.Extensions.Caching.Update;
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
	private readonly ICacheService __cache;

	public CachingApplicationCommandsRestResource
	(
		IDiscordApplicationCommandsRestResource underlying,
		ICacheService cache
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

		return await this.__cache.CacheMessageAsync
		(
			message
		);
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
	public ValueTask<DiscordMessage> EditFollowupMessageAsync
	(
		Int64 applicationId,
		String interactionToken,
		Int64 messageId,
		EditFollowupMessageRequestPayload payload,
		CancellationToken ct = default
	)
	{
		throw new NotImplementedException();
	}
	public ValueTask<DiscordMessage> EditOriginalResponseAsync(System.Int64 applicationId, System.String interactionToken, EditOriginalResponseRequestPayload payload, CancellationToken ct = default) => throw new System.NotImplementedException();
	public ValueTask<DiscordMessage> GetFollowupMessageAsync(System.Int64 applicationId, System.String interactionToken, System.Int64 messageId, CancellationToken ct = default) => throw new System.NotImplementedException();
	public ValueTask<DiscordMessage> GetOriginalResponseAsync(System.Int64 applicationId, System.String interactionToken, CancellationToken ct = default) => throw new System.NotImplementedException();
}

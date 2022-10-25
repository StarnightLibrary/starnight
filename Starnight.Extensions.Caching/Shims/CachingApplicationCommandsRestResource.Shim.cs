namespace Starnight.Extensions.Caching.Shims;

using System;
using System.Threading.Tasks;

using Starnight.Caching.Abstractions;
using Starnight.Extensions.Caching.Update;
using Starnight.Internal.Entities.Messages;
using Starnight.Internal.Rest.Payloads.ApplicationCommands;
using Starnight.Internal.Rest.Resources;

/// <summary>
/// Represents a shim over discords application commands rest resource which caches return values.
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
		CreateFollowupMessageRequestPayload payload
	)
	{
		DiscordMessage message = await this.__underlying.CreateFollowupMessageAsync
		(
			applicationId,
			interactionToken,
			payload
		);

		message = await this.__cache.CacheMessageAsync(message);

		return message;
	}

	/// <inheritdoc/>
	public ValueTask<Boolean> DeleteFollowupMessageAsync
	(
		Int64 applicationId,
		String interactionToken,
		Int64 messageId
	)
	{
		return this.__underlying.DeleteFollowupMessageAsync
		(
			applicationId,
			interactionToken,
			messageId
		);
	}

	/// <inheritdoc/>
	public ValueTask<Boolean> DeleteOriginalInteractionResponseAsync
	(
		Int64 applicationId,
		String interactionToken
	)
	{
		return this.__underlying.DeleteOriginalInteractionResponseAsync
		(
			applicationId,
			interactionToken
		);
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordMessage> EditFollowupMessageAsync
	(
		Int64 applicationId,
		Int64 interactionToken,
		Int64 messageId,
		EditFollowupMessageRequestPayload payload
	)
	{
		DiscordMessage editedFollowup = await this.__underlying.EditFollowupMessageAsync
		(
			applicationId,
			interactionToken,
			messageId,
			payload
		);

		editedFollowup = await this.__cache.CacheMessageAsync(editedFollowup);

		return editedFollowup;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordMessage> EditOriginalResponseAsync
	(
		Int64 applicationId,
		Int64 interactionToken,
		EditOriginalResponseRequestPayload payload
	)
	{
		DiscordMessage message = await this.__underlying.EditOriginalResponseAsync
		(
			applicationId,
			interactionToken,
			payload
		);

		message = await this.__cache.CacheMessageAsync(message);

		return message;
	}

	public ValueTask<DiscordMessage> GetFollowupMessageAsync
	(
		Int64 applicationId,
		String interactionToken,
		Int64 messageId
	) => throw new NotImplementedException();

	public ValueTask<DiscordMessage> GetOriginalResponseAsync
	(
		Int64 applicationId,
		String interactionToken
	) => throw new NotImplementedException();
}

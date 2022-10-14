namespace Starnight.Extensions.Caching.Shims;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Starnight.Caching.Abstractions;
using Starnight.Extensions.Caching.Update;
using Starnight.Internal.Entities.Interactions.ApplicationCommands;
using Starnight.Internal.Entities.Messages;
using Starnight.Internal.Rest.Payloads.ApplicationCommands;
using Starnight.Internal.Rest.Resources;

/// <summary>
/// Represents a shim over discords application commands rest resource which caches return values.
/// </summary>
public class CachingApplicationCommandsRestResource : IDiscordApplicationCommandsRestResource
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
	public async ValueTask<IEnumerable<DiscordApplicationCommand>> BulkOverwriteGlobalApplicationCommandsAsync
	(
		Int64 applicationId,
		IEnumerable<CreateApplicationCommandRequestPayload> payload
	)
	{
		IEnumerable<DiscordApplicationCommand> commands = await this.__underlying.BulkOverwriteGlobalApplicationCommandsAsync
		(
			applicationId,
			payload
		);

		Int32 count = 0;

		DiscordApplicationCommand[] corroboratedCommands = new DiscordApplicationCommand[commands.Count()];

		foreach(DiscordApplicationCommand command in commands)
		{
			corroboratedCommands[count++] = await this.__cache.CacheApplicationCommandAsync(command);
		}	

		return corroboratedCommands;
	}

	/// <inheritdoc/>
	public async ValueTask<IEnumerable<DiscordApplicationCommand>> BulkOverwriteGuildApplicationCommandsAsync
	(
		Int64 applicationId,
		Int64 guildId,
		IEnumerable<CreateApplicationCommandRequestPayload> payload
	)
	{
		IEnumerable<DiscordApplicationCommand> commands = await this.__underlying.BulkOverwriteGuildApplicationCommandsAsync
		(
			applicationId,
			guildId,
			payload
		);

		Int32 count = 0;

		DiscordApplicationCommand[] corroboratedCommands = new DiscordApplicationCommand[commands.Count()];

		foreach(DiscordApplicationCommand command in commands)
		{
			corroboratedCommands[count++] = await this.__cache.CacheApplicationCommandAsync(command);
		}

		return corroboratedCommands;
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
	public async ValueTask<DiscordApplicationCommand> CreateGlobalApplicationCommandAsync
	(
		Int64 applicationId,
		CreateApplicationCommandRequestPayload payload
	)
	{
		DiscordApplicationCommand command = await this.__underlying.CreateGlobalApplicationCommandAsync
		(
			applicationId,
			payload
		);

		command = await this.__cache.CacheApplicationCommandAsync(command);

		return command;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordApplicationCommand> CreateGuildApplicationCommandAsync
	(
		Int64 applicationId,
		Int64 guildId,
		CreateApplicationCommandRequestPayload payload
	)
	{
		DiscordApplicationCommand command = await this.__underlying.CreateGuildApplicationCommandAsync
		(
			applicationId,
			guildId,
			payload
		);

		command = await this.__cache.CacheApplicationCommandAsync(command);

		return command;
	}

	/// <inheritdoc/>
	public ValueTask<Boolean> CreateInteractionResponseAsync
	(
		Int64 interactionId,
		String interactionToken,
		CreateInteractionCallbackRequestPayload payload
	)
	{
		return this.__underlying.CreateInteractionResponseAsync
		(
			interactionId,
			interactionToken,
			payload
		);
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
	public ValueTask<Boolean> DeleteGlobalApplicationCommandAsync
	(
		Int64 applicationId,
		Int64 commandId
	)
	{
		return this.__underlying.DeleteGlobalApplicationCommandAsync
		(
			applicationId,
			commandId
		);
	}

	/// <inheritdoc/>
	public ValueTask<Boolean> DeleteGuildApplicationCommandAsync
	(
		Int64 applicationId,
		Int64 guildId,
		Int64 commandId
	)
	{
		return this.__underlying.DeleteGuildApplicationCommandAsync
		(
			applicationId,
			guildId,
			commandId
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
	public async ValueTask<DiscordApplicationCommand> EditGlobalApplicationCommandAsync
	(
		Int64 applicationId,
		Int64 commandId,
		EditApplicationCommandRequestPayload payload
	)
	{
		DiscordApplicationCommand command = await this.__underlying.EditGlobalApplicationCommandAsync
		(
			applicationId,
			commandId,
			payload
		);

		command = await this.__cache.CacheApplicationCommandAsync(command);

		return command;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordApplicationCommand> EditGuildApplicationCommandAsync
	(
		Int64 applicationId,
		Int64 guildId,
		Int64 commandId,
		EditApplicationCommandRequestPayload payload
	)
	{
		DiscordApplicationCommand command = await this.__underlying.EditGuildApplicationCommandAsync
		(
			applicationId,
			guildId,
			commandId,
			payload
		);

		command = await this.__cache.CacheApplicationCommandAsync(command);

		return command;
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

	/// <inheritdoc/>
	public async ValueTask<DiscordApplicationCommandPermissions> GetApplicationCommandPermissionsAsync
	(
		Int64 applicationId,
		Int64 guildId,
		Int64 commandId
	)
	{
		DiscordApplicationCommandPermissions permissions = await this.__underlying.GetApplicationCommandPermissionsAsync
		(
			applicationId,
			guildId,
			commandId
		);

		permissions = await this.__cache.CacheApplicationCommandPermissionsAsync(permissions);

		return permissions;
	}

	public ValueTask<DiscordMessage> GetFollowupMessageAsync(Int64 applicationId, String interactionToken, Int64 messageId) => throw new NotImplementedException();
	public ValueTask<DiscordApplicationCommand> GetGlobalApplicationCommandAsync(Int64 applicationId, Int64 commandId) => throw new NotImplementedException();
	public ValueTask<IEnumerable<DiscordApplicationCommand>> GetGlobalApplicationCommandsAsync(Int64 applicationId, Boolean? withLocalizations, String? locale) => throw new NotImplementedException();
	public ValueTask<DiscordApplicationCommand> GetGuildApplicationCommandAsync(Int64 applicationId, Int64 guildId, Int64 commandId) => throw new NotImplementedException();
	public ValueTask<IEnumerable<DiscordApplicationCommandPermissions>> GetGuildApplicationCommandPermissionsAsync(Int64 applicationId, Int64 guildId) => throw new NotImplementedException();
	public ValueTask<IEnumerable<DiscordApplicationCommand>> GetGuildApplicationCommandsAsync(Int64 applicationId, Int64 guildId, Boolean? withLocalizations) => throw new NotImplementedException();
	public ValueTask<DiscordMessage> GetOriginalResponseAsync(Int64 applicationId, String interactionToken) => throw new NotImplementedException();
}

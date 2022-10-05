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

	public ValueTask<DiscordMessage> CreateFollowupMessageAsync(System.Int64 applicationId, System.String interactionToken, CreateFollowupMessageRequestPayload payload) => throw new System.NotImplementedException();
	public ValueTask<DiscordApplicationCommand> CreateGlobalApplicationCommandAsync(System.Int64 applicationId, CreateApplicationCommandRequestPayload payload) => throw new System.NotImplementedException();
	public ValueTask<DiscordApplicationCommand> CreateGuildApplicationCommandAsync(System.Int64 applicationId, System.Int64 guildId, CreateApplicationCommandRequestPayload payload) => throw new System.NotImplementedException();
	public ValueTask<System.Boolean> CreateInteractionResponseAsync(System.Int64 interactionId, System.String interactionToken, CreateInteractionCallbackRequestPayload payload) => throw new System.NotImplementedException();
	public ValueTask<System.Boolean> DeleteFollowupMessageAsync(System.Int64 applicationId, System.String interactionToken, System.Int64 messageId) => throw new System.NotImplementedException();
	public ValueTask<System.Boolean> DeleteGlobalApplicationCommandAsync(System.Int64 applicationId, System.Int64 commandId) => throw new System.NotImplementedException();
	public ValueTask<System.Boolean> DeleteGuildApplicationCommandAsync(System.Int64 applicationId, System.Int64 guildId, System.Int64 commandId) => throw new System.NotImplementedException();
	public ValueTask<System.Boolean> DeleteOriginalInteractionResponseAsync(System.Int64 applicationId, System.String interactionToken) => throw new System.NotImplementedException();
	public ValueTask<DiscordMessage> EditFollowupMessageAsync(System.Int64 applicationId, System.Int64 interactionToken, System.Int64 messageId, EditFollowupMessageRequestPayload payload) => throw new System.NotImplementedException();
	public ValueTask<DiscordApplicationCommand> EditGlobalApplicationCommandAsync(System.Int64 applicationId, System.Int64 commandId, EditApplicationCommandRequestPayload payload) => throw new System.NotImplementedException();
	public ValueTask<DiscordApplicationCommand> EditGuildApplicationCommandAsync(System.Int64 applicationId, System.Int64 guildId, System.Int64 commandId, EditApplicationCommandRequestPayload payload) => throw new System.NotImplementedException();
	public ValueTask<DiscordMessage> EditOriginalResponseAsync(System.Int64 applicationId, System.Int64 interactionToken, EditOriginalResponseRequestPayload payload) => throw new System.NotImplementedException();
	public ValueTask<DiscordApplicationCommandPermissions> GetApplicationCommandPermissionsAsync(System.Int64 applicationId, System.Int64 guildId, System.Int64 commandId) => throw new System.NotImplementedException();
	public ValueTask<DiscordMessage> GetFollowupMessageAsync(System.Int64 applicationId, System.String interactionToken, System.Int64 messageId) => throw new System.NotImplementedException();
	public ValueTask<DiscordApplicationCommand> GetGlobalApplicationCommandAsync(System.Int64 applicationId, System.Int64 commandId) => throw new System.NotImplementedException();
	public ValueTask<IEnumerable<DiscordApplicationCommand>> GetGlobalApplicationCommandsAsync(System.Int64 applicationId, System.Boolean? withLocalizations, System.String? locale) => throw new System.NotImplementedException();
	public ValueTask<DiscordApplicationCommand> GetGuildApplicationCommandAsync(System.Int64 applicationId, System.Int64 guildId, System.Int64 commandId) => throw new System.NotImplementedException();
	public ValueTask<IEnumerable<DiscordApplicationCommandPermissions>> GetGuildApplicationCommandPermissionsAsync(System.Int64 applicationId, System.Int64 guildId) => throw new System.NotImplementedException();
	public ValueTask<IEnumerable<DiscordApplicationCommand>> GetGuildApplicationCommandsAsync(System.Int64 applicationId, System.Int64 guildId, System.Boolean? withLocalizations) => throw new System.NotImplementedException();
	public ValueTask<DiscordMessage> GetOriginalResponseAsync(System.Int64 applicationId, System.String interactionToken) => throw new System.NotImplementedException();
}

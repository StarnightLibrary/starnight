namespace Starnight.Extensions.Caching.Shims;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Starnight.Internal.Entities.Interactions.ApplicationCommands;
using Starnight.Internal.Rest.Payloads.ApplicationCommands;

public partial class CachingApplicationCommandsRestResource
{
	/// <inheritdoc/> 
	public ValueTask<IEnumerable<DiscordApplicationCommand>> BulkOverwriteGlobalApplicationCommandsAsync
	(
		Int64 applicationId,
		IEnumerable<CreateApplicationCommandRequestPayload> payload
	)
	{
		return this.__underlying.BulkOverwriteGlobalApplicationCommandsAsync
		(
			applicationId,
			payload
		);
	}

	/// <inheritdoc/>
	public ValueTask<IEnumerable<DiscordApplicationCommand>> BulkOverwriteGuildApplicationCommandsAsync
	(
		Int64 applicationId,
		Int64 guildId,
		IEnumerable<CreateApplicationCommandRequestPayload> payload
	)
	{
		return this.__underlying.BulkOverwriteGuildApplicationCommandsAsync
		(
			applicationId,
			guildId,
			payload
		);
	}

	/// <inheritdoc/>
	public ValueTask<DiscordApplicationCommand> CreateGlobalApplicationCommandAsync
	(
		Int64 applicationId,
		CreateApplicationCommandRequestPayload payload
	)
	{
		return this.__underlying.CreateGlobalApplicationCommandAsync
		(
			applicationId,
			payload
		);
	}

	/// <inheritdoc/>
	public ValueTask<DiscordApplicationCommand> CreateGuildApplicationCommandAsync
	(
		Int64 applicationId,
		Int64 guildId,
		CreateApplicationCommandRequestPayload payload
	)
	{
		return this.__underlying.CreateGuildApplicationCommandAsync
		(
			applicationId,
			guildId,
			payload
		);
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
	public ValueTask<DiscordApplicationCommand> EditGlobalApplicationCommandAsync
	(
		Int64 applicationId,
		Int64 commandId,
		EditApplicationCommandRequestPayload payload
	)
	{
		return this.__underlying.EditGlobalApplicationCommandAsync
		(
			applicationId,
			commandId,
			payload
		);
	}

	/// <inheritdoc/>
	public ValueTask<DiscordApplicationCommand> EditGuildApplicationCommandAsync
	(
		Int64 applicationId,
		Int64 guildId,
		Int64 commandId,
		EditApplicationCommandRequestPayload payload
	)
	{
		return this.__underlying.EditGuildApplicationCommandAsync
		(
			applicationId,
			guildId,
			commandId,
			payload
		);
	}

	/// <inheritdoc/>
	public ValueTask<DiscordApplicationCommandPermissions> GetApplicationCommandPermissionsAsync
	(
		Int64 applicationId,
		Int64 guildId,
		Int64 commandId
	)
	{
		return this.__underlying.GetApplicationCommandPermissionsAsync
		(
			applicationId,
			guildId,
			commandId
		);
	}

	///<inheritdoc/>
	public ValueTask<DiscordApplicationCommand> GetGlobalApplicationCommandAsync
	(
		Int64 applicationId,
		Int64 commandId
	)
	{
		return this.__underlying.GetGlobalApplicationCommandAsync
		(
			applicationId,
			commandId
		);
	}

	/// <inheritdoc/>
	public ValueTask<IEnumerable<DiscordApplicationCommand>> GetGlobalApplicationCommandsAsync
	(
		Int64 applicationId,
		Boolean? withLocalizations,
		String? locale
	)
	{
		return this.__underlying.GetGlobalApplicationCommandsAsync
		(
			applicationId,
			withLocalizations,
			locale
		);
	}

	/// <inheritdoc/>
	public ValueTask<DiscordApplicationCommand> GetGuildApplicationCommandAsync
	(
		Int64 applicationId,
		Int64 guildId,
		Int64 commandId
	)
	{
		return this.__underlying.GetGuildApplicationCommandAsync
		(
			applicationId,
			guildId,
			commandId
		);
	}

	/// <inheritdoc/>
	public ValueTask<IEnumerable<DiscordApplicationCommandPermissions>> GetGuildApplicationCommandPermissionsAsync
	(
		Int64 applicationId,
		Int64 guildId
	)
	{
		return this.__underlying.GetGuildApplicationCommandPermissionsAsync
		(
			applicationId,
			guildId
		);
	}

	/// <inheritdoc/>
	public ValueTask<IEnumerable<DiscordApplicationCommand>> GetGuildApplicationCommandsAsync
	(
		Int64 applicationId,
		Int64 guildId,
		Boolean? withLocalizations
	)
	{
		return this.__underlying.GetGuildApplicationCommandsAsync
		(
			applicationId,
			guildId,
			withLocalizations
		);
	}
}

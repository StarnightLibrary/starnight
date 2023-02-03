namespace Starnight.Caching.Shims;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Starnight.Internal.Entities.Interactions.ApplicationCommands;
using Starnight.Internal.Rest.Payloads.ApplicationCommands;

public partial class CachingApplicationCommandsRestResource
{
	/// <inheritdoc/>
	public ValueTask<IEnumerable<DiscordApplicationCommand>> BulkOverwriteGlobalApplicationCommandsAsync
	(
		Int64 applicationId,
		IEnumerable<CreateApplicationCommandRequestPayload> payload,
		CancellationToken ct = default
	)
	{
		return this.underlying.BulkOverwriteGlobalApplicationCommandsAsync
		(
			applicationId,
			payload,
			ct
		);
	}

	/// <inheritdoc/>
	public ValueTask<IEnumerable<DiscordApplicationCommand>> BulkOverwriteGuildApplicationCommandsAsync
	(
		Int64 applicationId,
		Int64 guildId,
		IEnumerable<CreateApplicationCommandRequestPayload> payload,
		CancellationToken ct = default
	)
	{
		return this.underlying.BulkOverwriteGuildApplicationCommandsAsync
		(
			applicationId,
			guildId,
			payload,
			ct
		);
	}

	/// <inheritdoc/>
	public ValueTask<DiscordApplicationCommand> CreateGlobalApplicationCommandAsync
	(
		Int64 applicationId,
		CreateApplicationCommandRequestPayload payload,
		CancellationToken ct = default
	)
	{
		return this.underlying.CreateGlobalApplicationCommandAsync
		(
			applicationId,
			payload,
			ct
		);
	}

	/// <inheritdoc/>
	public ValueTask<DiscordApplicationCommand> CreateGuildApplicationCommandAsync
	(
		Int64 applicationId,
		Int64 guildId,
		CreateApplicationCommandRequestPayload payload,
		CancellationToken ct = default
	)
	{
		return this.underlying.CreateGuildApplicationCommandAsync
		(
			applicationId,
			guildId,
			payload,
			ct
		);
	}

	/// <inheritdoc/>
	public ValueTask<Boolean> CreateInteractionResponseAsync
	(
		Int64 interactionId,
		String interactionToken,
		CreateInteractionCallbackRequestPayload payload,
		CancellationToken ct = default
	)
	{
		return this.underlying.CreateInteractionResponseAsync
		(
			interactionId,
			interactionToken,
			payload,
			ct
		);
	}

	/// <inheritdoc/>
	public ValueTask<Boolean> DeleteGlobalApplicationCommandAsync
	(
		Int64 applicationId,
		Int64 commandId,
		CancellationToken ct = default
	)
	{
		return this.underlying.DeleteGlobalApplicationCommandAsync
		(
			applicationId,
			commandId,
			ct
		);
	}

	/// <inheritdoc/>
	public ValueTask<Boolean> DeleteGuildApplicationCommandAsync
	(
		Int64 applicationId,
		Int64 guildId,
		Int64 commandId,
		CancellationToken ct = default
	)
	{
		return this.underlying.DeleteGuildApplicationCommandAsync
		(
			applicationId,
			guildId,
			commandId,
			ct
		);
	}

	/// <inheritdoc/>
	public ValueTask<DiscordApplicationCommand> EditGlobalApplicationCommandAsync
	(
		Int64 applicationId,
		Int64 commandId,
		EditApplicationCommandRequestPayload payload,
		CancellationToken ct = default
	)
	{
		return this.underlying.EditGlobalApplicationCommandAsync
		(
			applicationId,
			commandId,
			payload,
			ct
		);
	}

	/// <inheritdoc/>
	public ValueTask<DiscordApplicationCommand> EditGuildApplicationCommandAsync
	(
		Int64 applicationId,
		Int64 guildId,
		Int64 commandId,
		EditApplicationCommandRequestPayload payload,
		CancellationToken ct = default
	)
	{
		return this.underlying.EditGuildApplicationCommandAsync
		(
			applicationId,
			guildId,
			commandId,
			payload,
			ct
		);
	}

	/// <inheritdoc/>
	public ValueTask<DiscordApplicationCommandPermissions> GetApplicationCommandPermissionsAsync
	(
		Int64 applicationId,
		Int64 guildId,
		Int64 commandId,
		CancellationToken ct = default
	)
	{
		return this.underlying.GetApplicationCommandPermissionsAsync
		(
			applicationId,
			guildId,
			commandId,
			ct
		);
	}

	/// <inheritdoc/>
	public ValueTask<DiscordApplicationCommand> GetGlobalApplicationCommandAsync
	(
		Int64 applicationId,
		Int64 commandId,
		CancellationToken ct = default
	)
	{
		return this.underlying.GetGlobalApplicationCommandAsync
		(
			applicationId,
			commandId,
			ct
		);
	}

	/// <inheritdoc/>
	public ValueTask<IEnumerable<DiscordApplicationCommand>> GetGlobalApplicationCommandsAsync
	(
		Int64 applicationId,
		Boolean? withLocalizations = null,
		String? locale = null,
		CancellationToken ct = default
	)
	{
		return this.underlying.GetGlobalApplicationCommandsAsync
		(
			applicationId,
			withLocalizations,
			locale,
			ct
		);
	}

	/// <inheritdoc/>
	public ValueTask<DiscordApplicationCommand> GetGuildApplicationCommandAsync
	(
		Int64 applicationId,
		Int64 guildId,
		Int64 commandId,
		CancellationToken ct = default
	)
	{
		return this.underlying.GetGuildApplicationCommandAsync
		(
			applicationId,
			guildId,
			commandId,
			ct
		);
	}

	/// <inheritdoc/>
	public ValueTask<IEnumerable<DiscordApplicationCommandPermissions>> GetGuildApplicationCommandPermissionsAsync
	(
		Int64 applicationId,
		Int64 guildId,
		CancellationToken ct = default
	)
	{
		return this.underlying.GetGuildApplicationCommandPermissionsAsync
		(
			applicationId,
			guildId,
			ct
		);
	}

	/// <inheritdoc/>
	public ValueTask<IEnumerable<DiscordApplicationCommand>> GetGuildApplicationCommandsAsync
	(
		Int64 applicationId,
		Int64 guildId,
		Boolean? withLocalizations = null,
		CancellationToken ct = default
	)
	{
		return this.underlying.GetGuildApplicationCommandsAsync
		(
			applicationId,
			guildId,
			withLocalizations,
			ct
		);
	}
}

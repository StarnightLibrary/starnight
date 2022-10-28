namespace Starnight.Internal.Rest.Resources;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Starnight.Internal.Entities.Interactions.ApplicationCommands;
using Starnight.Internal.Entities.Messages;
using Starnight.Internal.Rest.Payloads.ApplicationCommands;

/// <summary>
/// Represents a request wrapper for all requests to the application commands rest resource
/// </summary>
public interface IDiscordApplicationCommandsRestResource
{
	/// <summary>
	/// Fetches a list of application commands for the given application.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of the given application.</param>
	/// <param name="withLocalizations">Specifies whether the response should include the full localizations
	/// (see also: <seealso cref="DiscordApplicationCommand.NameLocalizations"/> and related fields).</param>
	/// <param name="locale">If <paramref name="withLocalizations"/> is false, specifies a locale to include localizations for</param>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<IEnumerable<DiscordApplicationCommand>> GetGlobalApplicationCommandsAsync
	(
		Int64 applicationId,
		Boolean? withLocalizations,
		String? locale,
		CancellationToken ct = default
	);

	/// <summary>
	/// Creates a new global application command.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of your application.</param>
	/// <param name="payload">Command creation payload.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>The newly created application command.</returns>
	public ValueTask<DiscordApplicationCommand> CreateGlobalApplicationCommandAsync
	(
		Int64 applicationId,
		CreateApplicationCommandRequestPayload payload,
		CancellationToken ct = default
	);

	/// <summary>
	/// Fetches a global command.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of the command's owning application.</param>
	/// <param name="commandId">Snowflake identifier of the command itself.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<DiscordApplicationCommand> GetGlobalApplicationCommandAsync
	(
		Int64 applicationId,
		Int64 commandId,
		CancellationToken ct = default
	);

	/// <summary>
	/// Overwrites a global application command.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of your application.</param>
	/// <param name="commandId">Snowflake identifier of the command you want to overwrite.</param>
	/// <param name="payload">Edit payload.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>The new application command object.</returns>
	public ValueTask<DiscordApplicationCommand> EditGlobalApplicationCommandAsync
	(
		Int64 applicationId,
		Int64 commandId,
		EditApplicationCommandRequestPayload payload,
		CancellationToken ct = default
	);

	/// <summary>
	/// Deletes a global application command.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of your application.</param>
	/// <param name="commandId">Snowflake identifier of the command to be deleted.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>Whether the deletion was successful.</returns>
	public ValueTask<Boolean> DeleteGlobalApplicationCommandAsync
	(
		Int64 applicationId,
		Int64 commandId,
		CancellationToken ct = default
	);

	/// <summary>
	/// Bulk overwrites all global application commands for your application.
	/// </summary>
	/// <remarks>
	/// If this list contains any new commands, they will count towards the daily creation limits.
	/// </remarks>
	/// <param name="applicationId">Snowflake identifier of your application.</param>
	/// <param name="payload">List of create payloads.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>The new loadout of application commands.</returns>
	public ValueTask<IEnumerable<DiscordApplicationCommand>> BulkOverwriteGlobalApplicationCommandsAsync
	(
		Int64 applicationId,
		IEnumerable<CreateApplicationCommandRequestPayload> payload,
		CancellationToken ct = default
	);

	/// <summary>
	/// Fetches the guild-specific application commands for the specified guild.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of the application in question.</param>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="withLocalizations">Whether the returned objects should include localizations.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<IEnumerable<DiscordApplicationCommand>> GetGuildApplicationCommandsAsync
	(
		Int64 applicationId,
		Int64 guildId,
		Boolean? withLocalizations,
		CancellationToken ct = default
	);

	/// <summary>
	/// Creates a new, guild-specific application command.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of your application.</param>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="payload">Creation payload.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>The newly created application command.</returns>
	public ValueTask<DiscordApplicationCommand> CreateGuildApplicationCommandAsync
	(
		Int64 applicationId,
		Int64 guildId,
		CreateApplicationCommandRequestPayload payload,
		CancellationToken ct = default
	);

	/// <summary>
	/// Fetches a guild application command.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of the application in question.</param>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="commandId">Snowflake identifier of the command in question.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<DiscordApplicationCommand> GetGuildApplicationCommandAsync
	(
		Int64 applicationId,
		Int64 guildId,
		Int64 commandId,
		CancellationToken ct = default
	);

	/// <summary>
	/// Edits a guild application command.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of your application.</param>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="commandId">Snowflake identifier of the command in question.</param>
	/// <param name="payload">Edit payload.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>The new application command object.</returns>
	public ValueTask<DiscordApplicationCommand> EditGuildApplicationCommandAsync
	(
		Int64 applicationId,
		Int64 guildId,
		Int64 commandId,
		EditApplicationCommandRequestPayload payload,
		CancellationToken ct = default
	);

	/// <summary>
	/// Deletes a guild application command.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of your application.</param>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="commandId">Snowflake identifier of the command in question.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>Whether the deletion was successful.</returns>
	public ValueTask<Boolean> DeleteGuildApplicationCommandAsync
	(
		Int64 applicationId,
		Int64 guildId,
		Int64 commandId,
		CancellationToken ct = default
	);

	/// <summary>
	/// Bulk-overwrites application commands for this guild.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of your application.</param>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="payload">New commands for this guild.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>The newly created application commands.</returns>
	public ValueTask<IEnumerable<DiscordApplicationCommand>> BulkOverwriteGuildApplicationCommandsAsync
	(
		Int64 applicationId,
		Int64 guildId,
		IEnumerable<CreateApplicationCommandRequestPayload> payload,
		CancellationToken ct = default
	);

	/// <summary>
	/// Fetches command permissions for all commands for this application in the specified guild.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of the application in question.</param>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>An array of permissiono objects, one for each command.</returns>
	public ValueTask<IEnumerable<DiscordApplicationCommandPermissions>> GetGuildApplicationCommandPermissionsAsync
	(
		Int64 applicationId,
		Int64 guildId,
		CancellationToken ct = default
	);

	/// <summary>
	/// Fetches application command permissions for the specified command in the specified guild.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of your application.</param>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="commandId">Snowflake identifier of the command in question.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<DiscordApplicationCommandPermissions> GetApplicationCommandPermissionsAsync
	(
		Int64 applicationId,
		Int64 guildId,
		Int64 commandId,
		CancellationToken ct = default
	);

	/// <summary>
	/// Creates an initial response to the given interaction.
	/// </summary>
	/// <param name="interactionId">Snowflake identifier of the interaction.</param>
	/// <param name="interactionToken">Response token of the interaction.</param>
	/// <param name="payload">Payload data.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>Whether the request succeeded.</returns>
	public ValueTask<Boolean> CreateInteractionResponseAsync
	(
		Int64 interactionId,
		String interactionToken,
		CreateInteractionCallbackRequestPayload payload,
		CancellationToken ct = default
	);

	/// <summary>
	/// Returns the original response to this interaction.
	/// </summary>
	/// <param name="applicationId">Snowflake identifer of your application.</param>
	/// <param name="interactionToken">Interaction token for this interaction.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<DiscordMessage> GetOriginalResponseAsync
	(
		Int64 applicationId,
		String interactionToken,
		CancellationToken ct = default
	);

	/// <summary>
	/// Edits the original interaction response.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of your application.</param>
	/// <param name="interactionToken">Interaction token for this interaction.</param>
	/// <param name="payload">Editing payload.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>The newly edited message.</returns>
	public ValueTask<DiscordMessage> EditOriginalResponseAsync
	(
		Int64 applicationId,
		Int64 interactionToken,
		EditOriginalResponseRequestPayload payload,
		CancellationToken ct = default
	);

	/// <summary>
	/// Deletes the original interaction response.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of your application.</param>
	/// <param name="interactionToken">Interaction token for this interaction.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>Whether the deletion was successful.</returns>
	public ValueTask<Boolean> DeleteOriginalInteractionResponseAsync
	(
		Int64 applicationId,
		String interactionToken,
		CancellationToken ct = default
	);

	/// <summary>
	/// Creates a followup message for an interaction.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of your application.</param>
	/// <param name="interactionToken">Interaction token for this interaction.</param>
	/// <param name="payload">Message creation payload.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>The newly created message.</returns>
	public ValueTask<DiscordMessage> CreateFollowupMessageAsync
	(
		Int64 applicationId,
		String interactionToken,
		CreateFollowupMessageRequestPayload payload,
		CancellationToken ct = default
	);

	/// <summary>
	/// Returns the followup message for this interaction.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of your application.</param>
	/// <param name="interactionToken">Interaction token for this interaction.</param>
	/// <param name="messageId">Snowflake identifier of this message.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<DiscordMessage> GetFollowupMessageAsync
	(
		Int64 applicationId,
		String interactionToken,
		Int64 messageId,
		CancellationToken ct = default
	);

	/// <summary>
	/// Edits the specified followup message.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of your application.</param>
	/// <param name="interactionToken">Interaction token for this interaction.</param>
	/// <param name="messageId">Snowflake identifier of the followup message to be edited.</param>
	/// <param name="payload">Editing payload.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>The newly edited message.</returns>
	public ValueTask<DiscordMessage> EditFollowupMessageAsync
	(
		Int64 applicationId,
		Int64 interactionToken,
		Int64 messageId,
		EditFollowupMessageRequestPayload payload,
		CancellationToken ct = default
	);

	/// <summary>
	/// Deletes the specified followup message for this interaction.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of your application.</param>
	/// <param name="interactionToken">Interaction token for this interaction.</param>
	/// <param name="messageId">Snowflake identifier of this message.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<Boolean> DeleteFollowupMessageAsync
	(
		Int64 applicationId,
		String interactionToken,
		Int64 messageId,
		CancellationToken ct = default
	);
}

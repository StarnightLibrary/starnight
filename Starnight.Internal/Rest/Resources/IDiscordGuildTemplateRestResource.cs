namespace Starnight.Internal.Rest.Resources;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Rest.Payloads.GuildTemplates;

/// <summary>
/// Represents a wrapper for all requests to discord's guild template rest resource.
/// </summary>
public interface IDiscordGuildTemplateRestResource
{
	/// <summary>
	/// Fetches the guild template object corresponding to the given template code.
	/// </summary>
	public ValueTask<DiscordGuildTemplate> GetGuildTemplateAsync
	(
		String templateCode
	);

	/// <summary>
	/// Creates a new guild from the given guild template.
	/// </summary>
	/// <remarks>
	/// This endpoint can only be used by bots in less than 10 guilds.
	/// </remarks>
	/// <param name="templateCode">Template code to create the guild from.</param>
	/// <param name="payload">Request payload.</param>
	/// <returns>The newly created guild.</returns>
	public ValueTask<DiscordGuild> CreateGuildFromTemplateAsync
	(
		String templateCode,
		CreateGuildFromTemplateRequestPayload payload
	);

	/// <summary>
	/// Returns all guild templates associated with this guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	public ValueTask<IEnumerable<DiscordGuildTemplate>> GetGuildTemplatesAsync
	(
		Int64 guildId
	);

	/// <summary>
	/// Creates a new guild template from the given guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="payload">Request payload for this request.</param>
	/// <returns>The newly created guild template.</returns>
	public ValueTask<DiscordGuildTemplate> CreateGuildTemplateAsync
	(
		Int64 guildId,
		CreateGuildTemplateRequestPayload payload
	);

	/// <summary>
	/// Syncs the given template to the given guild's current state.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="templateCode">Snowflake identifier of the template in question.</param>
	/// <returns>The newly modified guild template.</returns>
	public ValueTask<DiscordGuildTemplate> SyncGuildTemplateAsync
	(
		Int64 guildId,
		String templateCode
	);

	/// <summary>
	/// Modifies the given guild template.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="templateCode">Template code of the template in question.</param>
	/// <param name="payload">Request payload for this request.</param>
	/// <returns>The newly modified guild template.</returns>
	public ValueTask<DiscordGuildTemplate> ModifyGuildTemplateAsync
	(
		Int64 guildId,
		String templateCode,
		ModifyGuildTemplateRequestPayload payload
	);

	/// <summary>
	/// Deletes the given guild template.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="templateCode">Code of the template to be deleted.</param>
	/// <returns>The deleted guild template.</returns>
	public ValueTask<DiscordGuildTemplate> DeleteGuildTemplateAsync
	(
		Int64 guildId,
		String templateCode
	);
}

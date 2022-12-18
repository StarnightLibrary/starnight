namespace Starnight.Internal.Rest.Resources;

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Entities.Guilds.Invites;
using Starnight.Internal.Entities.Voice;
using Starnight.Internal.Rest.Payloads.Guilds;

/// <summary>
/// Represents a wrapper for all requests to discord's guild rest resource.
/// </summary>
public interface IDiscordGuildRestResource
{
	/// <summary>
	/// Requests a guild from the discord API.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="withCounts">Whether or not the response should include total and online member counts.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>The guild requested.</returns>
	public ValueTask<DiscordGuild> GetGuildAsync
	(
		Int64 guildId,
		Boolean? withCounts = null,
		CancellationToken ct = default
	);

	/// <summary>
	/// Requests a guild preview from the discord API.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>The guild requested.</returns>
	public ValueTask<DiscordGuildPreview> GetGuildPreviewAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	);

	/// <summary>
	/// Modifies a guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="payload">Change payload for the guild.</param>
	/// <param name="reason">Optional audit log reason for the changes.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>The updated guild.</returns>
	public ValueTask<DiscordGuild> ModifyGuildAsync
	(
		Int64 guildId,
		ModifyGuildRequestPayload payload,
		String? reason = null,
		CancellationToken ct = default
	);

	/// <summary>
	/// Permanently deletes a guild. This user must own the guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>Whether or not the request succeeded.</returns>
	public ValueTask<Boolean> DeleteGuildAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	);

	/// <summary>
	/// Requests all active channels for this guild from the API. This excludes thread channels.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<IEnumerable<DiscordChannel>> GetGuildChannelsAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	);

	/// <summary>
	/// Creates a discord channel.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the parent guild.</param>
	/// <param name="payload">Channel creation payload, containing all initializing data.</param>
	/// <param name="reason">Audit log reason for this operation.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>The created channel.</returns>
	public ValueTask<DiscordChannel> CreateGuildChannelAsync
	(
		Int64 guildId,
		CreateGuildChannelRequestPayload payload,
		String? reason = null,
		CancellationToken ct = default
	);

	/// <summary>
	/// Moves channels in a guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the parent guild.</param>
	/// <param name="payload">Array of new channel data payloads, containing IDs and some optional data.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>Whether or not the call succeeded</returns>
	public ValueTask<Boolean> ModifyGuildChannelPositionsAsync
	(
		Int64 guildId,
		IEnumerable<ModifyGuildChannelPositionRequestPayload> payload,
		CancellationToken ct = default
	);

	/// <summary>
	/// Queries all active thread channels in the given guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the queried guild.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>
	/// A response payload object containing an array of thread channels and an array of thread member information
	/// for all threads the current user has joined.
	/// </returns>
	public ValueTask<ListActiveThreadsResponsePayload> ListActiveThreadsAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	);

	/// <summary>
	/// Returns the given users associated guild member object.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the queried guild.</param>
	/// <param name="userId">Snowflake identifier of the user in question.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>A <see cref="DiscordGuildMember"/> object for this user, if available.</returns>
	public ValueTask<DiscordGuildMember> GetGuildMemberAsync
	(
		Int64 guildId,
		Int64 userId,
		CancellationToken ct = default
	);

	/// <summary>
	/// Returns a list of guild member objects.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild to be queried.</param>
	/// <param name="limit">Amount of users to query, between 1 and 1000</param>
	/// <param name="afterUserId">Highest user ID to <b>not</b> query. Used for request pagination.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>A list of <see cref="DiscordGuildMember"/>s of the specified length.</returns>
	public ValueTask<IEnumerable<DiscordGuildMember>> ListGuildMembersAsync
	(
		Int64 guildId,
		Int32? limit = null,
		Int64? afterUserId = null,
		CancellationToken ct = default
	);

	/// <summary>
	/// Returns a list of guild member objects whose username or nickname starts with the given string.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the string in question.</param>
	/// <param name="query">Query string to search for.</param>
	/// <param name="limit">Maximum amount of members to return; 1 - 1000.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<IEnumerable<DiscordGuildMember>> SearchGuildMembersAsync
	(
		Int64 guildId,
		String query,
		Int32? limit = null,
		CancellationToken ct = default
	);

	/// <summary>
	/// Adds a discord user to the given guild, using the OAuth2 flow.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="userId">User ID of the guild in question.</param>
	/// <param name="payload">OAuth2 payload, containing the OAuth2 token and initial information for the user.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>The newly created guild member, or null if the member had already joined the guild.</returns>
	public ValueTask<DiscordGuildMember?> AddGuildMemberAsync
	(
		Int64 guildId,
		Int64 userId,
		AddGuildMemberRequestPayload payload,
		CancellationToken ct = default
	);

	/// <summary>
	/// Modifies a given user in the given guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="userId">Snowflake identifier of the user in question.</param>
	/// <param name="payload">Edit payload. Refer to the Discord documentation for required permissions.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>The modified guild member.</returns>
	public ValueTask<DiscordGuildMember> ModifyGuildMemberAsync
	(
		Int64 guildId,
		Int64 userId,
		ModifyGuildMemberRequestPayload payload,
		String? reason = null,
		CancellationToken ct = default
	);

	/// <summary>
	/// Sets the current user's nickname in the given guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="nickname">New nickname for the current user.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>The new current user event.</returns>
	public ValueTask<DiscordGuildMember> ModifyCurrentMemberAsync
	(
		Int64 guildId,
		String nickname,
		String? reason = null,
		CancellationToken ct = default
	);

	/// <summary>
	/// Adds a role to a guild member in a given guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="userId">Snowflake identifier of the user in question.</param>
	/// <param name="roleId">Snowflake identifier of the role in question.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>Whether the action was successful.</returns>
	public ValueTask<Boolean> AddGuildMemberRoleAsync
	(
		Int64 guildId,
		Int64 userId,
		Int64 roleId,
		String? reason = null,
		CancellationToken ct = default
	);

	/// <summary>
	/// Removes the given role from the given member in the given guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="userId">Snowflake identifier of the user in question.</param>
	/// <param name="roleId">Snowflake identifier of the role in question.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>Whether the action was successful.</returns>
	public ValueTask<Boolean> RemoveGuildMemberRoleAsync
	(
		Int64 guildId,
		Int64 userId,
		Int64 roleId,
		String? reason = null,
		CancellationToken ct = default
	);

	/// <summary>
	/// Kicks the given user from the given guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="userId">Snowflake identifier of the user in question.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>Returns whether the kick was successful.</returns>
	public ValueTask<Boolean> RemoveGuildMemberAsync
	(
		Int64 guildId,
		Int64 userId,
		String? reason = null,
		CancellationToken ct = default
	);

	/// <summary>
	/// Returns a list of bans from the given guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>An array of <see cref="DiscordGuildBan"/> objects, representing all bans in the guild.</returns>
	public ValueTask<IEnumerable<DiscordGuildBan>> GetGuildBansAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	);

	/// <summary>
	/// Returns the ban object for the given user.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="userId">Snowflake identifier of the user in question.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<DiscordGuildBan> GetGuildBanAsync
	(
		Int64 guildId,
		Int64 userId,
		CancellationToken ct = default
	);

	/// <summary>
	/// Bans the given user from the given guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="userId">Snowflake identifier of the user in question.</param>
	/// <param name="deleteMessageDays">
	/// Specifies how many days of message history from this user shall be purged.
	/// </param>
	/// <param name="reason">Specifies an audit log reason for the ban.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask BanMemberAsync
	(
		Int64 guildId,
		Int64 userId,
		Int32 deleteMessageDays,
		String? reason = null,
		CancellationToken ct = default
	);

	/// <summary>
	/// Removes a ban from the given guild for the given user.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="userId">Snowflake identifier of the user in question.</param>
	/// <param name="reason">Optional audit log reason for the ban.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>Whether the unban was successful.</returns>
	public ValueTask<Boolean> UnbanMemberAsync
	(
		Int64 guildId,
		Int64 userId,
		String? reason = null,
		CancellationToken ct = default
	);

	/// <summary>
	/// Fetches a list of all guild roles from the API.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<IEnumerable<DiscordRole>> GetRolesAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	);

	/// <summary>
	/// Creates a role in a given guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="payload">Role information to initialize the role with.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>The newly created role object.</returns>
	public ValueTask<DiscordRole> CreateRoleAsync
	(
		Int64 guildId,
		CreateGuildRoleRequestPayload payload,
		String? reason = null,
		CancellationToken ct = default
	);

	/// <summary>
	/// Modifies the positions of roles in the role list.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="payload">Array of id/new position objects.</param>
	/// <param name="reason">Optional audit log reason for this action.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>The newly ordered list of roles for this guild.</returns>
	public ValueTask<IEnumerable<DiscordRole>> ModifyRolePositionsAsync
	(
		Int64 guildId,
		IEnumerable<ModifyRolePositionRequestPayload> payload,
		String? reason = null,
		CancellationToken ct = default
	);

	/// <summary>
	/// Modifies the settings of a specific role.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild the role belongs to.</param>
	/// <param name="roleId">Snowflake identifier of the role in question.</param>
	/// <param name="payload">New role settings for this role.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>The modified role object.</returns>
	public ValueTask<DiscordRole> ModifyRoleAsync
	(
		Int64 guildId,
		Int64 roleId,
		ModifyGuildRoleRequestPayload payload,
		String? reason = null,
		CancellationToken ct = default
	);

	/// <summary>
	/// Modifies a guild's MFA level.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="level">The new MFA level for this guild.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>The updated MFA level.</returns>
	public ValueTask<DiscordGuildMultiFactorAuthLevel> ModifyGuildMFALevelAsync
	(
		Int64 guildId,
		DiscordGuildMultiFactorAuthLevel level,
		String? reason = null,
		CancellationToken ct = default
	);

	/// <summary>
	/// Deletes a role from a guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild the role belongs to.</param>
	/// <param name="roleId">Snowflake identifier of the role in question.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>Whether the deletion was successful.</returns>
	public ValueTask<Boolean> DeleteRoleAsync
	(
		Int64 guildId,
		Int64 roleId,
		String? reason,
		CancellationToken ct = default
	);

	/// <summary>
	/// Queries how many users would be kicked from a given guild in a prune.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="days">Amount of inactivity days to be measured, 0 to 30.</param>
	/// <param name="roles">Comma-separated list of role IDs to include in the prune.
	///		<para>
	///		Any user with a subset of these roles will be considered for the prune. Any user with any role not listed here
	///		will not be included in the count.
	///		</para>
	/// </param>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<Int32> GetGuildPruneCountAsync
	(
		Int64 guildId,
		Int32? days = null,
		String? roles = null,
		CancellationToken ct = default
	);

	/// <summary>
	/// Initiates a prune from the guild in question.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="days">Amount of inactivity days to be measured, 0 to 30</param>
	/// <param name="roles">Comma-separated list of role IDs to include in the prune
	///		<para>
	///		Any user with a subset of these roles will be considered for the prune. Any user with any role not listed here
	///		will not be included in the count.
	///		</para>
	/// </param>
	/// <param name="computeCount">Whether or not the amount of users pruned should be calculated.</param>
	/// <param name="reason">Optional audit log reason for the prune.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>The amount of users pruned.</returns>
	public ValueTask<Int32?> BeginGuildPruneAsync
	(
		Int64 guildId,
		Int32? days = null,
		String? roles = null,
		Boolean? computeCount = null,
		String? reason = null,
		CancellationToken ct = default
	);

	/// <summary>
	/// Queries all available voice regions for this guild, including VIP regions.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<IEnumerable<DiscordVoiceRegion>> GetGuildVoiceRegionsAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	);

	/// <summary>
	/// Returns a list of all active invites for this guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<IEnumerable<DiscordInvite>> GetGuildInvitesAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	);

	/// <summary>
	/// Returns a list of all active integrations for this guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<IEnumerable<DiscordGuildIntegration>> GetGuildIntegrationsAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	);

	/// <summary>
	/// Deletes an integration from the given guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="integrationId">Snowflake identifier of the integration to be deleted.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns><see langword="true"/> if the deletion succeeded, <see langword="false"/> if otherwise.</returns>
	public ValueTask<Boolean> DeleteGuildIntegrationAsync
	(
		Int64 guildId,
		Int64 integrationId,
		String? reason = null,
		CancellationToken ct = default
	);

	/// <summary>
	/// Queries the guild widget settings for the specified guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild to be queried.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<DiscordGuildWidgetSettings> GetGuildWidgetSettingsAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	);

	/// <summary>
	/// Modifies the <see cref="DiscordGuildWidget"/> for the specified guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="settings">New settings for this guild widget.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>The new guild widget object.</returns>
	public ValueTask<DiscordGuildWidget> ModifyGuildWidgetSettingsAsync
	(
		Int64 guildId,
		DiscordGuildWidgetSettings settings,
		String? reason = null,
		CancellationToken ct = default
	);

	/// <summary>
	/// Returns the guild widget for the specified guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier for the guild in question.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<DiscordGuildWidget> GetGuildWidgetAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	);

	/// <summary>
	/// Queries the vanity invite URL for this guild, if available.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<DiscordInvite> GetGuildVanityInviteAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	);

	/// <summary>
	/// Returns the guild widget image as a binary stream
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="style">Widget style, either "shield" (default) or "banner1" to "banner4".</param>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<Stream> GetGuildWidgetImageAsync
	(
		Int64 guildId,
		String? style = null,
		CancellationToken ct = default
	);

	/// <summary>
	/// Modifies the current user's stage voice state.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild everything takes place in.</param>
	/// <param name="payload">Stage voice state request payload.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>Whether the request succeeded.</returns>
	public ValueTask<Boolean> ModifyCurrentUserVoiceStateAsync
	(
		Int64 guildId,
		ModifyCurrentUserVoiceStateRequestPayload payload,
		CancellationToken ct = default
	);

	/// <summary>
	/// Modifies another user's stage voice state.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild everything takes place in.</param>
	/// <param name="userId">Snowflake identifier of the user whose voice state to modify.</param>
	/// <param name="payload">Stage voice state request payload.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask ModifyUserVoiceStateAsync
	(
		Int64 guildId,
		Int64 userId,
		ModifyUserVoiceStateRequestPayload payload,
		CancellationToken ct = default
	);
}

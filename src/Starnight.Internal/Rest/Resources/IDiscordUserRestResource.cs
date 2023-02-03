namespace Starnight.Internal.Rest.Resources;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Entities.Users;
using Starnight.Internal.Rest.Payloads.Users;

/// <summary>
/// Represents a wrapper for all requests to discord's user rest resource.
/// </summary>
public interface IDiscordUserRestResource
{
	/// <summary>
	/// Returns the current user.
	/// </summary>
	/// <remarks>
	/// For OAuth2, this requires the <c>identify</c> scope, which will return the object without an email,
	/// and optionally the <c>email</c> scope, which will return the object with an email.
	/// </remarks>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<DiscordUser> GetCurrentUserAsync
	(
		CancellationToken ct = default
	);

	/// <summary>
	/// Returns the requested user.
	/// </summary>
	/// <param name="userId">Snowflake identifier of the user in question.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<DiscordUser> GetUserAsync
	(
		Int64 userId,
		CancellationToken ct = default
	);

	/// <summary>
	/// Modifies the current user.
	/// </summary>
	/// <param name="payload">Payload to modify the current user by.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>The newly modified current user.</returns>
	public ValueTask<DiscordUser> ModifyCurrentUserAsync
	(
		ModifyCurrentUserRequestPayload payload,
		CancellationToken ct = default
	);

	/// <summary>
	/// Returns a list of partial guild objects representing the guilds the current user has joined.
	/// </summary>
	/// <remarks>
	/// <paramref name="limit"/> defaults to 200 guilds, which is the maximum number of guilds an user account can join.
	/// Pagination is therefore not needed for obtaining user guilds, but may be needed for obtaining bot guilds.
	/// </remarks>
	/// <param name="before">Specifies an upper bound of snowflakes to be returned.</param>
	/// <param name="after">Specifies a lower bound of snowflakes to be returned.</param>
	/// <param name="limit">Maximum number of guilds to return, ranging from 1 to 200.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<IEnumerable<DiscordGuild>> GetCurrentUserGuildsAsync
	(
		Int64? before = null,
		Int64? after = null,
		Int32? limit = null,
		CancellationToken ct = default
	);

	/// <summary>
	/// Returns a guild member object for the current user for the given guild.
	/// </summary>
	/// <param name="guildId">The snowflake ID of the guild in question.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<DiscordGuildMember> GetCurrentUserGuildMemberAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	);

	/// <summary>
	/// Leaves a guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild to be left.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>Whether the operation was successful.</returns>
	public ValueTask<Boolean> LeaveGuildAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	);

	/// <summary>
	/// Creates a new DM channel with a user.
	/// </summary>
	/// <param name="recipientId">Snowflake identifier of the user to create a DM channel with.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>The newly created channel object.</returns>
	public ValueTask<DiscordChannel> CreateDMAsync
	(
		Int64 recipientId,
		CancellationToken ct = default
	);

	/// <summary>
	/// Returns a list of connection objects for the current user.
	/// </summary>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<IEnumerable<DiscordUserConnection>> GetUserConnectionsAsync
	(
		CancellationToken ct = default
	);
}

namespace Starnight.Internal.Rest.Resources;

using System;
using System.Threading.Tasks;

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
	public ValueTask<DiscordUser> GetCurrentUserAsync();

	/// <summary>
	/// Returns the requested user.
	/// </summary>
	/// <param name="userId">Snowflake identifier of the user in question.</param>
	public ValueTask<DiscordUser> GetUserAsync
	(
		Int64 userId
	);

	/// <summary>
	/// Modifies the current user.
	/// </summary>
	/// <param name="payload">Payload to modify the current user by.</param>
	/// <returns>The newlly modified current user.</returns>
	public ValueTask<DiscordUser> ModifyCurrentUserAsync
	(
		ModifyCurrentUserRequestPayload payload
	);
}

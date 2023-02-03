namespace Starnight.Internal.Rest.Resources;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Starnight.Internal.Entities.Guilds.RoleConnections;

/// <summary>
/// Represents a wrapper for all requests to Discord's application role connections rest resource.
/// </summary>
public interface IDiscordRoleConnectionRestResource
{
	/// <summary>
	/// Returns a list of role connection metadata objects for the given application.
	/// </summary>
	public ValueTask<IEnumerable<DiscordRoleConnectionMetadata>> GetApplicationRoleConnectionMetadata
	(
		Int64 applicationId,
		CancellationToken ct = default
	);

	/// <summary>
	/// Updates the role connection metadata objects for the given application.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of the application in question.</param>
	/// <param name="payload">Up to 5 role connection metadata objects to update to.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>The updated role connection metadata objects.</returns>
	public ValueTask<IEnumerable<DiscordRoleConnectionMetadata>> UpdateApplicationRoleConnectionMetadata
	(
		Int64 applicationId,
		IEnumerable<DiscordRoleConnectionMetadata> payload,
		CancellationToken ct = default
	);
}

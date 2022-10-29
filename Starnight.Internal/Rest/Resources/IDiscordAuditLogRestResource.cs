namespace Starnight.Internal.Rest.Resources;

using System;
using System.Threading;
using System.Threading.Tasks;

using Starnight.Internal.Entities.Guilds.Audit;

/// <summary>
/// Represents a request wrapper for all requests to discord's audit log rest resource.
/// </summary>
public interface IDiscordAuditLogRestResource
{
	/// <summary>
	/// Fetches the audit logs for this guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="userId">User ID to obtain entries from.</param>
	/// <param name="actionType">Action type to obtain entries for.</param>
	/// <param name="before">Snowflake identifier all returned entries will precede.</param>
	/// <param name="limit">Maximum number of entries to return, defaults to 50.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<DiscordAuditLogObject> GetGuildAuditLogAsync
	(
		Int64 guildId,
		Int64? userId,
		DiscordAuditLogEvent? actionType,
		Int64? before,
		Int32? limit,
		CancellationToken ct  
	);
}

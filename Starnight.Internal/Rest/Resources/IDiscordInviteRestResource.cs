namespace Starnight.Internal.Rest.Resources;

using System;
using System.Threading;
using System.Threading.Tasks;

using Starnight.Internal.Entities.Guilds.Invites;

/// <summary>
/// Represents a wrapper for all requests to discord's invite rest resource.
/// </summary>
public interface IDiscordInviteRestResource
{
	/// <summary>
	/// Returns the queried invite.
	/// </summary>
	/// <param name="inviteCode">Invite code identifying this invite.</param>
	/// <param name="withCounts">Whether the invite should contain approximate member counts.</param>
	/// <param name="withExpiration">Whether the invite should contain the expiration date</param>
	/// <param name="scheduledEventId">The scheduled event to include with the invite.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<DiscordInvite> GetInviteAsync
	(
		String inviteCode,
		Boolean? withCounts,
		Boolean? withExpiration,
		Int64? scheduledEventId,
		CancellationToken ct = default
	);

	/// <summary>
	/// Deletes the given invite.
	/// </summary>
	/// <param name="inviteCode">The code identifying the invite.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>The deleted invite object.</returns>
	public ValueTask<DiscordInvite> DeleteInviteAsync
	(
		String inviteCode,
		String? reason,
		CancellationToken ct = default
	);
}

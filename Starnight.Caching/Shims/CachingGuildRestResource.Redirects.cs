namespace Starnight.Caching.Shims;

using System;
using System.Threading;
using System.Threading.Tasks;

public partial class CachingGuildRestResource
{
	/// <inheritdoc/>
	public ValueTask<Boolean> AddGuildMemberRoleAsync
	(
		Int64 guildId,
		Int64 userId,
		Int64 roleId,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		return this.underlying.AddGuildMemberRoleAsync
		(
			guildId,
			userId,
			roleId,
			reason,
			ct
		);
	}
}

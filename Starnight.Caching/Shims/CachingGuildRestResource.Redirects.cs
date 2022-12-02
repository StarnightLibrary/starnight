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

	/// <inheritdoc/>
	public ValueTask BanMemberAsync
	(
		Int64 guildId,
		Int64 userId,
		Int32 deleteMessageDays,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		return this.underlying.BanMemberAsync
		(
			guildId,
			userId,
			deleteMessageDays,
			reason,
			ct
		);
	}

	/// <inheritdoc/>
	public ValueTask<Int32?> BeginGuildPruneAsync
	(
		Int64 guildId,
		Int32? days = null,
		String? roles = null,
		Boolean? computeCount = null,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		return this.underlying.BeginGuildPruneAsync
		(
			guildId,
			days,
			roles,
			computeCount,
			reason,
			ct
		);
	}
}

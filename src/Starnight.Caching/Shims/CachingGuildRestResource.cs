namespace Starnight.Caching.Shims;

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Starnight.Caching.Services;
using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Entities.Guilds.Invites;
using Starnight.Internal.Entities.Users;
using Starnight.Internal.Entities.Voice;
using Starnight.Internal.Rest.Payloads.Guilds;
using Starnight.Internal.Rest.Resources;
using Starnight.SourceGenerators.Shims;

/// <summary>
/// Represents a shim over the present guild rest resource, updating and corroborating from
/// cache where possible.
/// </summary>
[Shim<IDiscordGuildRestResource>]
public partial class CachingGuildRestResource : IDiscordGuildRestResource
{
	private readonly IDiscordGuildRestResource underlying;
	private readonly IStarnightCacheService cache;

	public CachingGuildRestResource
	(
		IDiscordGuildRestResource underlying,
		IStarnightCacheService cache
	)
	{
		this.underlying = underlying;
		this.cache = cache;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordGuild> GetGuildAsync
	(
		Int64 guildId,
		Boolean? withCounts = null,
		CancellationToken ct = default
	)
	{
		DiscordGuild guild = await this.underlying.GetGuildAsync
		(
			guildId,
			withCounts,
			ct
		);

		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetGuildKey
			(
				guildId
			),
			guild
		);

		return guild;
	}

	/// <inheritdoc/>
	public async ValueTask<IEnumerable<DiscordChannel>> GetGuildChannelsAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	)
	{
		IEnumerable<DiscordChannel> channels = await this.underlying.GetGuildChannelsAsync
		(
			guildId,
			ct
		);

		await Parallel.ForEachAsync
		(
			channels,
			async (channel, _) =>
				await this.cache.CacheObjectAsync
				(
					KeyHelper.GetChannelKey
					(
						channel.Id
					),
					channel
				)
		);

		return channels;
	}

	/// <inheritdoc/>
	public async ValueTask<IEnumerable<DiscordInvite>> GetGuildInvitesAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	)
	{
		IEnumerable<DiscordInvite> invites = await this.underlying.GetGuildInvitesAsync
		(
			guildId,
			ct
		);

		await Parallel.ForEachAsync
		(
			invites,
			async (invite, _) =>
				await this.cache.CacheObjectAsync
				(
					KeyHelper.GetInviteKey
					(
						invite.Code
					),
					invite
				)
		);

		return invites;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordGuildMember> GetGuildMemberAsync
	(
		Int64 guildId,
		Int64 userId,
		CancellationToken ct = default
	)
	{
		DiscordGuildMember member = await this.underlying.GetGuildMemberAsync
		(
			guildId,
			userId,
			ct
		);

		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetGuildMemberKey
			(
				guildId,
				userId
			),
			member
		);

		return member;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordGuildPreview> GetGuildPreviewAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	)
	{
		DiscordGuildPreview preview = await this.underlying.GetGuildPreviewAsync
		(
			guildId,
			ct
		);

		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetGuildPreviewKey
			(
				preview.Id
			),
			preview
		);

		return preview;
	}

	/// <inheritdoc/>
	public async ValueTask<IEnumerable<DiscordRole>> GetRolesAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	)
	{
		IEnumerable<DiscordRole> roles = await this.underlying.GetRolesAsync
		(
			guildId,
			ct
		);

		await Parallel.ForEachAsync
		(
			roles,
			async (role, _) =>
				await this.cache.CacheObjectAsync
				(
					KeyHelper.GetRoleKey
					(
						role.Id
					),
					role
				)
		);

		return roles;
	}

	/// <inheritdoc/>
	public async ValueTask<IEnumerable<DiscordGuildMember>> SearchGuildMembersAsync
	(
		Int64 guildId,
		String query,
		Int32? limit = null,
		CancellationToken ct = default
	)
	{
		IEnumerable<DiscordGuildMember> members = await this.underlying.SearchGuildMembersAsync
		(
			guildId,
			query,
			limit,
			ct
		);

		await Parallel.ForEachAsync
		(
			members,
			async (member, _) =>
				await this.cache.CacheObjectAsync
				(
					KeyHelper.GetGuildMemberKey
					(
						guildId,
						member.User.Value.Id
					),
					member
				)
		);

		return members;
	}

	/// <inheritdoc/>
	public async ValueTask<ListActiveThreadsResponsePayload> ListActiveThreadsAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	)
	{
		ListActiveThreadsResponsePayload response = await this.underlying.ListActiveThreadsAsync
		(
			guildId,
			ct
		);

		await Parallel.ForEachAsync
		(
			response.Threads,
			async (thread, _) =>
				await this.cache.CacheObjectAsync
				(
					KeyHelper.GetChannelKey
					(
						thread.Id
					),
					thread
				)
		);

		await Parallel.ForEachAsync
		(
			response.MemberObjects,
			async (member, _) =>
			{
				if
				(
					!member.ThreadId.Resolve(out Int64 threadId)
					|| !member.UserId.Resolve(out Int64 userId)
				)
				{
					return;
				}

				await this.cache.CacheObjectAsync
				(
					KeyHelper.GetThreadMemberKey
					(
						threadId,
						userId
					),
					member
				);
			}
		);

		return response;
	}

	public async ValueTask<IEnumerable<DiscordGuildMember>> ListGuildMembersAsync
	(
		Int64 guildId,
		Int32? limit = null,
		Int64? afterUserId = null,
		CancellationToken ct = default
	)
	{
		IEnumerable<DiscordGuildMember> members = await this.underlying.ListGuildMembersAsync
		(
			guildId,
			limit,
			afterUserId,
			ct
		);

		await Parallel.ForEachAsync
		(
			members,
			async (member, _) =>
			{
				if(!member.User.Resolve(out DiscordUser? user))
				{
					return;
				}

				await this.cache.CacheObjectAsync
				(
					KeyHelper.GetGuildMemberKey
					(
						guildId,
						user.Id
					),
					member
				);
			}
		);

		return members;
	}

	// redirects
	public partial ValueTask<DiscordGuildMember?> AddGuildMemberAsync(Int64 guildId, Int64 userId, AddGuildMemberRequestPayload payload, CancellationToken ct = default);
	public partial ValueTask<Boolean> AddGuildMemberRoleAsync(Int64 guildId, Int64 userId, Int64 roleId, String? reason = null, CancellationToken ct = default);
	public partial ValueTask BanMemberAsync(Int64 guildId, Int64 userId, Int32 deleteMessageDays, String? reason = null, CancellationToken ct = default);
	public partial ValueTask<Int32?> BeginGuildPruneAsync(Int64 guildId, Int32? days = null, String? roles = null, Boolean? computeCount = null, String? reason = null, CancellationToken ct = default);
	public partial ValueTask<DiscordChannel> CreateGuildChannelAsync(Int64 guildId, CreateGuildChannelRequestPayload payload, String? reason = null, CancellationToken ct = default);
	public partial ValueTask<DiscordRole> CreateRoleAsync(Int64 guildId, CreateGuildRoleRequestPayload payload, String? reason = null, CancellationToken ct = default);
	public partial ValueTask<Boolean> DeleteGuildAsync(Int64 guildId, CancellationToken ct = default);
	public partial ValueTask<Boolean> DeleteGuildIntegrationAsync(Int64 guildId, Int64 integrationId, String? reason = null, CancellationToken ct = default);
	public partial ValueTask<Boolean> DeleteRoleAsync(Int64 guildId, Int64 roleId, String? reason, CancellationToken ct = default);
	public partial ValueTask<DiscordGuildBan> GetGuildBanAsync(Int64 guildId, Int64 userId, CancellationToken ct = default);
	public partial ValueTask<IEnumerable<DiscordGuildBan>> GetGuildBansAsync(Int64 guildId, CancellationToken ct = default);
	public partial ValueTask<IEnumerable<DiscordGuildIntegration>> GetGuildIntegrationsAsync(Int64 guildId, CancellationToken ct = default);
	public partial ValueTask<Int32> GetGuildPruneCountAsync(Int64 guildId, Int32? days = null, String? roles = null, CancellationToken ct = default);
	public partial ValueTask<DiscordInvite> GetGuildVanityInviteAsync(Int64 guildId, CancellationToken ct = default);
	public partial ValueTask<IEnumerable<DiscordVoiceRegion>> GetGuildVoiceRegionsAsync(Int64 guildId, CancellationToken ct = default);
	public partial ValueTask<DiscordGuildWidget> GetGuildWidgetAsync(Int64 guildId, CancellationToken ct = default);
	public partial ValueTask<Stream> GetGuildWidgetImageAsync(Int64 guildId, String? style = null, CancellationToken ct = default);
	public partial ValueTask<DiscordGuildWidgetSettings> GetGuildWidgetSettingsAsync(Int64 guildId, CancellationToken ct = default);
	public partial ValueTask<DiscordGuildMember> ModifyCurrentMemberAsync(Int64 guildId, String nickname, String? reason = null, CancellationToken ct = default);
	public partial ValueTask<Boolean> ModifyCurrentUserVoiceStateAsync(Int64 guildId, ModifyCurrentUserVoiceStateRequestPayload payload, CancellationToken ct = default);
	public partial ValueTask<DiscordGuild> ModifyGuildAsync(Int64 guildId, ModifyGuildRequestPayload payload, String? reason = null, CancellationToken ct = default);
	public partial ValueTask<Boolean> ModifyGuildChannelPositionsAsync(Int64 guildId, IEnumerable<ModifyGuildChannelPositionRequestPayload> payload, CancellationToken ct = default);
	public partial ValueTask<DiscordGuildMember> ModifyGuildMemberAsync(Int64 guildId, Int64 userId, ModifyGuildMemberRequestPayload payload, String? reason = null, CancellationToken ct = default);
	public partial ValueTask<DiscordGuildMultiFactorAuthLevel> ModifyGuildMFALevelAsync(Int64 guildId, DiscordGuildMultiFactorAuthLevel level, String? reason = null, CancellationToken ct = default);
	public partial ValueTask<DiscordGuildWidget> ModifyGuildWidgetSettingsAsync(Int64 guildId, DiscordGuildWidgetSettings settings, String? reason = null, CancellationToken ct = default);
	public partial ValueTask<DiscordRole> ModifyRoleAsync(Int64 guildId, Int64 roleId, ModifyGuildRoleRequestPayload payload, String? reason = null, CancellationToken ct = default);
	public partial ValueTask<IEnumerable<DiscordRole>> ModifyRolePositionsAsync(Int64 guildId, IEnumerable<ModifyRolePositionRequestPayload> payload, String? reason = null, CancellationToken ct = default);
	public partial ValueTask ModifyUserVoiceStateAsync(Int64 guildId, Int64 userId, ModifyUserVoiceStateRequestPayload payload, CancellationToken ct = default);
	public partial ValueTask<Boolean> RemoveGuildMemberAsync(Int64 guildId, Int64 userId, String? reason = null, CancellationToken ct = default);
	public partial ValueTask<Boolean> RemoveGuildMemberRoleAsync(Int64 guildId, Int64 userId, Int64 roleId, String? reason = null, CancellationToken ct = default);
	public partial ValueTask<Boolean> UnbanMemberAsync(Int64 guildId, Int64 userId, String? reason = null, CancellationToken ct = default);
}

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
			async (channel, __) =>
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

	public ValueTask<IEnumerable<DiscordInvite>> GetGuildInvitesAsync(Int64 guildId, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<DiscordGuildMember> GetGuildMemberAsync(Int64 guildId, Int64 userId, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<DiscordGuildPreview> GetGuildPreviewAsync(Int64 guildId, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<IEnumerable<DiscordRole>> GetRolesAsync(Int64 guildId, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<IEnumerable<DiscordGuildMember>> SearchGuildMembersAsync(Int64 guildId, String query, Int32? limit = null, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<ListActiveThreadsResponsePayload> ListActiveThreadsAsync(Int64 guildId, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<IEnumerable<DiscordGuildMember>> ListGuildMembersAsync(Int64 guildId, Int32? limit = null, Int64? afterUserId = null, CancellationToken ct = default) => throw new NotImplementedException();

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

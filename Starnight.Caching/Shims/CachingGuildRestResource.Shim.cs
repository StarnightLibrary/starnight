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

/// <summary>
/// Represents a shim over the present guild rest resource, updating and corroborating from
/// cache where possible.
/// </summary>
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
	public async ValueTask<DiscordGuildMember?> AddGuildMemberAsync
	(
		Int64 guildId,
		Int64 userId,
		AddGuildMemberRequestPayload payload,
		CancellationToken ct = default
	)
	{
		DiscordGuildMember? member = await this.underlying.AddGuildMemberAsync
		(
			guildId,
			userId,
			payload,
			ct
		);

		if(member is not null)
		{
			await this.cache.CacheObjectAsync
			(
				KeyHelper.GetGuildMemberKey
				(
					guildId,
					userId
				),
				member
			);
		}

		return member;
	}
	public ValueTask<DiscordChannel> CreateGuildChannelAsync(Int64 guildId, CreateGuildChannelRequestPayload payload, String? reason = null, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<DiscordRole> CreateRoleAsync(Int64 guildId, CreateGuildRoleRequestPayload payload, String? reason = null, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<Boolean> DeleteGuildAsync(Int64 guildId, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<Boolean> DeleteGuildIntegrationAsync(Int64 guildId, Int64 integrationId, String? reason = null, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<Boolean> DeleteRoleAsync(Int64 guildId, Int64 roleId, String? reason, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<DiscordGuild> GetGuildAsync(Int64 guildId, Boolean? withCounts = null, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<DiscordGuildBan> GetGuildBanAsync(Int64 guildId, Int64 userId, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<IEnumerable<DiscordGuildBan>> GetGuildBansAsync(Int64 guildId, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<IEnumerable<DiscordChannel>> GetGuildChannelsAsync(Int64 guildId, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<IEnumerable<DiscordGuildIntegration>> GetGuildIntegrationsAsync(Int64 guildId, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<IEnumerable<DiscordInvite>> GetGuildInvitesAsync(Int64 guildId, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<DiscordGuildMember> GetGuildMemberAsync(Int64 guildId, Int64 userId, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<DiscordGuildPreview> GetGuildPreviewAsync(Int64 guildId, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<Int32> GetGuildPruneCountAsync(Int64 guildId, Int32? days = null, String? roles = null, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<DiscordInvite> GetGuildVanityInviteAsync(Int64 guildId, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<IEnumerable<DiscordVoiceRegion>> GetGuildVoiceRegionsAsync(Int64 guildId, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<DiscordGuildWidget> GetGuildWidgetAsync(Int64 guildId, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<Stream> GetGuildWidgetImageAsync(Int64 guildId, String? style = null, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<DiscordGuildWidgetSettings> GetGuildWidgetSettingsAsync(Int64 guildId, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<IEnumerable<DiscordRole>> GetRolesAsync(Int64 guildId, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<ListActiveThreadsResponsePayload> ListActiveThreadsAsync(Int64 guildId, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<IEnumerable<DiscordGuildMember>> ListGuildMembersAsync(Int64 guildId, Int32? limit = null, Int64? afterUserId = null, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<DiscordGuildMember> ModifyCurrentMemberAsync(Int64 guildId, String nickname, String? reason = null, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<Boolean> ModifyCurrentUserVoiceStateAsync(Int64 guildId, ModifyCurrentUserVoiceStateRequestPayload payload, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<DiscordGuild> ModifyGuildAsync(Int64 guildId, ModifyGuildRequestPayload payload, String? reason = null, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<Boolean> ModifyGuildChannelPositionsAsync(Int64 guildId, IEnumerable<ModifyGuildChannelPositionRequestPayload> payload, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<DiscordGuildMember> ModifyGuildMemberAsync(Int64 guildId, Int64 userId, ModifyGuildMemberRequestPayload payload, String? reason = null, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<DiscordGuildMultiFactorAuthLevel> ModifyGuildMFALevelAsync(Int64 guildId, DiscordGuildMultiFactorAuthLevel level, String? reason = null, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<DiscordGuildWidget> ModifyGuildWidgetSettingsAsync(Int64 guildId, DiscordGuildWidgetSettings settings, String? reason = null, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<DiscordRole> ModifyRoleAsync(Int64 guildId, Int64 roleId, ModifyGuildRoleRequestPayload payload, String? reason = null, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<IEnumerable<DiscordRole>> ModifyRolePositionsAsync(Int64 guildId, IEnumerable<ModifyRolePositionRequestPayload> payload, String? reason = null, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask ModifyUserVoiceStateAsync(Int64 guildId, Int64 userId, ModifyUserVoiceStateRequestPayload payload, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<Boolean> RemoveGuildMemberAsync(Int64 guildId, Int64 userId, String? reason = null, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<Boolean> RemoveGuildMemberRoleAsync(Int64 guildId, Int64 userId, Int64 roleId, String? reason = null, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<IEnumerable<DiscordGuildMember>> SearchGuildMembersAsync(Int64 guildId, String query, Int32? limit = null, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<Boolean> UnbanMemberAsync(Int64 guildId, Int64 userId, String? reason = null, CancellationToken ct = default) => throw new NotImplementedException();
}

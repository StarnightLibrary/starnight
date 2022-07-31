namespace Starnight.Internal.Rest.Resources.Discord;

using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using Starnight.Caching.Abstractions;
using Starnight.Internal.Entities.Guilds.Invites;

using static DiscordApiConstants;

using HttpMethodEnum = HttpMethod;

/// <inheritdoc cref="IDiscordInviteRestResource"/>
public class DiscordInviteRestResource : AbstractRestResource, IDiscordInviteRestResource
{
	private readonly RestClient __rest_client;

	public DiscordInviteRestResource
	(
		RestClient client,
		ICacheService cache
	)
		: base(cache)
		=> this.__rest_client = client;

	/// <inheritdoc/>
	public async ValueTask<DiscordInvite> GetInviteAsync
	(
		String inviteCode,
		Boolean? withCounts = null,
		Boolean? withExpiration = null,
		Int64? scheduledEventId = null
	)
	{
		QueryBuilder builder = new($"{BaseUri}/{Templates}/{inviteCode}");

		_ = builder.AddParameter("with_counts", withCounts.ToString())
			.AddParameter("with_expiration", withExpiration.ToString())
			.AddParameter("guild_scheduled_event_id", scheduledEventId.ToString());

		IRestRequest request = new RestRequest
		{
			Path = $"/{Invites}/{InviteCode}",
			Url = builder.Build(),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Invites}/{InviteCode}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordInvite>(await response.Content.ReadAsStringAsync())!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordInvite> DeleteInviteAsync
	(
		String inviteCode,
		String? reason = null
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Invites}/{InviteCode}",
			Url = new($"{BaseUri}/{Invites}/{inviteCode}"),
			Method = HttpMethodEnum.Delete,
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{Invites}/{InviteCode}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordInvite>(await response.Content.ReadAsStringAsync())!;
	}
}

namespace Starnight.Internal.Rest.Resources;

using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Memory;

using Starnight.Internal.Entities.Guilds.Invites;

using static DiscordApiConstants;

using HttpMethodEnum = HttpMethod;

/// <summary>
/// Represents a wrapper for all requests to the invite rest resource.
/// </summary>
public class DiscordInviteRestResource : AbstractRestResource
{
	private readonly RestClient __rest_client;

	public DiscordInviteRestResource
	(
		RestClient client,
		IMemoryCache cache
	)
		: base(cache)
		=> this.__rest_client = client;

	/// <summary>
	/// Returns the queried invite.
	/// </summary>
	/// <param name="inviteCode">Invite code identifying this invite.</param>
	/// <param name="withCounts">Whether the invite should contain approximate member counts.</param>
	/// <param name="withExpiration">Whether the invite should contain the expiration date</param>
	/// <param name="scheduledEventId">The scheduled event to include with the invite.</param>
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
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordInvite>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Deletes the given invite.
	/// </summary>
	/// <param name="inviteCode">The code identifying the invite.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <returns>The deleted invite object.</returns>
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
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordInvite>(await response.Content.ReadAsStringAsync())!;
	}
}

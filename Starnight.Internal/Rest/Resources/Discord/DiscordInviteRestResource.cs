namespace Starnight.Internal.Rest.Resources.Discord;

using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Starnight.Caching.Abstractions;
using Starnight.Internal.Entities.Guilds.Invites;

using static DiscordApiConstants;

/// <inheritdoc cref="IDiscordInviteRestResource"/>
public sealed class DiscordInviteRestResource
	: AbstractRestResource, IDiscordInviteRestResource
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
		Int64? scheduledEventId = null,
		CancellationToken ct = default
	)
	{
		QueryBuilder builder = new
		(
			$"{Templates}/{inviteCode}"
		);

		_ = builder.AddParameter
			(
				"with_counts",
				withCounts.ToString()
			)
			.AddParameter
			(
				"with_expiration",
				withExpiration.ToString()
			)
			.AddParameter
			(
				"guild_scheduled_event_id",
				scheduledEventId.ToString()
			);

		IRestRequest request = new RestRequest
		{
			Url = builder.Build(),
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Invites}/{InviteCode}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<DiscordInvite>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordInvite> DeleteInviteAsync
	(
		String inviteCode,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Invites}/{inviteCode}",
			Method = HttpMethod.Delete,
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

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<DiscordInvite>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}
}

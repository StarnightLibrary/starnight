namespace Starnight.Internal.Rest.Resources.Discord;

using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using Starnight.Caching.Abstractions;
using Starnight.Internal.Entities.Guilds.Audit;

using static DiscordApiConstants;

using HttpMethodEnum = HttpMethod;

/// <inheritdoc cref="IDiscordAuditLogRestResource"/>
public class DiscordAuditLogRestResource : AbstractRestResource, IDiscordAuditLogRestResource
{
	private readonly RestClient __rest_client;

	/// <inheritdoc/>
	public DiscordAuditLogRestResource
	(
		RestClient client,
		ICacheService cache
	)
		: base(cache)
		=> this.__rest_client = client;

	/// <inheritdoc/>
	public async ValueTask<DiscordAuditLogObject> GetGuildAuditLogAsync
	(
		Int64 guildId,
		Int64? userId = null,
		DiscordAuditLogEvent? actionType = null,
		Int64? before = null,
		Int32? limit = null
	)
	{
		QueryBuilder builder = new($"{BaseUri}/{Guilds}/{guildId}/{AuditLogs}");

		_ = builder.AddParameter("user_id", userId.ToString())
			.AddParameter("action_type", ((Int32?)actionType).ToString())
			.AddParameter("before", before.ToString())
			.AddParameter("limit", limit.ToString());

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{AuditLogs}",
			Url = builder.Build(),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{AuditLogs}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordAuditLogObject>(await response.Content.ReadAsStringAsync())!;
	}
}

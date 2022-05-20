namespace Starnight.Internal.Rest.Resources;

using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Memory;

using Starnight.Internal.Entities.Guilds.Audit;

using static DiscordApiConstants;

using HttpMethodEnum = Starnight.Internal.Rest.HttpMethod;

/// <summary>
/// Represents a wrapper for all requests to the audit log rest resource.
/// </summary>
public class DiscordAuditLogRestResource : AbstractRestResource
{
	private readonly RestClient __rest_client;

	public DiscordAuditLogRestResource(RestClient client, IMemoryCache cache)
		: base(cache) => this.__rest_client = client;

	/// <summary>
	/// Fetches the audit logs for this guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="userId">User ID to obtain entries from.</param>
	/// <param name="actionType">Action type to obtain entries for.</param>
	/// <param name="before">Snowflake identifier all returned entries will precede.</param>
	/// <param name="limit">Maximum number of entries to return, defaults to 50.</param>
	public async ValueTask<DiscordAuditLogObject> GetGuildAuditLogAsync(Int64 guildId, Int64? userId = null,
		DiscordAuditLogEvent? actionType = null, Int64? before = null, Int32? limit = null)
	{
		QueryBuilder builder = new($"{BaseUri}/{Guilds}/{guildId}/{AuditLogs}");

		if(userId != null)
		{
			_ = builder.AddParameter("user_id", userId.ToString()!);
		}

		if(actionType != null)
		{
			_ = builder.AddParameter("action_type", ((Int32)actionType).ToString()!);
		}

		if(before != null)
		{
			_ = builder.AddParameter("before", before.ToString()!);
		}

		if(limit != null)
		{
			_ = builder.AddParameter("limit", limit.ToString()!);
		}

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{AuditLogs}",
			Url = builder.Build(),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{AuditLogs}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordAuditLogObject>(await response.Content.ReadAsStringAsync())!;
	}
}

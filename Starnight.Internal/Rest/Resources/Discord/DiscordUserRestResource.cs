namespace Starnight.Internal.Rest.Resources.Discord;

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using Starnight.Caching.Abstractions;
using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Entities.Users;
using Starnight.Internal.Rest.Payloads.Users;

using static DiscordApiConstants;

using HttpMethodEnum = HttpMethod;

/// <inheritdoc cref="IDiscordUserRestResource"/>
public class DiscordUserRestResource : AbstractRestResource, IDiscordUserRestResource
{
	private readonly RestClient __rest_client;

	/// <inheritdoc/>
	public DiscordUserRestResource
	(
		RestClient client,
		ICacheService cache
	)
		: base(cache)
		=> this.__rest_client = client;

	/// <inheritdoc/>
	public async ValueTask<DiscordUser> GetCurrentUserAsync()
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Users}/{Me}",
			Url = new($"{BaseUri}/{Users}/{Me}"),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Users}/{Me}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordUser>(await response.Content.ReadAsStringAsync())!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordUser> GetUserAsync
	(
		Int64 userId
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Users}/{UserId}",
			Url = new($"{BaseUri}/{Users}/{userId}"),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Users}/{UserId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordUser>(await response.Content.ReadAsStringAsync())!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordUser> ModifyCurrentUserAsync
	(
		ModifyCurrentUserRequestPayload payload
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Users}/{Me}",
			Url = new($"{BaseUri}/{Users}/{Me}"),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Users}/{Me}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordUser>(await response.Content.ReadAsStringAsync())!;
	}

	/// <inheritdoc/>
	public async ValueTask<IEnumerable<DiscordGuild>> GetCurrentUserGuildsAsync
	(
		Int64? before = null,
		Int64? after = null,
		Int32? limit = null
	)
	{
		QueryBuilder builder = new($"{BaseUri}/{Users}/{Me}/{Guilds}");

		_ = builder.AddParameter("before", before?.ToString())
			.AddParameter("after", after?.ToString())
			.AddParameter("limit", limit?.ToString());

		IRestRequest request = new RestRequest
		{
			Path = $"/{Users}/{Me}/{Guilds}",
			Url = builder.Build(),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Users}/{Me}/{Guilds}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<IEnumerable<DiscordGuild>>(await response.Content.ReadAsStringAsync())!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordGuildMember> GetCurrentUserGuildMemberAsync
	(
		Int64 guildId
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Users}/{Me}/{Guilds}/{GuildId}/{Member}",
			Url = new($"{BaseUri}/{Users}/{Me}/{Guilds}/{guildId}/{Member}"),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Users}/{Me}/{Guilds}/{GuildId}/{Member}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordGuildMember>(await response.Content.ReadAsStringAsync())!;
	}

	/// <inheritdoc/>
	public async ValueTask<Boolean> LeaveGuildAsync
	(
		Int64 guildId
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Users}/{Me}/{Guilds}/{GuildId}",
			Url = new($"{BaseUri}/{Users}/{Me}/{Guilds}/{guildId}"),
			Method = HttpMethodEnum.Delete,
			Context = new()
			{
				["endpoint"] = $"/{Users}/{Me}/{Guilds}/{GuildId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return response.StatusCode == HttpStatusCode.NoContent;
	}
}

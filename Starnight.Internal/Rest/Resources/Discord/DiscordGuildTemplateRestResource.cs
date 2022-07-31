namespace Starnight.Internal.Rest.Resources.Discord;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using Starnight.Caching.Abstractions;
using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Rest.Payloads.GuildTemplates;

using static DiscordApiConstants;

using HttpMethodEnum = HttpMethod;

/// <inheritdoc cref="IDiscordGuildTemplateRestResource"/>
public class DiscordGuildTemplateRestResource : AbstractRestResource, IDiscordGuildTemplateRestResource
{
	private readonly RestClient __rest_client;

	/// <inheritdoc/>
	public DiscordGuildTemplateRestResource
	(
		RestClient client,
		ICacheService cache
	)
		: base(cache)
		=> this.__rest_client = client;

	/// <inheritdoc/>
	public async ValueTask<DiscordGuildTemplate> GetGuildTemplateAsync
	(
		String templateCode
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{Templates}/{TemplateCode}",
			Url = new($"{BaseUri}/{Guilds}/{Templates}/{templateCode}"),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{Templates}/{TemplateCode}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordGuildTemplate>(await response.Content.ReadAsStringAsync())!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordGuild> CreateGuildFromTemplateAsync
	(
		String templateCode,
		CreateGuildFromTemplateRequestPayload payload
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{Templates}/{TemplateCode}",
			Url = new($"{BaseUri}/{Guilds}/{Templates}/{templateCode}"),
			Payload = JsonSerializer.Serialize(payload),
			Method = HttpMethodEnum.Post,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{Templates}/{TemplateCode}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordGuild>(await response.Content.ReadAsStringAsync())!;
	}

	/// <inheritdoc/>
	public async ValueTask<IEnumerable<DiscordGuildTemplate>> GetGuildTemplatesAsync
	(
		Int64 guildId
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Templates}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Templates}"),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Templates}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<IEnumerable<DiscordGuildTemplate>>(await response.Content.ReadAsStringAsync())!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordGuildTemplate> CreateGuildTemplateAsync
	(
		Int64 guildId,
		CreateGuildTemplateRequestPayload payload
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Templates}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Templates}"),
			Payload = JsonSerializer.Serialize(payload),
			Method = HttpMethodEnum.Post,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Templates}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordGuildTemplate>(await response.Content.ReadAsStringAsync())!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordGuildTemplate> SyncGuildTemplateAsync
	(
		Int64 guildId,
		String templateCode
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Templates}/{TemplateCode}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Templates}/{templateCode}"),
			Method = HttpMethodEnum.Put,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Templates}/{TemplateCode}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordGuildTemplate>(await response.Content.ReadAsStringAsync())!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordGuildTemplate> ModifyGuildTemplateAsync
	(
		Int64 guildId,
		String templateCode,
		ModifyGuildTemplateRequestPayload payload
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Templates}/{TemplateCode}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Templates}/{templateCode}"),
			Payload = JsonSerializer.Serialize(payload),
			Method = HttpMethodEnum.Patch,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Templates}/{TemplateCode}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordGuildTemplate>(await response.Content.ReadAsStringAsync())!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordGuildTemplate> DeleteGuildTemplateAsync
	(
		Int64 guildId,
		String templateCode
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Templates}/{TemplateCode}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Templates}/{templateCode}"),
			Method = HttpMethodEnum.Delete,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Templates}/{TemplateCode}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordGuildTemplate>(await response.Content.ReadAsStringAsync())!;
	}
}

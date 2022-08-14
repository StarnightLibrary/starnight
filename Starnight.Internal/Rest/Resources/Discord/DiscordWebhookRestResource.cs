namespace Starnight.Internal.Rest.Resources.Discord;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Channels;
using System.Threading.Tasks;

using Starnight.Caching.Abstractions;
using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Rest.Payloads.Webhooks;

using static DiscordApiConstants;

using HttpMethodEnum = HttpMethod;

/// <inheritdoc cref="IDiscordWebhookRestResource"/>
public sealed class DiscordWebhookRestResource
	: AbstractRestResource, IDiscordWebhookRestResource
{
	private readonly RestClient __rest_client;

	public DiscordWebhookRestResource
	(
		RestClient client,
		ICacheService cache
	)
		: base(cache)
		=> this.__rest_client = client;

	/// <inheritdoc/>
	public async ValueTask<DiscordWebhook> CreateWebhookAsync
	(
		Int64 channelId,
		CreateWebhookRequestPayload payload,
		String? reason
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Channels}/{channelId}/{Webhooks}",
			Url = new($"{Channels}/{channelId}/{Webhooks}"),
			Payload = JsonSerializer.Serialize(payload, StarnightConstants.DefaultSerializerOptions),
			Method = HttpMethodEnum.Post,
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Webhooks}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordWebhook>
			(await response.Content.ReadAsStringAsync(), StarnightConstants.DefaultSerializerOptions)!;
	}

	/// <inheritdoc/>
	public async ValueTask<IEnumerable<DiscordWebhook>> GetChannelWebhooksAsync
	(
		Int64 channelId
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Channels}/{channelId}/{Webhooks}",
			Url = new($"{Channels}/{channelId}/{Webhooks}"),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Webhooks}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<IEnumerable<DiscordWebhook>>
			(await response.Content.ReadAsStringAsync(), StarnightConstants.DefaultSerializerOptions)!;
	}

	/// <inheritdoc/>
	public async ValueTask<IEnumerable<DiscordWebhook>> GetGuildWebhooksAsync
	(
		Int64 guildId
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}/{Webhooks}",
			Url = new($"{Guilds}/{guildId}/{Webhooks}"),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Webhooks}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<IEnumerable<DiscordWebhook>>
			(await response.Content.ReadAsStringAsync(), StarnightConstants.DefaultSerializerOptions)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordWebhook> GetWebhookAsync
	(
		Int64 webhookId
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Webhooks}/{webhookId}",
			Url = new($"{Webhooks}/{webhookId}"),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Webhooks}/{webhookId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordWebhook>
			(await response.Content.ReadAsStringAsync(), StarnightConstants.DefaultSerializerOptions)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordWebhook> GetWebhookWithTokenAsync
	(
		Int64 webhookId,
		String webhookToken
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Webhooks}/{webhookId}/{WebhookToken}",
			Url = new($"{Webhooks}/{webhookId}/{webhookToken}"),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Webhooks}/{webhookId}/{WebhookToken}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = true,
				["is-webhook-request"] = true
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordWebhook>
			(await response.Content.ReadAsStringAsync(), StarnightConstants.DefaultSerializerOptions)!;
	}
}

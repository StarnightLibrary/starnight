namespace Starnight.Internal.Rest.Resources.Discord;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Channels;
using System.Threading.Tasks;

using Starnight.Caching.Abstractions;
using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Messages;
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

	/// <inheritdoc/>
	public async ValueTask<DiscordWebhook> ModifyWebhookAsync
	(
		Int64 webhookId,
		ModifyWebhookRequestPayload payload,
		String? reason
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Webhooks}/{webhookId}",
			Url = new($"{Webhooks}/{webhookId}"),
			Method = HttpMethodEnum.Patch,
			Payload = JsonSerializer.Serialize(payload, StarnightConstants.DefaultSerializerOptions),
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
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
	public async ValueTask<DiscordWebhook> ModifyWebhookWithTokenAsync
	(
		Int64 webhookId,
		String webhookToken,
		ModifyWebhookWithTokenRequestPayload payload,
		String? reason
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Webhooks}/{webhookId}/{WebhookToken}",
			Url = new($"{Webhooks}/{webhookId}/{webhookToken}"),
			Method = HttpMethodEnum.Patch,
			Payload = JsonSerializer.Serialize(payload, StarnightConstants.DefaultSerializerOptions),
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
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

	/// <inheritdoc/>
	public async ValueTask<Boolean> DeleteWebhookAsync
	(
		Int64 webhookId,
		String? reason
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Webhooks}/{webhookId}",
			Url = new($"{Webhooks}/{webhookId}"),
			Method = HttpMethodEnum.Delete,
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{Webhooks}/{webhookId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return response.StatusCode == HttpStatusCode.NoContent;
	}

	/// <inheritdoc/>
	public async ValueTask<Boolean> DeleteWebhookWithTokenAsync
	(
		Int64 webhookId,
		String webhookToken,
		String? reason
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Webhooks}/{webhookId}/{WebhookToken}",
			Url = new($"{Webhooks}/{webhookId}/{webhookToken}"),
			Method = HttpMethodEnum.Delete,
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{Webhooks}/{webhookId}/{WebhookToken}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = true,
				["is-webhook-request"] = true
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return response.StatusCode == HttpStatusCode.NoContent;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordMessage?> ExecuteWebhookAsync
	(
		Int64 webhookId,
		String webhookToken,
		Boolean? wait,
		Int64? threadId,
		ExecuteWebhookRequestPayload payload
	)
	{
		String payloadBody = JsonSerializer.Serialize(payload, StarnightConstants.DefaultSerializerOptions);

		QueryBuilder builder = new($"{Webhooks}/{webhookId}/{webhookToken}");

		_ = builder.AddParameter("wait", wait?.ToString())
			.AddParameter("thread_id", threadId?.ToString());

		IRestRequest request =

			payload.Files is null ?

				new RestRequest
				{
					Path = $"/{Webhooks}/{webhookId}/{WebhookToken}",
					Url = builder.Build(),
					Payload = payloadBody,
					Method = HttpMethodEnum.Post,
					Context = new()
					{
						["endpoint"] = $"/{Webhooks}/{webhookId}/{webhookToken}",
						["cache"] = this.RatelimitBucketCache,
						["exempt-from-global-limit"] = true,
						["is-webhook-request"] = true
					}
				} :

				new MultipartRestRequest
				{
					Path = $"/{Webhooks}/{webhookId}/{webhookToken}",
					Url = builder.Build(),
					Payload = String.IsNullOrWhiteSpace(payloadBody)
						? new()
						: new()
						{
							["payload_json"] = payloadBody
						},
					Method = HttpMethodEnum.Post,
					Files = payload.Files.ToList(),
					Context = new()
					{
						["endpoint"] = $"/{Webhooks}/{webhookId}/{webhookToken}",
						["cache"] = this.RatelimitBucketCache,
						["exempt-from-global-limit"] = false,
						["is-webhook-request"] = false
					}
				};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return wait ?? false
			? JsonSerializer.Deserialize<DiscordMessage>
				(await response.Content.ReadAsStringAsync(), StarnightConstants.DefaultSerializerOptions)!
			: null;
	}
}

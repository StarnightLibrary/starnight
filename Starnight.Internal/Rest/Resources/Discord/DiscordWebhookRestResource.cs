namespace Starnight.Internal.Rest.Resources.Discord;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Starnight.Caching.Providers.Abstractions;
using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Messages;
using Starnight.Internal.Rest.Payloads.Webhooks;

using static DiscordApiConstants;

/// <inheritdoc cref="IDiscordWebhookRestResource"/>
public sealed class DiscordWebhookRestResource
	: AbstractRestResource, IDiscordWebhookRestResource
{
	private readonly RestClient __rest_client;

	public DiscordWebhookRestResource
	(
		RestClient client,
		ICacheProvider cache
	)
		: base(cache)
		=> this.__rest_client = client;

	/// <inheritdoc/>
	public async ValueTask<DiscordWebhook> CreateWebhookAsync
	(
		Int64 channelId,
		CreateWebhookRequestPayload payload,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Channels}/{channelId}/{Webhooks}",
			Payload = JsonSerializer.Serialize
			(
				payload,
				StarnightInternalConstants.DefaultSerializerOptions
			),
			Method = HttpMethod.Post,
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

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<DiscordWebhook>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<IEnumerable<DiscordWebhook>> GetChannelWebhooksAsync
	(
		Int64 channelId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Channels}/{channelId}/{Webhooks}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Webhooks}",
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

		return JsonSerializer.Deserialize<IEnumerable<DiscordWebhook>>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<IEnumerable<DiscordWebhook>> GetGuildWebhooksAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Guilds}/{guildId}/{Webhooks}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Webhooks}",
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

		return JsonSerializer.Deserialize<IEnumerable<DiscordWebhook>>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordWebhook> GetWebhookAsync
	(
		Int64 webhookId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Webhooks}/{webhookId}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Webhooks}/{webhookId}",
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

		return JsonSerializer.Deserialize<DiscordWebhook>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordWebhook> GetWebhookWithTokenAsync
	(
		Int64 webhookId,
		String webhookToken,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Webhooks}/{webhookId}/{webhookToken}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Webhooks}/{webhookId}/{WebhookToken}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = true,
				["is-webhook-request"] = true
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<DiscordWebhook>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordWebhook> ModifyWebhookAsync
	(
		Int64 webhookId,
		ModifyWebhookRequestPayload payload,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Webhooks}/{webhookId}",
			Method = HttpMethod.Patch,
			Payload = JsonSerializer.Serialize
			(
				payload,
				StarnightInternalConstants.DefaultSerializerOptions
			),
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

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<DiscordWebhook>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordWebhook> ModifyWebhookWithTokenAsync
	(
		Int64 webhookId,
		String webhookToken,
		ModifyWebhookWithTokenRequestPayload payload,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Webhooks}/{webhookId}/{webhookToken}",
			Method = HttpMethod.Patch,
			Payload = JsonSerializer.Serialize
			(
				payload,
				StarnightInternalConstants.DefaultSerializerOptions
			),
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

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<DiscordWebhook>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<Boolean> DeleteWebhookAsync
	(
		Int64 webhookId,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Webhooks}/{webhookId}",
			Method = HttpMethod.Delete,
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

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return response.StatusCode == HttpStatusCode.NoContent;
	}

	/// <inheritdoc/>
	public async ValueTask<Boolean> DeleteWebhookWithTokenAsync
	(
		Int64 webhookId,
		String webhookToken,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Webhooks}/{webhookId}/{webhookToken}",
			Method = HttpMethod.Delete,
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

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return response.StatusCode == HttpStatusCode.NoContent;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordMessage?> ExecuteWebhookAsync
	(
		Int64 webhookId,
		String webhookToken,
		ExecuteWebhookRequestPayload payload,
		Boolean? wait = null,
		Int64? threadId = null,
		CancellationToken ct = default
	)
	{
		String payloadBody = JsonSerializer.Serialize
		(
			payload,
			StarnightInternalConstants.DefaultSerializerOptions
		);

		QueryBuilder builder = new
		(
			$"{Webhooks}/{webhookId}/{webhookToken}"
		);

		_ = builder.AddParameter
			(
				"wait",
				wait?.ToString()
			)
			.AddParameter
			(
				"thread_id",
				threadId?.ToString()
			);

		IRestRequest request =

			payload.Files is null ?

				new RestRequest
				{
					Url = builder.Build(),
					Payload = payloadBody,
					Method = HttpMethod.Post,
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
					Url = builder.Build(),
					Payload = String.IsNullOrWhiteSpace
					(
						payloadBody
					)
						? new()
						: new()
						{
							["payload_json"] = payloadBody
						},
					Method = HttpMethod.Post,
					Files = payload.Files.ToList(),
					Context = new()
					{
						["endpoint"] = $"/{Webhooks}/{webhookId}/{webhookToken}",
						["cache"] = this.RatelimitBucketCache,
						["exempt-from-global-limit"] = true,
						["is-webhook-request"] = true
					}
				};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return wait ?? false
			? JsonSerializer.Deserialize<DiscordMessage>
			  (
				  await response.Content.ReadAsStringAsync
				  (
					  ct
				  ),
				  StarnightInternalConstants.DefaultSerializerOptions
			  )!
			: null;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordMessage> GetWebhookMessageAsync
	(
		Int64 webhookId,
		String webhookToken,
		Int64 messageId,
		Int64? threadId = null,
		CancellationToken ct = default
	)
	{
		QueryBuilder builder = new
		(
			$"{Webhooks}/{webhookId}/{webhookToken}/{Messages}/{messageId}"
		);

		_ = builder.AddParameter
		(
			"thread_id",
			threadId?.ToString()
		);

		IRestRequest request = new RestRequest
		{
			Url = builder.Build(),
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Webhooks}/{webhookId}/{WebhookToken}/{Messages}/{messageId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = true,
				["is-webhook-request"] = true
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<DiscordMessage>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordMessage> EditWebhookMessageAsync
	(
		Int64 webhookId,
		String webhookToken,
		Int64 messageId,
		EditWebhookMessageRequestPayload payload,
		Int64? threadId = null,
		CancellationToken ct = default
	)
	{
		String payloadBody = JsonSerializer.Serialize
		(
			payload,
			StarnightInternalConstants.DefaultSerializerOptions
		);

		QueryBuilder builder = new
		(
			$"{Webhooks}/{webhookId}/{webhookToken}"
		);

		_ = builder.AddParameter
		(
			"thread_id",
			threadId?.ToString()
		);

		IRestRequest request =

			payload.Files is null ?

				new RestRequest
				{
					Url = builder.Build(),
					Payload = payloadBody,
					Method = HttpMethod.Post,
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
					Url = builder.Build(),
					Payload = String.IsNullOrWhiteSpace
					(
						payloadBody
					)
						? new()
						: new()
						{
							["payload_json"] = payloadBody
						},
					Method = HttpMethod.Post,
					Files = payload.Files.ToList(),
					Context = new()
					{
						["endpoint"] = $"/{Webhooks}/{webhookId}/{webhookToken}",
						["cache"] = this.RatelimitBucketCache,
						["exempt-from-global-limit"] = true,
						["is-webhook-request"] = true
					}
				};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<DiscordMessage>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<Boolean> DeleteWebhookMessageAsync
	(
		Int64 webhookId,
		String webhookToken,
		Int64 messageId,
		Int64? threadId = null,
		CancellationToken ct = default
	)
	{
		QueryBuilder builder = new
		(
			$"{Webhooks}/{webhookId}/{webhookToken}/{Messages}/{messageId}"
		);

		_ = builder.AddParameter
		(
			"thread_id",
			threadId?.ToString()
		);

		IRestRequest request = new RestRequest
		{
			Url = builder.Build(),
			Method = HttpMethod.Delete,
			Context = new()
			{
				["endpoint"] = $"/{Webhooks}/{webhookId}/{WebhookToken}/{Messages}/{messageId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = true,
				["is-webhook-request"] = true
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return response.StatusCode == HttpStatusCode.NoContent;
	}
}

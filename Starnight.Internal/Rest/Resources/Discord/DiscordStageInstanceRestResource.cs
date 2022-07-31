namespace Starnight.Internal.Rest.Resources.Discord;

using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using Starnight.Caching.Abstractions;
using Starnight.Internal.Entities.Voice;
using Starnight.Internal.Rest.Payloads.StageInstances;

using static DiscordApiConstants;

using HttpMethodEnum = HttpMethod;

/// <inheritdoc cref="IDiscordStageInstanceRestResource"/>
public class DiscordStageInstanceRestResource : AbstractRestResource, IDiscordStageInstanceRestResource
{
	private readonly RestClient __rest_client;

	public DiscordStageInstanceRestResource
	(
		RestClient client,
		ICacheService cache
	)
		: base(cache)
		=> this.__rest_client = client;

	/// <inheritdoc/>
	public async ValueTask<DiscordStageInstance> CreateStageInstanceAsync
	(
		CreateStageInstanceRequestPayload payload,
		String? reason = null
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{StageInstances}",
			Url = new($"{BaseUri}/{StageInstances}"),
			Payload = JsonSerializer.Serialize(payload),
			Method = HttpMethodEnum.Post,
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{StageInstances}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordStageInstance>(await response.Content.ReadAsStringAsync())!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordStageInstance?> GetStageInstanceAsync
	(
		Int64 channelId
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{StageInstances}/{ChannelId}",
			Url = new($"{BaseUri}/{StageInstances}/{channelId}"),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{StageInstances}/{channelId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordStageInstance>(await response.Content.ReadAsStringAsync());
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordStageInstance> ModifyStageInstanceAsync
	(
		Int64 channelId,
		ModifyStageInstanceRequestPayload payload,
		String? reason = null
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{StageInstances}/{ChannelId}",
			Url = new($"{BaseUri}/{StageInstances}/{channelId}"),
			Payload = JsonSerializer.Serialize(payload),
			Method = HttpMethodEnum.Patch,
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{StageInstances}/{channelId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordStageInstance>(await response.Content.ReadAsStringAsync())!;
	}

	/// <inheritdoc/>
	public async ValueTask<Boolean> DeleteStageInstanceAsync
	(
		Int64 channelId,
		String? reason = null
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{StageInstances}/{ChannelId}",
			Url = new($"{BaseUri}/{StageInstances}/{channelId}"),
			Method = HttpMethodEnum.Delete,
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{StageInstances}/{channelId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return response.StatusCode == HttpStatusCode.NoContent;
	}
}

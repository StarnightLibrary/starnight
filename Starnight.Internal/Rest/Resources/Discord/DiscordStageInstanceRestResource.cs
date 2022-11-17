namespace Starnight.Internal.Rest.Resources.Discord;

using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Starnight.Caching.Providers.Abstractions;
using Starnight.Internal.Entities.Voice;
using Starnight.Internal.Rest.Payloads.StageInstances;

using static DiscordApiConstants;

/// <inheritdoc cref="IDiscordStageInstanceRestResource"/>
public sealed class DiscordStageInstanceRestResource
	: AbstractRestResource, IDiscordStageInstanceRestResource
{
	private readonly RestClient __rest_client;

	public DiscordStageInstanceRestResource
	(
		RestClient client,
		ICacheProvider cache
	)
		: base(cache)
		=> this.__rest_client = client;

	/// <inheritdoc/>
	public async ValueTask<DiscordStageInstance> CreateStageInstanceAsync
	(
		CreateStageInstanceRequestPayload payload,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{StageInstances}",
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
				["endpoint"] = $"/{StageInstances}",
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

		return JsonSerializer.Deserialize<DiscordStageInstance>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordStageInstance?> GetStageInstanceAsync
	(
		Int64 channelId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{StageInstances}/{channelId}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{StageInstances}/{channelId}",
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

		return JsonSerializer.Deserialize<DiscordStageInstance>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		);
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordStageInstance> ModifyStageInstanceAsync
	(
		Int64 channelId,
		ModifyStageInstanceRequestPayload payload,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{StageInstances}/{channelId}",
			Payload = JsonSerializer.Serialize
			(
				payload,
				StarnightInternalConstants.DefaultSerializerOptions
			),
			Method = HttpMethod.Patch,
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

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<DiscordStageInstance>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<Boolean> DeleteStageInstanceAsync
	(
		Int64 channelId,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{StageInstances}/{channelId}",
			Method = HttpMethod.Delete,
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

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return response.StatusCode == HttpStatusCode.NoContent;
	}
}

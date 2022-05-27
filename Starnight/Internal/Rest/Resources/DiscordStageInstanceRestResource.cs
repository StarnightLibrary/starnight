namespace Starnight.Internal.Rest.Resources;

using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Memory;

using Starnight.Internal.Entities.Voice;
using Starnight.Internal.Rest.Payloads.StageInstances;

using static DiscordApiConstants;

using HttpMethodEnum = HttpMethod;

/// <summary>
/// Represents a wrapper for all rest requests to Discord's stage instance resource.
/// </summary>
public class DiscordStageInstanceRestResource : AbstractRestResource
{
	private readonly RestClient __rest_client;

	public DiscordStageInstanceRestResource
	(
		RestClient client,
		IMemoryCache cache
	)
		: base(cache)
		=> this.__rest_client = client;

	/// <summary>
	/// Creates a new stage instance associated to a stage channel.
	/// </summary>
	/// <param name="payload">Request payload, among others containing the channel ID to create a stage instance for.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <returns>The newly created stage instance.</returns>
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
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordStageInstance>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Returns the stage instance associated with the stage channel, if one exists.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the associated stage channel.</param>
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
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordStageInstance>(await response.Content.ReadAsStringAsync());
	}

	/// <summary>
	/// Modifies the given stage instance.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the parent channel.</param>
	/// <param name="payload">Request payload.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <returns>The newly modified stage instance.</returns>
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
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordStageInstance>(await response.Content.ReadAsStringAsync())!;
	}
}

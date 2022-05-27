namespace Starnight.Internal.Rest.Resources;

using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

using Starnight.Internal.Entities.Voice;

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
}

namespace Starnight.Internal.Rest.Resources.Discord;

using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using Starnight.Caching.Abstractions;
using Starnight.Internal.Entities.Voice;

using static DiscordApiConstants;

using HttpMethodEnum = HttpMethod;

/// <inheritdoc cref="IDiscordVoiceRestResource"/>
public sealed class DiscordVoiceRestResource
	: AbstractRestResource, IDiscordVoiceRestResource
{
	private readonly RestClient __rest_client;

	public DiscordVoiceRestResource
	(
		RestClient client,
		ICacheService cache
	)
		: base(cache)
		=> this.__rest_client = client;

	/// <inheritdoc/>
	public async ValueTask<IEnumerable<DiscordVoiceRegion>> ListVoiceRegionsAsync()
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Voice}/{Regions}",
			Url = new($"{Voice}/{Regions}"),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Voice}/{Regions}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<IEnumerable<DiscordVoiceRegion>>
			(await response.Content.ReadAsStringAsync(), StarnightConstants.DefaultSerializerOptions)!;
	}
}

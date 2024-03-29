namespace Starnight.Internal.Gateway.Services;

using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using Starnight.Caching.Providers.Abstractions;
using Starnight.Internal.Gateway.Payloads;
using Starnight.Internal.Rest;
using Starnight.Internal.Rest.Resources.Implementation;

using static DiscordApiConstants;

public sealed class DiscordGatewayRestResource
	: AbstractRestResource
{
	private readonly RestClient restClient;

	public DiscordGatewayRestResource
	(
		RestClient client,
		ICacheProvider cache
	)
		: base(cache)
		=> this.restClient = client;

	public async ValueTask<GetGatewayBotResponsePayload> GetBotGatewayInfoAsync()
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Gateway}/{Bot}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Users}/{Me}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.restClient.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<GetGatewayBotResponsePayload>
			(await response.Content.ReadAsStringAsync(), StarnightInternalConstants.DefaultSerializerOptions)!;
	}

	public async ValueTask<String> GetGatewayUrlAsync()
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Gateway}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Users}/{Me}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = true,
				["is-webhook-request"] = true
			}
		};

		HttpResponseMessage response = await this.restClient.MakeRequestAsync(request);

		// if any of these are ever null, we should yell at Discord.
		return JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement.GetProperty("url")!.GetString()!;
	}
}

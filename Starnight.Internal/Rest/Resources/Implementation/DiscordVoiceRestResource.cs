namespace Starnight.Internal.Rest.Resources.Implementation;

using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Starnight.Caching.Providers.Abstractions;
using Starnight.Internal.Entities.Voice;

using static DiscordApiConstants;

/// <inheritdoc cref="IDiscordVoiceRestResource"/>
public sealed class DiscordVoiceRestResource
	: AbstractRestResource, IDiscordVoiceRestResource
{
	private readonly RestClient restClient;

	public DiscordVoiceRestResource
	(
		RestClient client,
		ICacheProvider cache
	)
		: base(cache)
		=> this.restClient = client;

	/// <inheritdoc/>
	public async ValueTask<IEnumerable<DiscordVoiceRegion>> ListVoiceRegionsAsync
	(
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Voice}/{Regions}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Voice}/{Regions}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.restClient.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<IEnumerable<DiscordVoiceRegion>>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}
}

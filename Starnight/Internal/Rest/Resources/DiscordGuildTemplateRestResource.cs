namespace Starnight.Internal.Rest.Resources;

using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Memory;

using Starnight.Internal.Entities.Guilds;

using static DiscordApiConstants;

using HttpMethodEnum = HttpMethod;

/// <summary>
/// Represents a wrapper for all requests to the guild template resource.
/// </summary>
public class DiscordGuildTemplateRestResource : AbstractRestResource
{
	private readonly RestClient __rest_client;

	/// <inheritdoc/>
	public DiscordGuildTemplateRestResource
	(
		RestClient client,
		IMemoryCache cache
	)
		: base(cache)
		=> this.__rest_client = client;

	/// <summary>
	/// Fetches the guild template object corresponding to the given template code.
	/// </summary>
	public async ValueTask<DiscordGuildTemplate> GetGuildTemplateAsync
	(
		String templateCode
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{Templates}/{TemplateCode}",
			Url = new($"{BaseUri}/{Guilds}/{Templates}/{templateCode}"),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{Templates}/{TemplateCode}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordGuildTemplate>(await response.Content.ReadAsStringAsync())!;
	}
}

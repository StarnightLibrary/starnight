namespace Starnight.Internal.Rest.Resources;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Memory;

using Starnight.Internal.Entities.Guilds;

using static DiscordApiConstants;

using HttpMethodEnum = Starnight.Internal.Rest.HttpMethod;

/// <summary>
/// Represents a wrapper for all requests to the emote rest resource.
/// </summary>
public class DiscordEmoteRestResource : AbstractRestResource
{
	private readonly RestClient __rest_client;

	public DiscordEmoteRestResource(RestClient client, IMemoryCache cache)
		: base(cache) => this.__rest_client = client;

	/// <summary>
	/// Fetches a list of emotes for the given guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	public async ValueTask<IEnumerable<DiscordEmote>> ListGuildEmotesAsync(Int64 guildId)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Emotes}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Emotes}"),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Emotes}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<IEnumerable<DiscordEmote>>(await response.Content.ReadAsStringAsync())!;
	}
}

namespace Starnight.Internal.Rest.Resources;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Memory;

using Starnight.Internal.Entities.Guilds;

using static DiscordApiConstants;

using HttpMethodEnum = HttpMethod;

/// <summary>
/// Represents a wrapper for all requests to the Guild Scheduled Event rest resource.
/// </summary>
public class DiscordScheduledEventRestResource : AbstractRestResource
{
	private readonly RestClient __rest_client;

	/// <inheritdoc/>
	public DiscordScheduledEventRestResource(RestClient client, IMemoryCache cache)
		: base(cache) => this.__rest_client = client;

	/// <summary>
	/// Returns a list of scheduled events taking place in this guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="withUserCount">Whether the answer should include user counts.</param>
	public async ValueTask<IEnumerable<DiscordScheduledEvent>> ListScheduledEventsAsync(Int64 guildId,
		Boolean? withUserCount = null)
	{
		StringBuilder builder = new($"{BaseUri}/{Guilds}/{guildId}/{ScheduledEvents}");

		if(withUserCount is not null)
		{
			_ = builder.Append($"?with_user_count={withUserCount}");
		}

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{ScheduledEvents}",
			Url = new(builder.ToString()),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{ScheduledEvents}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<IEnumerable<DiscordScheduledEvent>>(await response.Content.ReadAsStringAsync())!;
	}
}

namespace Starnight.Internal.Rest.Resources;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using Starnight.Internal.Entities.Guilds;

using static DiscordApiConstants;

using HttpMethodEnum = Starnight.Internal.Rest.HttpMethod;

/// <summary>
/// Represents a request wrapper for all requests against the Guild resource.
/// </summary>
public class DiscordGuildRestResource : IRestResource
{
	private readonly RestClient __rest_client;
	private DateTimeOffset __shared_ratelimit_expiration;
	private readonly ConcurrentDictionary<Guid, TaskCompletionSource<HttpResponseMessage>> __waiting_responses;
	private readonly String __token;

	public DiscordGuildRestResource(RestClient client, String token)
	{
		this.__rest_client = client;
		this.__shared_ratelimit_expiration = DateTimeOffset.MinValue;
		this.__waiting_responses = new();
		this.__token = token;

		this.__rest_client.RequestSucceeded += this.requestSucceeded;
		this.__rest_client.SharedRatelimitHit += this.sharedRatelimitHit;
	}


	public async Task<DiscordGuild> GetGuildAsync(Int64 id, Boolean withCounts = false)
	{
		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Route = $"{Guilds}/{GuildId}",
			Url = new($"{BaseUri}/{Guilds}/{id}?with_counts={withCounts}"),
			Token = this.__token,
			Method = HttpMethodEnum.Get
		};

		TaskCompletionSource<HttpResponseMessage> taskSource = new();

		_ = this.__waiting_responses.AddOrUpdate(guid, taskSource, (x, y) => taskSource);
		this.__rest_client.EnqueueRequest(request, guid);

		HttpResponseMessage response = await taskSource.Task;

		return JsonSerializer.Deserialize<DiscordGuild>(await response.Content.ReadAsStringAsync())!;
	}

	private void sharedRatelimitHit(RatelimitBucket arg1, HttpResponseMessage arg2)
	{
		if(__resource_routes.Contains(arg1.Path!))
		{
			this.__shared_ratelimit_expiration = DateTimeOffset.UtcNow.AddSeconds(
						Double.Parse(arg2.Headers.GetValues("Retry-After").First()));
		}
	}

	private void requestSucceeded(Guid arg1, HttpResponseMessage arg2)
		=> this.__waiting_responses[arg1].SetResult(arg2);

	private readonly static List<String> __resource_routes = new()
	{
		$"{Guilds}/{GuildId}"
	};
}

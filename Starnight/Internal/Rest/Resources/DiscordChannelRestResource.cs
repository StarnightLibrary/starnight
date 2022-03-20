namespace Starnight.Internal.Rest.Resources;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using Starnight.Exceptions;
using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Channels.Threads;

using HttpMethodEnum = HttpMethod;

using static Starnight.Internal.DiscordApiConstants;

public class DiscordChannelRestResource : IRestResource
{
	private readonly RestClient __rest_client;
	private DateTimeOffset __allow_next_request_at;
	private readonly ConcurrentDictionary<Guid, TaskCompletionSource<HttpResponseMessage>> __waiting_responses;
	private readonly String __token;

	public DiscordChannelRestResource(RestClient client, String token)
	{
		this.__rest_client = client;
		this.__allow_next_request_at = DateTimeOffset.MinValue;
		this.__waiting_responses = new();
		this.__token = token;

		this.__rest_client.RequestDenied += this.requestDenied;
		this.__rest_client.RequestSucceeded += this.requestSucceeded;
		this.__rest_client.SharedRatelimitHit += this.sharedRatelimitHit;
		this.__rest_client.TokenInvalidOrMissing += this.disableAll;
	}

	/// <summary>
	/// Returns a channel object for the given ID. If the channel is a thread channel, a
	/// <see cref="DiscordThreadMember"/> object is included in the returned channel.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the channel in question.</param>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<DiscordChannel> GetChannelAsync(Int64 channelId)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordChannelRestResource.GetChannelAsync",
				"channel",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Channels}/{ChannelId}",
			Url = new($"{BaseUri}/{Channels}/{channelId}"),
			Token = this.__token,
			Method = HttpMethodEnum.Get
		};

		TaskCompletionSource<HttpResponseMessage> taskSource = new();

		_ = this.__waiting_responses.AddOrUpdate(guid, taskSource, (x, y) => taskSource);
		this.__rest_client.EnqueueRequest(request, guid);

		HttpResponseMessage response = await taskSource.Task;

		return JsonSerializer.Deserialize<DiscordChannel>(await response.Content.ReadAsStringAsync())!;
	}

	private void sharedRatelimitHit(RatelimitBucket arg1, HttpResponseMessage arg2)
	{
		if(__resource_routes.Contains(arg1.Path!))
		{
			this.__allow_next_request_at = DateTimeOffset.UtcNow.AddSeconds(
				Double.Parse(arg2.Headers.GetValues("Retry-After").First()));
		}
	}

	private void requestSucceeded(Guid arg1, HttpResponseMessage arg2)
		=> this.__waiting_responses[arg1].SetResult(arg2);

	private void requestDenied(Guid arg1, Int32 starnightError, Int32 httpError)
		=> this.__waiting_responses[arg1].SetException(RestExceptionTranslator.TranslateException(starnightError, httpError));

	private void disableAll() => this.__allow_next_request_at = DateTimeOffset.MaxValue;

	private readonly static List<String> __resource_routes = new()
	{
		$"/{Channels}/{ChannelId}"
	};
}

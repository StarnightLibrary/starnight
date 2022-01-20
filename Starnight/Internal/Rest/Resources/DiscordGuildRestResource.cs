namespace Starnight.Internal.Rest.Resources;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Rest.Payloads.Guild;

using static DiscordApiConstants;

using HttpMethodEnum = HttpMethod;

/// <summary>
/// Represents a request wrapper for all requests against the Guild resource.
/// </summary>
public class DiscordGuildRestResource : IRestResource
{
	private readonly RestClient __rest_client;
	private DateTimeOffset __allow_next_request_at;
	private readonly ConcurrentDictionary<Guid, TaskCompletionSource<HttpResponseMessage>> __waiting_responses;
	private readonly String __token;

	public DiscordGuildRestResource(RestClient client, String token)
	{
		this.__rest_client = client;
		this.__allow_next_request_at = DateTimeOffset.MinValue;
		this.__waiting_responses = new();
		this.__token = token;

		this.__rest_client.RequestSucceeded += this.requestSucceeded;
		this.__rest_client.SharedRatelimitHit += this.sharedRatelimitHit;
		this.__rest_client.TokenInvalidOrMissing += this.disableAll;
	}

	/// <summary>
	/// Requests a guild from the discord API.
	/// </summary>
	/// <param name="id">Snowflake identifier of the guild in question.</param>
	/// <param name="withCounts">Whether or not the response should include total and online member counts.</param>
	/// <returns>The guild requested.</returns>
	public async Task<DiscordGuild> GetGuildAsync(Int64 id, Boolean withCounts = false)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			return null!;
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Route = $"/{Guilds}/{GuildId}",
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

	/// <summary>
	/// Requests a guild preview from the discord API.
	/// </summary>
	/// <param name="id">Snowflake identifier of the guild in question.</param>
	/// <returns>The guild requested.</returns>
	public async Task<DiscordGuildPreview> GetGuildPreviewAsync(Int64 id)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			return null!;
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Route = $"/{Guilds}/{GuildId}/{Preview}",
			Url = new($"{BaseUri}/{Guilds}/{id}/{Preview}"),
			Token = this.__token,
			Method = HttpMethodEnum.Get
		};

		TaskCompletionSource<HttpResponseMessage> taskSource = new();

		_ = this.__waiting_responses.AddOrUpdate(guid, taskSource, (x, y) => taskSource);
		this.__rest_client.EnqueueRequest(request, guid);

		HttpResponseMessage response = await taskSource.Task;

		return JsonSerializer.Deserialize<DiscordGuildPreview>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Modifies a guild.
	/// </summary>
	/// <param name="id">Snowflake identifier of the guild in question.</param>
	/// <param name="payload">Change payload for the guild.</param>
	/// <param name="reason">Optional audit log reason for the changes.</param>
	/// <returns>The updated guild.</returns>
	public async Task<DiscordGuild> ModifyGuildAsync(Int64 id, ModifyGuildRequestPayload payload, String? reason = null)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			return null!;
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Route = $"/{Guilds}/{GuildId}",
			Url = new($"{BaseUri}/{Guilds}/{id}"),
			Token = this.__token,
			Method = HttpMethodEnum.Patch,
			Payload = JsonSerializer.Serialize(payload),
			Headers = reason != null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new()
		};

		TaskCompletionSource<HttpResponseMessage> taskSource = new();

		_ = this.__waiting_responses.AddOrUpdate(guid, taskSource, (x, y) => taskSource);
		this.__rest_client.EnqueueRequest(request, guid);

		HttpResponseMessage response = await taskSource.Task;

		return JsonSerializer.Deserialize<DiscordGuild>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Permanently deletes a guild. This user must own the guild.
	/// </summary>
	/// <param name="id">Snowflake identifier of the guild in question.</param>
	/// <returns>Whether or not the request succeeded.</returns>
	public async Task<Boolean> DeleteGuildAsync(Int64 id)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			return false;
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Route = $"/{Guilds}/{GuildId}",
			Url = new($"{BaseUri}/{Guilds}/{id}"),
			Token = this.__token,
			Method = HttpMethodEnum.Delete
		};

		TaskCompletionSource<HttpResponseMessage> taskSource = new();

		_ = this.__waiting_responses.AddOrUpdate(guid, taskSource, (x, y) => taskSource);
		this.__rest_client.EnqueueRequest(request, guid);

		HttpResponseMessage response = await taskSource.Task;

		return response.StatusCode == HttpStatusCode.NoContent;
	}

	/// <summary>
	/// Requests all active channels for this guild from the API. This excludes thread channels.
	/// </summary>
	/// <param name="id">Snowflake identifier of the guild in question.</param>
	public async Task<DiscordChannel[]> GetGuildChannelsAsync(Int64 id)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			return null!;
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Route = $"/{Guilds}/{GuildId}/{Channels}",
			Url = new($"{BaseUri}/{Guilds}/{id}/{Channels}"),
			Token = this.__token,
			Method = HttpMethodEnum.Get
		};

		TaskCompletionSource<HttpResponseMessage> taskSource = new();

		_ = this.__waiting_responses.AddOrUpdate(guid, taskSource, (x, y) => taskSource);
		this.__rest_client.EnqueueRequest(request, guid);

		HttpResponseMessage response = await taskSource.Task;

		return JsonSerializer.Deserialize<DiscordChannel[]>(await response.Content.ReadAsStringAsync())!;
	}

	public async Task<DiscordChannel> CreateGuildChannelAsync(Int64 id, CreateGuildChannelRequestPayload payload, String? reason = null)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			return null!;
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Route = $"/{Guilds}/{GuildId}/{Channels}",
			Url = new($"{BaseUri}/{Guilds}/{id}/{Channels}"),
			Token = this.__token,
			Method = HttpMethodEnum.Post,
			Payload = JsonSerializer.Serialize(payload),
			Headers = reason != null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new()
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

	private void disableAll() => this.__allow_next_request_at = DateTimeOffset.MaxValue;

	private readonly static List<String> __resource_routes = new()
	{
		$"/{Guilds}/{GuildId}",
		$"/{Guilds}/{GuildId}/{Preview}",
		$"/{Guilds}/{GuildId}/{Channels}"
	};
}

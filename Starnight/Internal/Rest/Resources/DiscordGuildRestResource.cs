namespace Starnight.Internal.Rest.Resources;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using Starnight.Exceptions;
using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Rest.Payloads.Guilds;

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

		this.__rest_client.RequestDenied += this.requestDenied;
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

	/// <summary>
	/// Creates a discord channel.
	/// </summary>
	/// <param name="id">Snowflake identifier of the parent guild.</param>
	/// <param name="payload">Channel creation payload, containing all initializing data.</param>
	/// <param name="reason">Audit log reason for this operation.</param>
	/// <returns>The created channel.</returns>
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

	/// <summary>
	/// Moves channels in a guild.
	/// </summary>
	/// <param name="id">Snowflake identifier of the parent guild.</param>
	/// <param name="payload">Array of new channel data payloads, containing IDs and some optional data.</param>
	/// <param name="reason">Audit log reason for this operation.</param>
	/// <returns>Whether or not the call succeeded</returns>
	public async Task<Boolean> ModifyGuildChannelPositionsAsync(Int64 id, ModifyGuildChannelPositionRequestPayload[] payload,
		String? reason = null)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			return false;
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Route = $"/{Guilds}/{GuildId}/{Channels}",
			Url = new($"{BaseUri}/{Guilds}/{id}/{Channels}"),
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

		return response.StatusCode == HttpStatusCode.NoContent;
	}

	/// <summary>
	/// Queries all active thread channels in the given guild.
	/// </summary>
	/// <param name="id">Snowflake identifier of the queried guild.</param>
	/// <returns>A response payload object containing an array of thread channels and an array of thread member information
	/// for all threads the current user has joined.</returns>
	public async Task<ListActiveThreadsResponsePayload> ListActiveThreadsAsync(Int64 id)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			return null!;
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Route = $"/{Guilds}/{GuildId}/{Threads}/{Active}",
			Url = new($"{BaseUri}/{Guilds}/{id}/{Threads}/{Active}"),
			Token = this.__token,
			Method = HttpMethodEnum.Get
		};

		TaskCompletionSource<HttpResponseMessage> taskSource = new();

		_ = this.__waiting_responses.AddOrUpdate(guid, taskSource, (x, y) => taskSource);
		this.__rest_client.EnqueueRequest(request, guid);

		HttpResponseMessage response = await taskSource.Task;

		return JsonSerializer.Deserialize<ListActiveThreadsResponsePayload>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Returns the given users associated guild member object.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the queried guild.</param>
	/// <param name="userId">Snowflake identifier of the user in question.</param>
	/// <returns>A <see cref="DiscordGuildMember"/> object for this user, if available.</returns>
	public async Task<DiscordGuildMember> GetGuildMemberAsync(Int64 guildId, Int64 userId)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			return null!;
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Route = $"/{Guilds}/{GuildId}/{Members}/{UserId}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Members}/{userId}"),
			Token = this.__token,
			Method = HttpMethodEnum.Get
		};

		TaskCompletionSource<HttpResponseMessage> taskSource = new();

		_ = this.__waiting_responses.AddOrUpdate(guid, taskSource, (x, y) => taskSource);
		this.__rest_client.EnqueueRequest(request, guid);

		HttpResponseMessage response = await taskSource.Task;

		return JsonSerializer.Deserialize<DiscordGuildMember>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Returns a list of guild member objects.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild to be queried.</param>
	/// <param name="limit">Amount of users to query, between 1 and 1000</param>
	/// <param name="afterUserId">Highest user ID to <b>not</b> query. Used for request pagination.</param>
	/// <returns>A list of <see cref="DiscordGuildMember"/>s of the specified length.</returns>
	public async Task<DiscordGuildMember[]> ListGuildMembersAsync(Int64 guildId, Int32 limit = 1, Int64 afterUserId = 0)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			return null!;
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Route = $"/{Guilds}/{GuildId}/{Members}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Members}?limit={limit}&after={afterUserId}"),
			Token = this.__token,
			Method = HttpMethodEnum.Get
		};

		TaskCompletionSource<HttpResponseMessage> taskSource = new();

		_ = this.__waiting_responses.AddOrUpdate(guid, taskSource, (x, y) => taskSource);
		this.__rest_client.EnqueueRequest(request, guid);

		HttpResponseMessage response = await taskSource.Task;

		return JsonSerializer.Deserialize<DiscordGuildMember[]>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Returns a list of guild member objects whose username or nickname starts with the given string.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the string in question.</param>
	/// <param name="query">Query string to search for.</param>
	/// <param name="limit">Maximum amount of members to return; 1 - 1000.</param>
	/// <returns></returns>
	public async Task<DiscordGuildMember[]> SearchGuildMembersAsync(Int64 guildId, String query, Int32 limit = 1)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			return null!;
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Route = $"/{Guilds}/{GuildId}/{Members}/{Search}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Members}?query={query}&limit={limit}"),
			Token = this.__token,
			Method = HttpMethodEnum.Get
		};

		TaskCompletionSource<HttpResponseMessage> taskSource = new();

		_ = this.__waiting_responses.AddOrUpdate(guid, taskSource, (x, y) => taskSource);
		this.__rest_client.EnqueueRequest(request, guid);

		HttpResponseMessage response = await taskSource.Task;

		return JsonSerializer.Deserialize<DiscordGuildMember[]>(await response.Content.ReadAsStringAsync())!;
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
		$"/{Guilds}/{GuildId}",
		$"/{Guilds}/{GuildId}/{Preview}",
		$"/{Guilds}/{GuildId}/{Channels}"
	};
}

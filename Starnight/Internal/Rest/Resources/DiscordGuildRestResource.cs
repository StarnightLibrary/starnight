namespace Starnight.Internal.Rest.Resources;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using Starnight.Exceptions;
using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Entities.Guilds.Invites;
using Starnight.Internal.Entities.Voice;
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
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<DiscordGuild> GetGuildAsync(Int64 id, Boolean withCounts = false)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.GetGuildAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}",
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
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<DiscordGuildPreview> GetGuildPreviewAsync(Int64 id)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.GetGuildPreviewAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Preview}",
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
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<DiscordGuild> ModifyGuildAsync(Int64 id, ModifyGuildRequestPayload payload, String? reason = null)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.ModifyGuildAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}",
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
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<Boolean> DeleteGuildAsync(Int64 id)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{

			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.DeleteGuildAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();
		
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}",
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
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<DiscordChannel[]> GetGuildChannelsAsync(Int64 id)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{

			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.GetGuildChannelsAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Channels}",
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
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<DiscordChannel> CreateGuildChannelAsync(Int64 id, CreateGuildChannelRequestPayload payload, String? reason = null)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{

			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.CreateGuildChannelAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Channels}",
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
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<Boolean> ModifyGuildChannelPositionsAsync(Int64 id, ModifyGuildChannelPositionRequestPayload[] payload,
		String? reason = null)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.ModifyGuildChannelPositionsAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Channels}",
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
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<ListActiveThreadsResponsePayload> ListActiveThreadsAsync(Int64 id)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.ListActiveThreadsAync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Threads}/{Active}",
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
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<DiscordGuildMember> GetGuildMemberAsync(Int64 guildId, Int64 userId)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.GetGuildMemberAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Members}/{UserId}",
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
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<DiscordGuildMember[]> ListGuildMembersAsync(Int64 guildId, Int32 limit = 1, Int64 afterUserId = 0)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.ListGuildMembersAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Members}",
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
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<DiscordGuildMember[]> SearchGuildMembersAsync(Int64 guildId, String query, Int32 limit = 1)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.SearchGuildMembersAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Members}/{Search}",
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

	/// <summary>
	/// Adds a discord user to the given guild, using the OAuth2 flow.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="userId">User ID of the guild in question.</param>
	/// <param name="payload">OAuth2 payload, containing the OAuth2 token and initial information for the user.</param>
	/// <returns>The newly created guild member, or null if the member had already joined the guild.</returns>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<DiscordGuildMember?> AddGuildMemberAsync(Int64 guildId, Int64 userId, AddGuildMemberRequestPayload payload)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.AddGuildMemberAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Members}/{UserId}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Members}/{userId}"),
			Token = this.__token,
			Method = HttpMethodEnum.Put,
			Payload = JsonSerializer.Serialize(payload)
		};

		TaskCompletionSource<HttpResponseMessage> taskSource = new();

		_ = this.__waiting_responses.AddOrUpdate(guid, taskSource, (x, y) => taskSource);
		this.__rest_client.EnqueueRequest(request, guid);

		// still awaited so we get any potential errors
		HttpResponseMessage response = await taskSource.Task;

		return response.StatusCode == HttpStatusCode.Created
			? JsonSerializer.Deserialize<DiscordGuildMember>(await response.Content.ReadAsStringAsync())
			: null;
	}

	/// <summary>
	/// Modifies a given user in the given guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="userId">Snowflake identifier of the user in question.</param>
	/// <param name="payload">Edit payload. Refer to the Discord documentation for required permissions.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <returns>The modified guild member.</returns>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<DiscordGuildMember> ModifyGuildMemberAsync(Int64 guildId, Int64 userId,
		ModifyGuildMemberRequestPayload payload, String? reason = null)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.ModifyGuildMemberAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Members}/{UserId}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Members}/{userId}"),
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

		return JsonSerializer.Deserialize<DiscordGuildMember>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Sets the current user's nickname in the given guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="nickname">New nickname for the current user.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <returns>The new current user event.</returns>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<DiscordGuildMember> ModifyCurrentMemberAsync(Int64 guildId, String nickname, String? reason = null)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.ModifyCurrentMemberAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Members}/{Me}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Members}/{Me}"),
			Token = this.__token,
			Method = HttpMethodEnum.Patch,
			Payload = $"{{\"nick\":\"{nickname}\"}}",
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

		return JsonSerializer.Deserialize<DiscordGuildMember>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Adds a role to a guild member in a given guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="userId">Snowflake identifier of the user in question.</param>
	/// <param name="roleId">Snowflake identifier of the role in question.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <returns>Whether the action was successful.</returns>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<Boolean> AddGuildMemberRoleAsync(Int64 guildId, Int64 userId, Int64 roleId, String? reason = null)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.AddGuildMemberRoleAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Members}/{UserId}/{Roles}/{RoleId}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Members}/{userId}/{Roles}/{roleId}"),
			Token = this.__token,
			Method = HttpMethodEnum.Put,
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
	/// Removes the given role from the given member in the given guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="userId">Snowflake identifier of the user in question.</param>
	/// <param name="roleId">Snowflake identifier of the role in question.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <returns>Whether the action was successful.</returns>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<Boolean> RemoveGuildMemberRoleAsync(Int64 guildId, Int64 userId, Int64 roleId, String? reason = null)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.RemoveGuildMemberRoleAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Members}/{UserId}/{Roles}/{RoleId}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Members}/{userId}/{Roles}/{roleId}"),
			Token = this.__token,
			Method = HttpMethodEnum.Delete,
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
	/// Kicks the given user from the given guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="userId">Snowflake identifier of the user in question.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <returns>Returns whether the kick was successful.</returns>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<Boolean> RemoveGuildMemberAsync(Int64 guildId, Int64 userId, String? reason = null)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.RemoveGuildMemberAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Members}/{UserId}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Members}/{userId}"),
			Token = this.__token,
			Method = HttpMethodEnum.Delete,
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
	/// Returns a list of bans from the given guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <returns>An array of <see cref="DiscordGuildBan"/> objects, representing all bans in the guild.</returns>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<DiscordGuildBan[]> GetGuildBansAsync(Int64 guildId)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.GetGuildBansAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Bans}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Bans}"),
			Token = this.__token,
			Method = HttpMethodEnum.Get
		};

		TaskCompletionSource<HttpResponseMessage> taskSource = new();

		_ = this.__waiting_responses.AddOrUpdate(guid, taskSource, (x, y) => taskSource);
		this.__rest_client.EnqueueRequest(request, guid);

		HttpResponseMessage response = await taskSource.Task;

		return JsonSerializer.Deserialize<DiscordGuildBan[]>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Returns the ban object for the given user, or a <see cref="DiscordNotFoundException"/> if there is no associated ban.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="userId">Snowflake identifier of the user in question.</param>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<DiscordGuildBan> GetGuildBanAsync(Int64 guildId, Int64 userId)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.GetGuildBanAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Bans}/{UserId}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Bans}/{userId}"),
			Token = this.__token,
			Method = HttpMethodEnum.Get
		};

		TaskCompletionSource<HttpResponseMessage> taskSource = new();

		_ = this.__waiting_responses.AddOrUpdate(guid, taskSource, (x, y) => taskSource);
		this.__rest_client.EnqueueRequest(request, guid);

		HttpResponseMessage response = await taskSource.Task;

		return JsonSerializer.Deserialize<DiscordGuildBan>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Bans the given user from the given guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="userId">Snowflake identifier of the user in question.</param>
	/// <param name="deleteMessageDays">Specifies how many days of message history from this user shall be purged.</param>
	/// <param name="reason">Specifies an audit log reason for the ban.</param>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task BanMemberAsync(Int64 guildId, Int64 userId, Int32 deleteMessageDays = 0, String? reason = null)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.BanMemberAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Bans}/{UserId}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Bans}/{userId}"),
			Token = this.__token,
			Method = HttpMethodEnum.Put,
			Payload = $"{{\"delete_message_days\": \"{deleteMessageDays}\"}}",
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
	}

	/// <summary>
	/// Removes a ban from the given guild for the given user.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="userId">Snowflake identifier of the user in question.</param>
	/// <param name="reason">Optional audit log reason for the ban.</param>
	/// <returns>Whether the unban was successful.</returns>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<Boolean> UnbanMemberAsync(Int64 guildId, Int64 userId, String? reason = null)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.UnbanMemberAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Bans}/{UserId}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Bans}/{userId}"),
			Token = this.__token,
			Method = HttpMethodEnum.Delete,
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
	/// Fetches a list of all guild roles from the API.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<DiscordRole[]> GetRolesAsync(Int64 guildId)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.GetRolesAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Roles}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Roles}"),
			Token = this.__token,
			Method = HttpMethodEnum.Get
		};

		TaskCompletionSource<HttpResponseMessage> taskSource = new();

		_ = this.__waiting_responses.AddOrUpdate(guid, taskSource, (x, y) => taskSource);
		this.__rest_client.EnqueueRequest(request, guid);

		HttpResponseMessage response = await taskSource.Task;

		return JsonSerializer.Deserialize<DiscordRole[]>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Creates a role in a given guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="payload">Role information to initialize the role with.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <returns>The newly created role object.</returns>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<DiscordRole> CreateRoleAsync(Int64 guildId, RoleMetadataRequestPayload payload, String? reason = null)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.CreateRoleAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Roles}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Roles}"),
			Token = this.__token,
			Payload = JsonSerializer.Serialize(payload),
			Method = HttpMethodEnum.Post,
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

		return JsonSerializer.Deserialize<DiscordRole>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Modifies the positions of roles in the role list.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="payload">Array of id/new position objects.</param>
	/// <param name="reason">Optional audit log reason for this action.</param>
	/// <returns>The newly ordered list of roles for this guild.</returns>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<DiscordRole[]> ModifyRolePositionsAsync(Int64 guildId, ModifyRolePositionRequestPayload[] payload, String? reason = null)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.ModifyRolePositionsAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Roles}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Roles}"),
			Token = this.__token,
			Payload = JsonSerializer.Serialize(payload),
			Method = HttpMethodEnum.Patch,
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

		return JsonSerializer.Deserialize<DiscordRole[]>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Modifies the settings of a specific role.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild the role belongs to.</param>
	/// <param name="roleId">Snowflake identifier of the role in question.</param>
	/// <param name="payload">New role settings for this role.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <returns>The modified role object.</returns>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<DiscordRole> ModifyRoleAsync(Int64 guildId, Int64 roleId, RoleMetadataRequestPayload payload, String? reason = null)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.ModifyRoleAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Roles}/{RoleId}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Roles}/{roleId}"),
			Token = this.__token,
			Payload = JsonSerializer.Serialize(payload),
			Method = HttpMethodEnum.Patch,
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

		return JsonSerializer.Deserialize<DiscordRole>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Deletes a role from a guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild the role belongs to.</param>
	/// <param name="roleId">Snowflake identifier of the role in question.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <returns>Whether the deletion was successful.</returns>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<Boolean> DeleteRoleAsync(Int64 guildId, Int64 roleId, String? reason = null)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.DeleteRoleAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Roles}/{RoleId}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Roles}/{roleId}"),
			Token = this.__token,
			Method = HttpMethodEnum.Delete,
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
	/// Queries how many users would be kicked from a given guild in a prune.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="days">Amount of inactivity days to be measured, 0 to 30</param>
	/// <param name="roles">Comma-separated list of role IDs to include in the prune
	///		<para>
	///		Any user with a subset of these roles will be considered for the prune. Any user with any role not listed here
	///		will not be included in the count.
	///		</para>
	/// </param>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<Int32> GetGuildPruneCountAsync(Int64 guildId, Int32 days = 0, String? roles = null)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.GetGuildPruneCountAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Prune}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Prune}?days={days}{(roles != null ? $"&include_roles={roles}" : "")}"),
			Token = this.__token,
			Method = HttpMethodEnum.Get
		};

		TaskCompletionSource<HttpResponseMessage> taskSource = new();

		_ = this.__waiting_responses.AddOrUpdate(guid, taskSource, (x, y) => taskSource);
		this.__rest_client.EnqueueRequest(request, guid);

		HttpResponseMessage response = await taskSource.Task;

		return JsonDocument
			.Parse(await response.Content.ReadAsStringAsync())
			.RootElement
				.GetProperty("pruned")
					.GetInt32();
	}

	/// <summary>
	/// Initiates a prune from the guild in question.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="days">Amount of inactivity days to be measured, 0 to 30</param>
	/// <param name="roles">Comma-separated list of role IDs to include in the prune
	///		<para>
	///		Any user with a subset of these roles will be considered for the prune. Any user with any role not listed here
	///		will not be included in the count.
	///		</para>
	/// </param>
	/// <param name="computeCount">Whether or not the amount of users pruned should be calculated.</param>
	/// <param name="reason">Optional audit log reason for the prune.</param>
	/// <returns>The amount of users pruned.</returns>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<Int32?> BeginGuildPruneAsync(Int64 guildId, Int32 days = 0, String? roles = null, Boolean? computeCount = null,
		String? reason = null)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.BeginGuildPruneAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Prune}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Prune}?days={days}" +
				$"{(roles != null ? $"&include_roles={roles}" : "")}" +
				$"{(computeCount != null ? $"compute_prune_count={computeCount}" : "")}"),
			Token = this.__token,
			Method = HttpMethodEnum.Post,
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

		return computeCount == true ?
				JsonDocument.Parse(await response.Content.ReadAsStringAsync())
				.RootElement
					.GetProperty("pruned")
						.GetInt32()
			: null;
	}

	/// <summary>
	/// Queries all available voice regions for this guild, including VIP regions.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<DiscordVoiceRegion[]> GetGuildVoiceRegionsAsync(Int64 guildId)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.GetGuildVoiceRegionsAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Voice}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Voice}"),
			Token = this.__token,
			Method = HttpMethodEnum.Get
		};

		TaskCompletionSource<HttpResponseMessage> taskSource = new();

		_ = this.__waiting_responses.AddOrUpdate(guid, taskSource, (x, y) => taskSource);
		this.__rest_client.EnqueueRequest(request, guid);

		HttpResponseMessage response = await taskSource.Task;

		return JsonSerializer.Deserialize<DiscordVoiceRegion[]>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Returns a list of all active invites for this guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<DiscordInvite[]> GetGuildInvitesAsync(Int64 guildId)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.GetGuildInvitesAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Invites}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Invites}"),
			Token = this.__token,
			Method = HttpMethodEnum.Get
		};

		TaskCompletionSource<HttpResponseMessage> taskSource = new();

		_ = this.__waiting_responses.AddOrUpdate(guid, taskSource, (x, y) => taskSource);
		this.__rest_client.EnqueueRequest(request, guid);

		HttpResponseMessage response = await taskSource.Task;

		return JsonSerializer.Deserialize<DiscordInvite[]>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Returns a list of all active integrations for this guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<DiscordGuildIntegration[]> GetGuildIntegrationsAsync(Int64 guildId)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.GetGuildIntegrationsAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Integrations}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Integrations}"),
			Token = this.__token,
			Method = HttpMethodEnum.Get
		};

		TaskCompletionSource<HttpResponseMessage> taskSource = new();

		_ = this.__waiting_responses.AddOrUpdate(guid, taskSource, (x, y) => taskSource);
		this.__rest_client.EnqueueRequest(request, guid);

		HttpResponseMessage response = await taskSource.Task;

		return JsonSerializer.Deserialize<DiscordGuildIntegration[]>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Deletes an integration from the given guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="integrationId">Snowflake identifier of the integration to be deleted.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <returns><see langword="true"/> if the deletion succeeded, <see langword="false"/> if otherwise.</returns>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<Boolean> DeleteGuildIntegrationAsync(Int64 guildId, Int64 integrationId, String? reason = null)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.DeleteGuildIntegrationAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Integrations}/{IntegrationId}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Integrations}/{integrationId}"),
			Token = this.__token,
			Method = HttpMethodEnum.Delete,
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
	/// Queries the guild widget settings for the specified guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild to be queried.</param>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<DiscordGuildWidgetSettings> GetGuildWidgetSettingsAsync(Int64 guildId)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.GetGuildWidgetSettingsAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Widget}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Widget}"),
			Token = this.__token,
			Method = HttpMethodEnum.Get
		};

		TaskCompletionSource<HttpResponseMessage> taskSource = new();

		_ = this.__waiting_responses.AddOrUpdate(guid, taskSource, (x, y) => taskSource);
		this.__rest_client.EnqueueRequest(request, guid);

		HttpResponseMessage response = await taskSource.Task;

		return JsonSerializer.Deserialize<DiscordGuildWidgetSettings>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Modifies the <see cref="DiscordGuildWidget"/> for the specified guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="settings">New settings for this guild widget.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <returns>The new guild widget object.</returns>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<DiscordGuildWidget> ModifyGuildWidgetSettingsAsync(Int64 guildId,
		DiscordGuildWidgetSettings settings, String? reason = null)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.ModifyGuildWidgetSettingsAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Widget}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Widget}"),
			Token = this.__token,
			Payload = JsonSerializer.Serialize(settings),
			Method = HttpMethodEnum.Patch,
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

		return JsonSerializer.Deserialize<DiscordGuildWidget>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Returns the guild widget for the specified guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier for the guild in question.</param>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<DiscordGuildWidget> GetGuildWidgetAsync(Int64 guildId)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.GetGuildWidgetAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{WidgetJson}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{WidgetJson}"),
			Token = this.__token,
			Method = HttpMethodEnum.Get
		};

		TaskCompletionSource<HttpResponseMessage> taskSource = new();

		_ = this.__waiting_responses.AddOrUpdate(guid, taskSource, (x, y) => taskSource);
		this.__rest_client.EnqueueRequest(request, guid);

		HttpResponseMessage response = await taskSource.Task;

		return JsonSerializer.Deserialize<DiscordGuildWidget>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Queries the vanity invite URL for this guild, if available.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<DiscordInvite> GetGuildVanityInviteAsync(Int64 guildId)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.GetGuildVanityInviteAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{VanityUrl}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{VanityUrl}"),
			Token = this.__token,
			Method = HttpMethodEnum.Get
		};

		TaskCompletionSource<HttpResponseMessage> taskSource = new();

		_ = this.__waiting_responses.AddOrUpdate(guid, taskSource, (x, y) => taskSource);
		this.__rest_client.EnqueueRequest(request, guid);

		HttpResponseMessage response = await taskSource.Task;

		return JsonSerializer.Deserialize<DiscordInvite>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Returns the guild widget image as a binary stream
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="style">Widget style, either "shield" (default) or "banner1" to "banner4".</param>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<Stream> GetGuildWidgetImageAsync(Int64 guildId, String style = "shield")
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.GetGuildWidgetImageAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{WidgetPng}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{WidgetPng}?style={style}"),
			Method = HttpMethodEnum.Get
		};

		TaskCompletionSource<HttpResponseMessage> taskSource = new();

		_ = this.__waiting_responses.AddOrUpdate(guid, taskSource, (x, y) => taskSource);
		this.__rest_client.EnqueueRequest(request, guid);

		HttpResponseMessage response = await taskSource.Task;

		return await response.Content.ReadAsStreamAsync();
	}

	/// <summary>
	/// Modifies the current user's stage voice state.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild everything takes place in.</param>
	/// <param name="payload">Stage voice state request payload.</param>
	/// <returns>Whether the request succeeded.</returns>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<Boolean> ModifyCurrentUserVoiceStateAsync(Int64 guildId, ModifyCurrentUserVoiceStateRequestPayload payload)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.ModifyCurrentUserVoiceStateAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{VoiceStates}/{Me}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{VoiceStates}/{Me}"),
			Token = this.__token,
			Payload = JsonSerializer.Serialize(payload),
			Method = HttpMethodEnum.Patch
		};

		TaskCompletionSource<HttpResponseMessage> taskSource = new();

		_ = this.__waiting_responses.AddOrUpdate(guid, taskSource, (x, y) => taskSource);
		this.__rest_client.EnqueueRequest(request, guid);

		HttpResponseMessage response = await taskSource.Task;

		return response.StatusCode == HttpStatusCode.NoContent;
	}

	/// <summary>
	/// Modifies another user's stage voice state.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild everything takes place in.</param>
	/// <param name="userId">Snowflake identifier of the user whose voice state to modify.</param>
	/// <param name="payload">Stage voice state request payload.</param>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task ModifyUserVoiceStateAsync(Int64 guildId, Int64 userId, ModifyUserVoiceStateRequestPayload payload)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordGuildRestResource.ModifyUserVoiceStateAsync",
				"guild",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{VoiceStates}/{UserId}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{VoiceStates}/{userId}"),
			Token = this.__token,
			Payload = JsonSerializer.Serialize(payload),
			Method = HttpMethodEnum.Patch
		};

		TaskCompletionSource<HttpResponseMessage> taskSource = new();

		_ = this.__waiting_responses.AddOrUpdate(guid, taskSource, (x, y) => taskSource);
		this.__rest_client.EnqueueRequest(request, guid);

		HttpResponseMessage response = await taskSource.Task;
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
		$"/{Guilds}/{GuildId}/{Channels}",
		$"/{Guilds}/{GuildId}/{Threads}/{Active}",
		$"/{Guilds}/{GuildId}/{Members}/{UserId}",
		$"/{Guilds}/{GuildId}/{Members}",
		$"/{Guilds}/{GuildId}/{Members}/{Search}",
		$"/{Guilds}/{GuildId}/{Members}/{Me}",
		$"/{Guilds}/{GuildId}/{Members}/{UserId}/{Roles}/{RoleId}",
		$"/{Guilds}/{GuildId}/{Bans}",
		$"/{Guilds}/{GuildId}/{Bans}/{UserId}",
		$"/{Guilds}/{GuildId}/{Roles}",
		$"/{Guilds}/{GuildId}/{Roles}/{RoleId}",
		$"/{Guilds}/{GuildId}/{Prune}",
		$"/{Guilds}/{GuildId}/{Voice}",
		$"/{Guilds}/{GuildId}/{Invites}",
		$"/{Guilds}/{GuildId}/{Integrations}",
		$"/{Guilds}/{GuildId}/{Integrations}/{IntegrationId}",
		$"/{Guilds}/{GuildId}/{Widget}",
		$"/{Guilds}/{GuildId}/{WidgetJson}",
		$"/{Guilds}/{GuildId}/{VanityUrl}",
		$"/{Guilds}/{GuildId}/{WidgetPng}",
		$"/{Guilds}/{GuildId}/{VoiceStates}/{Me}",
		$"/{Guilds}/{GuildId}/{VoiceStates}/{UserId}"
	};
}

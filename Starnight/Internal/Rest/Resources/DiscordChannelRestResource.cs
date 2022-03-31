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
using Starnight.Internal.Rest.Payloads.Channel;

using HttpMethodEnum = HttpMethod;

using static DiscordApiConstants;
using Starnight.Internal.Entities.Messages;
using System.Threading.Channels;
using System.Text;
using System.Diagnostics.Metrics;

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

	/// <summary>
	/// Modifies a group DM channel with the given parameters.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the group DM in question.</param>
	/// <param name="payload">Payload object containing the modification parameters.</param>
	/// <returns>The modified channel object.</returns>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<DiscordChannel> ModifyChannelAsync(Int64 channelId, ModifyGroupDMRequestPayload payload)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordChannelRestResource.ModifyChannelAsync",
				"channel",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Channels}/{ChannelId}",
			Url = new($"{BaseUri}/{Channels}/{channelId}"),
			Token = this.__token,
			Payload = JsonSerializer.Serialize(payload),
			Method = HttpMethodEnum.Patch
		};

		TaskCompletionSource<HttpResponseMessage> taskSource = new();

		_ = this.__waiting_responses.AddOrUpdate(guid, taskSource, (x, y) => taskSource);
		this.__rest_client.EnqueueRequest(request, guid);

		HttpResponseMessage response = await taskSource.Task;

		return JsonSerializer.Deserialize<DiscordChannel>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Modifies a guild channel with the given parameters.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the channel in question.</param>
	/// <param name="payload">Payload object containing the modification parameters.</param>
	/// <param name="reason">Optional audit log reason for the edit.</param>
	/// <returns>The modified channel object.</returns>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<DiscordChannel> ModifyChannelAsync(Int64 channelId, ModifyGuildChannelRequestPayload payload, String? reason = null)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordChannelRestResource.ModifyChannelAsync",
				"channel",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Channels}/{ChannelId}",
			Url = new($"{BaseUri}/{Channels}/{channelId}"),
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

		return JsonSerializer.Deserialize<DiscordChannel>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Modifies a thread channel with the given parameters.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the channel in question.</param>
	/// <param name="payload">Payload object containing the modification parameters.</param>
	/// <param name="reason">Optional audit log reason for the edit.</param>
	/// <returns>The modified channel object.</returns>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<DiscordChannel> ModifyChannelAsync(Int64 channelId, ModifyThreadChannelRequestPayload payload, String? reason = null)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordChannelRestResource.ModifyChannelAsync",
				"channel",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Channels}/{ChannelId}",
			Url = new($"{BaseUri}/{Channels}/{channelId}"),
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

		return JsonSerializer.Deserialize<DiscordChannel>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Deletes a channel. Deleting guild channels cannot be undone. DM channels, however, cannot be deleted
	/// and are restored by opening a direct message channel again.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the channel in question.</param>
	/// <param name="reason">Optional audit log reason if this is a guild channel.</param>
	/// <returns>The associated channel object.</returns>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<DiscordChannel> DeleteChannelAsync(Int64 channelId, String? reason = null)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordChannelRestResource.DeleteChannelAsync",
				"channel",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Channels}/{ChannelId}",
			Url = new($"{BaseUri}/{Channels}/{channelId}"),
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

		return JsonSerializer.Deserialize<DiscordChannel>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Returns a set amount of messages, optionally before, after or around a certain message.
	/// </summary>
	/// <remarks>
	/// <c>around</c>, <c>before</c> and <c>after</c> are mutually exclusive. Only one may be passed. If multiple are passed,
	/// only the first one in the parameter list is respected, independent of the order they are passed in client code.
	/// </remarks>
	/// <param name="channelId">Snowflake identifier of the channel in question.</param>
	/// <param name="count">Maximum amount of messages to return.</param>
	/// <param name="around">Snowflake identifier of the center message of the requested block.</param>
	/// <param name="before">Snowflake identifier of the first older message than the requested block.</param>
	/// <param name="after">Snowflake identifier of the first newer message than the requested block.</param>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<DiscordMessage[]> GetChannelMessagesAsync(Int64 channelId, Int32 count,
		Int64? around = null, Int64? before = null, Int64? after = null)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordChannelRestResource.GetChannelMessagesAsync",
				"channel",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		StringBuilder queryBuilder = new();

		_ = queryBuilder.Append($"limit={count}");

		if(around != null)
		{
			_ = queryBuilder.Append($"&around={around}");
		}
		else if(before != null)
		{
			_ = queryBuilder.Append($"&before={before}");
		}
		else if(after != null)
		{
			_ = queryBuilder.Append($"&after={after}");
		}

		IRestRequest request = new RestRequest
		{
			Path = $"/{Channels}/{ChannelId}/{Messages}",
			Url = new($"{BaseUri}/{Channels}/{channelId}/{Messages}?{queryBuilder}"),
			Token = this.__token,
			Method = HttpMethodEnum.Get
		};

		TaskCompletionSource<HttpResponseMessage> taskSource = new();

		_ = this.__waiting_responses.AddOrUpdate(guid, taskSource, (x, y) => taskSource);
		this.__rest_client.EnqueueRequest(request, guid);

		HttpResponseMessage response = await taskSource.Task;

		return JsonSerializer.Deserialize<DiscordMessage[]>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Gets a message by snowflake identifier.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the message's parent channel.</param>
	/// <param name="messageId">Snowflake identifier of the message in question.</param>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<DiscordMessage> GetChannelMessageAsync(Int64 channelId, Int64 messageId)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordChannelRestResource.GetChannelMessageAsync",
				"channel",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Channels}/{ChannelId}/{Messages}/{MessageId}",
			Url = new($"{BaseUri}/{Channels}/{channelId}/{Messages}/{messageId}"),
			Token = this.__token,
			Method = HttpMethodEnum.Get
		};

		TaskCompletionSource<HttpResponseMessage> taskSource = new();

		_ = this.__waiting_responses.AddOrUpdate(guid, taskSource, (x, y) => taskSource);
		this.__rest_client.EnqueueRequest(request, guid);

		HttpResponseMessage response = await taskSource.Task;

		return JsonSerializer.Deserialize<DiscordMessage>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Creates a new message in a channel.
	/// </summary>
	/// <param name="channelId">snowflake identifier of the message's target channel.</param>
	/// <param name="payload">Message creation payload including potential attachment files.</param>
	/// <returns>The newly created message object.</returns>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<DiscordMessage> CreateMessageAsync(Int64 channelId, CreateMessageRequestPayload payload)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordChannelRestResource.CreateMessageAsync",
				"channel",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		String payloadBody = JsonSerializer.Serialize(payload);

		IRestRequest request =

			payload.Files == null ?

				new RestRequest
				{
					Path = $"/{Channels}/{ChannelId}/{Messages}",
					Url = new($"{BaseUri}/{Channels}/{channelId}/{Messages}"),
					Token = this.__token,
					Payload = payloadBody,
					Method = HttpMethodEnum.Post
				} :

				new MultipartRestRequest
				{
					Path = $"/{Channels}/{ChannelId}/{Messages}",
					Url = new($"{BaseUri}/{Channels}/{channelId}/{Messages}"),
					Token = this.__token,
					Payload = String.IsNullOrWhiteSpace(payloadBody)
						? new()
						: new()
						{
							["payload_json"] = payloadBody
						},
					Method = HttpMethodEnum.Post,
					Files = payload.Files.ToList()
				};

		TaskCompletionSource<HttpResponseMessage> taskSource = new();

		_ = this.__waiting_responses.AddOrUpdate(guid, taskSource, (x, y) => taskSource);
		this.__rest_client.EnqueueRequest(request, guid);

		HttpResponseMessage response = await taskSource.Task;

		return JsonSerializer.Deserialize<DiscordMessage>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Publishes a message in an announcement channel to following channels.
	/// </summary>
	/// <param name="channelId">Source announcement channel for this message.</param>
	/// <param name="messageId">Snowflake identifier of the message.</param>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<DiscordMessage> CrosspostMessageAsync(Int64 channelId, Int64 messageId)
	{
		if(DateTimeOffset.UtcNow < this.__allow_next_request_at)
		{
			throw new StarnightSharedRatelimitHitException(
				"Starnight.Internal.Rest.Resources.DiscordChannelRestResource.CrosspostMessageAsync",
				"channel",
				this.__allow_next_request_at);
		}

		Guid guid = Guid.NewGuid();

		IRestRequest request = new RestRequest
		{
			Path = $"/{Channels}/{ChannelId}/{Messages}/{MessageId}/{Crosspost}",
			Url = new($"{BaseUri}/{Channels}/{channelId}/{Messages}/{messageId}/{Crosspost}"),
			Token = this.__token,
			Method = HttpMethodEnum.Post
		};

		TaskCompletionSource<HttpResponseMessage> taskSource = new();

		_ = this.__waiting_responses.AddOrUpdate(guid, taskSource, (x, y) => taskSource);
		this.__rest_client.EnqueueRequest(request, guid);

		HttpResponseMessage response = await taskSource.Task;

		return JsonSerializer.Deserialize<DiscordMessage>(await response.Content.ReadAsStringAsync())!;
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

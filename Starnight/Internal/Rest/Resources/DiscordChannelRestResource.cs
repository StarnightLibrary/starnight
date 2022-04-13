namespace Starnight.Internal.Rest.Resources;

using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Memory;

using Starnight.Exceptions;
using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Channels.Threads;
using Starnight.Internal.Entities.Messages;
using Starnight.Internal.Entities.Users;
using Starnight.Internal.Rest.Payloads.Channel;

using static DiscordApiConstants;

using HttpMethodEnum = HttpMethod;

public class DiscordChannelRestResource : AbstractRestResource
{
	private readonly RestClient __rest_client;

	public DiscordChannelRestResource(RestClient client, IMemoryCache ratelimitBucketCache)
		: base(ratelimitBucketCache) => this.__rest_client = client;

	/// <summary>
	/// Returns a channel object for the given ID. If the channel is a thread channel, a
	/// <see cref="DiscordThreadMember"/> object is included in the returned channel.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the channel in question.</param>
	/// <exception cref="StarnightSharedRatelimitHitException">Thrown if the shared resource ratelimit is exceeded.</exception>
	public async Task<DiscordChannel> GetChannelAsync(Int64 channelId)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Channels}/{channelId}",
			Url = new($"{BaseUri}/{Channels}/{channelId}"),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

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
		IRestRequest request = new RestRequest
		{
			Path = $"/{Channels}/{channelId}",
			Url = new($"{BaseUri}/{Channels}/{channelId}"),
			Payload = JsonSerializer.Serialize(payload),
			Method = HttpMethodEnum.Patch,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

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
		IRestRequest request = new RestRequest
		{
			Path = $"/{Channels}/{channelId}",
			Url = new($"{BaseUri}/{Channels}/{channelId}"),
			Payload = JsonSerializer.Serialize(payload),
			Method = HttpMethodEnum.Patch,
			Headers = reason != null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

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
		IRestRequest request = new RestRequest
		{
			Path = $"/{Channels}/{channelId}",
			Url = new($"{BaseUri}/{Channels}/{channelId}"),
			Payload = JsonSerializer.Serialize(payload),
			Method = HttpMethodEnum.Patch,
			Headers = reason != null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

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
		IRestRequest request = new RestRequest
		{
			Path = $"/{Channels}/{channelId}",
			Url = new($"{BaseUri}/{Channels}/{channelId}"),
			Method = HttpMethodEnum.Delete,
			Headers = reason != null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

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
			Path = $"/{Channels}/{channelId}/{Messages}",
			Url = new($"{BaseUri}/{Channels}/{channelId}/{Messages}?{queryBuilder}"),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Messages}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

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
		IRestRequest request = new RestRequest
		{
			Path = $"/{Channels}/{channelId}/{Messages}/{MessageId}",
			Url = new($"{BaseUri}/{Channels}/{channelId}/{Messages}/{messageId}"),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Messages}/{MessageId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

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
		String payloadBody = JsonSerializer.Serialize(payload);

		IRestRequest request =

			payload.Files == null ?

				new RestRequest
				{
					Path = $"/{Channels}/{channelId}/{Messages}",
					Url = new($"{BaseUri}/{Channels}/{channelId}/{Messages}"),
					Payload = payloadBody,
					Method = HttpMethodEnum.Post,
					Context = new()
					{
						["endpoint"] = $"/{Channels}/{channelId}/{Messages}",
						["cache"] = this.RatelimitBucketCache,
						["exempt-from-global-limit"] = false
					}
				} :

				new MultipartRestRequest
				{
					Path = $"/{Channels}/{channelId}/{Messages}",
					Url = new($"{BaseUri}/{Channels}/{channelId}/{Messages}"),
					Payload = String.IsNullOrWhiteSpace(payloadBody)
						? new()
						: new()
						{
							["payload_json"] = payloadBody
						},
					Method = HttpMethodEnum.Post,
					Files = payload.Files.ToList(),
					Context = new()
					{
						["endpoint"] = $"/{Channels}/{channelId}/{MessageId}",
						["cache"] = this.RatelimitBucketCache,
						["exempt-from-global-limit"] = false
					}
				};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

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
		IRestRequest request = new RestRequest
		{
			Path = $"/{Channels}/{channelId}/{Messages}/{MessageId}/{Crosspost}",
			Url = new($"{BaseUri}/{Channels}/{channelId}/{Messages}/{messageId}/{Crosspost}"),
			Method = HttpMethodEnum.Post,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Messages}/{MessageId}/{Crosspost}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordMessage>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Creates a reaction with the given emote on the specified message.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the message's parent channel.</param>
	/// <param name="messageId">Snowflake identifier of the message in question.</param>
	/// <param name="emote">String representation of the emote.</param>
	/// <returns>Whether the reaction was added successfully.</returns>
	public async Task<Boolean> CreateReactionAsync(Int64 channelId, Int64 messageId, String emote)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Channels}/{channelId}/{Messages}/{MessageId}/{Reactions}/{Emote}/{Me}",
			Url = new($"{BaseUri}/{Channels}/{channelId}/{Messages}/{messageId}/{Reactions}/{emote}/{Me}"),
			Method = HttpMethodEnum.Put,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Messages}/{MessageId}/{Reactions}/{Emote}/{Me}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return response.StatusCode == HttpStatusCode.NoContent;
	}

	/// <summary>
	/// Deletes your own reaction with the specified emote on the specified message.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the message's parent channel.</param>
	/// <param name="messageId">Snowflake identifier of the message in question.</param>
	/// <param name="emote">String representation of the emote.</param>
	/// <returns>Whether the reaction was removed successfully.</returns>
	public async Task<Boolean> DeleteOwnReactionAsync(Int64 channelId, Int64 messageId, String emote)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Channels}/{channelId}/{Messages}/{MessageId}/{Reactions}/{Emote}/{Me}",
			Url = new($"{BaseUri}/{Channels}/{channelId}/{Messages}/{messageId}/{Reactions}/{emote}/{Me}"),
			Method = HttpMethodEnum.Delete,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Messages}/{MessageId}/{Reactions}/{Emote}/{Me}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return response.StatusCode == HttpStatusCode.NoContent;
	}

	/// <summary>
	/// Deletes the specified user's reaction with the specified emote on the specified message.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the message's parent channel.</param>
	/// <param name="messageId">Snowflake identifier of the message in question.</param>
	/// <param name="userId">Snowflake identifier of the user in question.</param>
	/// <param name="emote">String representation of the emote.</param>
	/// <returns>Whether the reaction was removed successfully.</returns>
	public async Task<Boolean> DeleteUserReactionAsync(Int64 channelId, Int64 messageId, Int64 userId, String emote)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Channels}/{channelId}/{Messages}/{MessageId}/{Reactions}/{Emote}/{UserId}",
			Url = new($"{BaseUri}/{Channels}/{channelId}/{Messages}/{messageId}/{Reactions}/{emote}/{userId}"),
			Method = HttpMethodEnum.Delete,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Messages}/{MessageId}/{Reactions}/{Emote}/{UserId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return response.StatusCode == HttpStatusCode.NoContent;
	}

	/// <summary>
	/// Gets a list of users that reacted with the given emote.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the message's parent channel.</param>
	/// <param name="messageId">Snowflake identifier of the message in question.</param>
	/// <param name="emote">String representation of the queried emote.</param>
	/// <param name="after">Specifies a minimum user ID to return from, to paginate queries.</param>
	/// <param name="limit">Maximum amount of users to return. Defaults to 25.</param>
	public async Task<DiscordUser[]> GetReactionsAsync(Int64 channelId, Int64 messageId, String emote,
		Int64? after = null, Int32? limit = null)
	{
		StringBuilder urlBuilder = new($"{BaseUri}/{Channels}/{channelId}/{Messages}/{messageId}/{Reactions}/{emote}");

		if(after != null && limit != null)
		{
			_ = urlBuilder.Append($"?after={after}&limit={limit}");
		}
		else
		{
			if(after != null)
			{
				_ = urlBuilder.Append($"?after={after}");
			}
			else if(limit != null)
			{
				_ = urlBuilder.Append($"?limit={limit}");
			}
		}

		IRestRequest request = new RestRequest
		{
			Path = $"/{Channels}/{channelId}/{Messages}/{MessageId}/{Reactions}/{Emote}",
			Url = new(urlBuilder.ToString()),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Messages}/{MessageId}/{Reactions}/{Emote}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordUser[]>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Deletes all reactions on the given message.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the message's parent channel.</param>
	/// <param name="messageId">Snowflake identifier of the message in question.</param>
	public async Task DeleteAllReactionsAsync(Int64 channelId, Int64 messageId)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Channels}/{channelId}/{Messages}/{MessageId}/{Reactions}",
			Url = new($"{BaseUri}/{Channels}/{channelId}/{Messages}/{messageId}/{Reactions}"),
			Method = HttpMethodEnum.Delete,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Messages}/{MessageId}/{Reactions}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		_ = await this.__rest_client.MakeRequestAsync(request);
	}
}

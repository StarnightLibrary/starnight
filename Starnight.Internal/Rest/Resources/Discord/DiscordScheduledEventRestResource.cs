namespace Starnight.Internal.Rest.Resources.Discord;

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using Starnight.Caching.Abstractions;
using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Rest.Payloads.ScheduledEvents;

using static DiscordApiConstants;

/// <inheritdoc cref="IDiscordScheduledEventRestResource"/>
public sealed class DiscordScheduledEventRestResource
	: AbstractRestResource, IDiscordScheduledEventRestResource
{
	private readonly RestClient __rest_client;

	/// <inheritdoc/>
	public DiscordScheduledEventRestResource
	(
		RestClient client,
		ICacheService cache
	)
		: base(cache)
		=> this.__rest_client = client;

	/// <inheritdoc/>
	public async ValueTask<IEnumerable<DiscordScheduledEvent>> ListScheduledEventsAsync
	(
		Int64 guildId,
		Boolean? withUserCount = null
	)
	{
		QueryBuilder builder = new($"{Guilds}/{guildId}/{ScheduledEvents}");

		_ = builder.AddParameter("with_user_count", withUserCount.ToString());

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{ScheduledEvents}",
			Url = builder.Build(),
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{ScheduledEvents}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<IEnumerable<DiscordScheduledEvent>>
			(await response.Content.ReadAsStringAsync(), StarnightInternalConstants.DefaultSerializerOptions)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordScheduledEvent> CreateScheduledEventAsync
	(
		Int64 guildId,
		CreateScheduledEventRequestPayload payload,
		String? reason = null
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{ScheduledEvents}",
			Url = $"/{Guilds}/{guildId}/{ScheduledEvents}",
			Method = HttpMethod.Post,
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{ScheduledEvents}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordScheduledEvent>
			(await response.Content.ReadAsStringAsync(), StarnightInternalConstants.DefaultSerializerOptions)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordScheduledEvent> GetScheduledEventAsync
	(
		Int64 guildId,
		Int64 eventId,
		Boolean? withUserCount = null
	)
	{
		QueryBuilder builder = new($"{Guilds}/{guildId}/{ScheduledEvents}/{eventId}");

		_ = builder.AddParameter("with_user_count", withUserCount.ToString());

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{ScheduledEvents}/{ScheduledEventId}",
			Url = builder.Build(),
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{ScheduledEvents}/{ScheduledEventId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordScheduledEvent>
			(await response.Content.ReadAsStringAsync(), StarnightInternalConstants.DefaultSerializerOptions)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordScheduledEvent> ModifyScheduledEventAsync
	(
		Int64 guildId,
		Int64 eventId,
		ModifyScheduledEventRequestPayload payload,
		String? reason = null
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{ScheduledEvents}/{ScheduledEventId}",
			Url = $"{Guilds}/{guildId}/{ScheduledEvents}/{eventId}",
			Payload = JsonSerializer.Serialize(payload, StarnightInternalConstants.DefaultSerializerOptions),
			Method = HttpMethod.Patch,
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{ScheduledEvents}/{ScheduledEventId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordScheduledEvent>
			(await response.Content.ReadAsStringAsync(), StarnightInternalConstants.DefaultSerializerOptions)!;
	}

	/// <inheritdoc/>
	public async ValueTask<Boolean> DeleteScheduledEventAsync
	(
		Int64 guildId,
		Int64 eventId
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{ScheduledEvents}/{ScheduledEventId}",
			Url = $"{Guilds}/{guildId}/{ScheduledEvents}/{eventId}",
			Method = HttpMethod.Delete,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{ScheduledEvents}/{ScheduledEventId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return response.StatusCode == HttpStatusCode.NoContent;
	}

	/// <inheritdoc/>
	public async ValueTask<IEnumerable<DiscordScheduledEventUser>> GetScheduledEventUsersAsync
	(
		Int64 guildId,
		Int64 eventId,
		Int32? limit = null,
		Boolean? withMemberObject = null,
		Int64? before = null,
		Int64? after = null
	)
	{
		QueryBuilder builder = new($"{Guilds}/{guildId}/{ScheduledEvents}/{eventId}/{Users}");

		_ = builder.AddParameter("limit", limit.ToString())
			.AddParameter("with_member", withMemberObject.ToString())
			.AddParameter("before", before.ToString())
			.AddParameter("after", after.ToString());

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{ScheduledEvents}/{ScheduledEventId}/{Users}",
			Url = builder.Build(),
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{ScheduledEvents}/{ScheduledEventId}/{Users}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<IEnumerable<DiscordScheduledEventUser>>
			(await response.Content.ReadAsStringAsync(), StarnightInternalConstants.DefaultSerializerOptions)!;
	}
}

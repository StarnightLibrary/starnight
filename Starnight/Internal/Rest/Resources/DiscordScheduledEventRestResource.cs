namespace Starnight.Internal.Rest.Resources;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Memory;

using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Rest.Payloads.ScheduledEvents;

using static DiscordApiConstants;

using HttpMethodEnum = HttpMethod;

/// <summary>
/// Represents a wrapper for all requests to the Guild Scheduled Event rest resource.
/// </summary>
public class DiscordScheduledEventRestResource : AbstractRestResource
{
	private readonly RestClient __rest_client;

	/// <inheritdoc/>
	public DiscordScheduledEventRestResource
	(
		RestClient client,
		IMemoryCache cache
	)
		: base(cache)
		=> this.__rest_client = client;

	/// <summary>
	/// Returns a list of scheduled events taking place in this guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="withUserCount">Whether the answer should include user counts.</param>
	public async ValueTask<IEnumerable<DiscordScheduledEvent>> ListScheduledEventsAsync
	(
		Int64 guildId,
		Boolean? withUserCount = null
	)
	{
		QueryBuilder builder = new($"{BaseUri}/{Guilds}/{guildId}/{ScheduledEvents}");

		_ = builder.AddParameter("with_user_count", withUserCount.ToString());

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{ScheduledEvents}",
			Url = builder.Build(),
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

	/// <summary>
	/// Creates a new scheduled event in the specified guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="payload">Event creation payload.</param>
	/// <param name="reason">Optional audit log reason</param>
	/// <returns>The newly created scheduled event.</returns>
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
			Url = new($"/{Guilds}/{guildId}/{ScheduledEvents}"),
			Method = HttpMethodEnum.Post,
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{ScheduledEvents}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordScheduledEvent>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Returns the requested scheduled event.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild this scheduled event takes place in.</param>
	/// <param name="eventId">Snowflake identifier of the scheduled event in qeustion.</param>
	/// <param name="withUserCount">Specifies whether the number of users subscribed to this event should be included.</param>
	public async ValueTask<DiscordScheduledEvent> GetScheduledEventAsync
	(
		Int64 guildId,
		Int64 eventId,
		Boolean? withUserCount = null
	)
	{
		QueryBuilder builder = new($"{BaseUri}/{Guilds}/{guildId}/{ScheduledEvents}/{eventId}");

		_ = builder.AddParameter("with_user_count", withUserCount.ToString());

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{ScheduledEvents}/{ScheduledEventId}",
			Url = builder.Build(),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{ScheduledEvents}/{ScheduledEventId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordScheduledEvent>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Modifies the given scheduled event.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild this event takes place in.</param>
	/// <param name="eventId">Snowflake identifier of the event to be modified.</param>
	/// <param name="payload">Request payload.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <returns>The newly modified scheduled event.</returns>
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
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{ScheduledEvents}/{eventId}"),
			Payload = JsonSerializer.Serialize(payload),
			Method = HttpMethodEnum.Patch,
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{ScheduledEvents}/{ScheduledEventId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordScheduledEvent>(await response.Content.ReadAsStringAsync())!;
	}
}

namespace Starnight.Internal.Rest.Resources.Discord;

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Starnight.Caching.Abstractions;
using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Entities.Users;
using Starnight.Internal.Rest.Payloads.Users;

using static DiscordApiConstants;

/// <inheritdoc cref="IDiscordUserRestResource"/>
public sealed class DiscordUserRestResource
	: AbstractRestResource, IDiscordUserRestResource
{
	private readonly RestClient __rest_client;

	/// <inheritdoc/>
	public DiscordUserRestResource
	(
		RestClient client,
		ICacheService cache
	)
		: base(cache)
		=> this.__rest_client = client;

	/// <inheritdoc/>
	public async ValueTask<DiscordUser> GetCurrentUserAsync
	(
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Users}/{Me}",
			Url = $"{Users}/{Me}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Users}/{Me}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<DiscordUser>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordUser> GetUserAsync
	(
		Int64 userId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Users}/{UserId}",
			Url = $"{Users}/{userId}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Users}/{UserId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<DiscordUser>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordUser> ModifyCurrentUserAsync
	(
		ModifyCurrentUserRequestPayload payload,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Users}/{Me}",
			Url = $"{Users}/{Me}",
			Method = HttpMethod.Get,
			Payload = JsonSerializer.Serialize
			(
				payload,
				StarnightInternalConstants.DefaultSerializerOptions
			),
			Context = new()
			{
				["endpoint"] = $"/{Users}/{Me}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<DiscordUser>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<IEnumerable<DiscordGuild>> GetCurrentUserGuildsAsync
	(
		Int64? before = null,
		Int64? after = null,
		Int32? limit = null,
		CancellationToken ct = default
	)
	{
		QueryBuilder builder = new
		(
			$"{Users}/{Me}/{Guilds}"
		);

		_ = builder.AddParameter
			(
				"before",
				before?.ToString()
			)
			.AddParameter
			(
				"after",
				after?.ToString()
			)
			.AddParameter
			(
				"limit",
				limit?.ToString()
			);

		IRestRequest request = new RestRequest
		{
			Path = $"/{Users}/{Me}/{Guilds}",
			Url = builder.Build(),
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Users}/{Me}/{Guilds}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<IEnumerable<DiscordGuild>>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordGuildMember> GetCurrentUserGuildMemberAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Users}/{Me}/{Guilds}/{GuildId}/{Member}",
			Url = $"{Users}/{Me}/{Guilds}/{guildId}/{Member}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Users}/{Me}/{Guilds}/{GuildId}/{Member}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<DiscordGuildMember>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<Boolean> LeaveGuildAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Users}/{Me}/{Guilds}/{GuildId}",
			Url = $"{Users}/{Me}/{Guilds}/{guildId}",
			Method = HttpMethod.Delete,
			Context = new()
			{
				["endpoint"] = $"/{Users}/{Me}/{Guilds}/{GuildId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return response.StatusCode == HttpStatusCode.NoContent;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordChannel> CreateDMAsync
	(
		Int64 recipientId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Users}/{Me}/{Channels}",
			Url = $"{Users}/{Me}/{Channels}",
			Method = HttpMethod.Post,
			Payload =
			$$"""
			{ "recipient_id": {{recipientId}} }
			""",
			Context = new()
			{
				["endpoint"] = $"/{Users}/{Me}/{Channels}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<DiscordChannel>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<IEnumerable<DiscordUserConnection>> GetUserConnectionsAsync
	(
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Users}/{Me}/{Connections}",
			Url = $"{Users}/{Me}/{Connections}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Users}/{Me}/{Connections}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<IEnumerable<DiscordUserConnection>>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}
}

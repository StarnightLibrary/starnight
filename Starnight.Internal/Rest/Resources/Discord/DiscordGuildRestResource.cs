namespace Starnight.Internal.Rest.Resources.Discord;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Starnight.Caching.Abstractions;
using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Entities.Guilds.Invites;
using Starnight.Internal.Entities.Voice;
using Starnight.Internal.Rest.Payloads.Guilds;

using static Starnight.Internal.DiscordApiConstants;

/// <inheritdoc cref="IDiscordGuildRestResource"/>
public sealed class DiscordGuildRestResource
	: AbstractRestResource, IDiscordGuildRestResource
{
	private readonly RestClient __rest_client;

	/// <inheritdoc/>
	public DiscordGuildRestResource
	(
		RestClient client,
		ICacheService cache
	)
		: base(cache)
		=> this.__rest_client = client;

	/// <inheritdoc/>
	public async ValueTask<DiscordGuild> GetGuildAsync
	(
		Int64 guildId,
		Boolean? withCounts = null,
		CancellationToken ct = default
	)
	{
		QueryBuilder builder = new
		(
			$"{Guilds}/{guildId}"
		);

		_ = builder.AddParameter
		(
			"with_counts",
			withCounts.ToString()
		);

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}",
			Url = builder.Build(),
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}",
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

		return JsonSerializer.Deserialize<DiscordGuild>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordGuildPreview> GetGuildPreviewAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}/{Preview}",
			Url = $"{Guilds}/{guildId}/{Preview}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Preview}",
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

		return JsonSerializer.Deserialize<DiscordGuildPreview>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordGuild> ModifyGuildAsync
	(
		Int64 guildId,
		ModifyGuildRequestPayload payload,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}",
			Url = $"{Guilds}/{guildId}",
			Method = HttpMethod.Patch,
			Payload = JsonSerializer.Serialize(payload, StarnightInternalConstants.DefaultSerializerOptions),
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}",
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

		return JsonSerializer.Deserialize<DiscordGuild>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<Boolean> DeleteGuildAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}",
			Url = $"{Guilds}/{guildId}",
			Method = HttpMethod.Delete,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}",
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
	public async ValueTask<IEnumerable<DiscordChannel>> GetGuildChannelsAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}/{Channels}",
			Url = $"{Guilds}/{guildId}/{Channels}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Channels}",
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

		return JsonSerializer.Deserialize<IEnumerable<DiscordChannel>>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordChannel> CreateGuildChannelAsync
	(
		Int64 guildId,
		CreateGuildChannelRequestPayload payload,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}/{Channels}",
			Url = $"{Guilds}/{guildId}/{Channels}",
			Method = HttpMethod.Post,
			Payload = JsonSerializer.Serialize
			(
				payload,
				StarnightInternalConstants.DefaultSerializerOptions
			),
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Channels}",
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
	public async ValueTask<Boolean> ModifyGuildChannelPositionsAsync
	(
		Int64 guildId,
		IEnumerable<ModifyGuildChannelPositionRequestPayload> payload,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}/{Channels}",
			Url = $"{Guilds}/{guildId}/{Channels}",
			Method = HttpMethod.Patch,
			Payload = JsonSerializer.Serialize
			(
				payload,
				StarnightInternalConstants.DefaultSerializerOptions
			),
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Channels}",
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
	public async ValueTask<ListActiveThreadsResponsePayload> ListActiveThreadsAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}/{Threads}/{Active}",
			Url = $"{Guilds}/{guildId}/{Threads}/{Active}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Threads}/{Active}",
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

		return JsonSerializer.Deserialize<ListActiveThreadsResponsePayload>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordGuildMember> GetGuildMemberAsync
	(
		Int64 guildId,
		Int64 userId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}/{Members}/{UserId}",
			Url = $"{Guilds}/{guildId}/{Members}/{userId}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Members}/{UserId}",
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

	///<inheritdoc/>
	public async ValueTask<IEnumerable<DiscordGuildMember>> ListGuildMembersAsync
	(
		Int64 guildId,
		Int32? limit = null,
		Int64? afterUserId = null,
		CancellationToken ct = default
	)
	{
		QueryBuilder builder = new
		(
			$"{Guilds}/{guildId}/{Members}"
		);

		_ = builder.AddParameter
			(
				"limit",
				limit.ToString()
			)
			.AddParameter
			(
				"after",
				afterUserId.ToString()
			);

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}/{Members}",
			Url = builder.Build(),
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Members}",
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

		return JsonSerializer.Deserialize<IEnumerable<DiscordGuildMember>>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<IEnumerable<DiscordGuildMember>> SearchGuildMembersAsync
	(
		Int64 guildId,
		String query,
		Int32? limit = null,
		CancellationToken ct = default
	)
	{
		QueryBuilder builder = new
		(
			$"{Guilds}/{guildId}/{Members}"
		);

		_ = builder.AddParameter
			(
				"query",
				query
			)
			.AddParameter
			(
				"limit",
				limit.ToString()
			);

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}/{Members}/{Search}",
			Url = builder.Build(),
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Members}/{Search}",
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

		return JsonSerializer.Deserialize<IEnumerable<DiscordGuildMember>>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordGuildMember?> AddGuildMemberAsync
	(
		Int64 guildId,
		Int64 userId,
		AddGuildMemberRequestPayload payload,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}/{Members}/{UserId}",
			Url = $"{Guilds}/{guildId}/{Members}/{userId}",
			Method = HttpMethod.Put,
			Payload = JsonSerializer.Serialize
			(
				payload,
				StarnightInternalConstants.DefaultSerializerOptions
			),
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Members}/{UserId}",
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

		return response.StatusCode == HttpStatusCode.Created
			? JsonSerializer.Deserialize<DiscordGuildMember>
			  (
			  	  await response.Content.ReadAsStringAsync
			  	  (
			  	  	  ct
			  	  ),
			  	  StarnightInternalConstants.DefaultSerializerOptions
			  )
			: null;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordGuildMember> ModifyGuildMemberAsync
	(
		Int64 guildId,
		Int64 userId,
		ModifyGuildMemberRequestPayload payload,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}/{Members}/{UserId}",
			Url = $"{Guilds}/{guildId}/{Members}/{userId}",
			Method = HttpMethod.Patch,
			Payload = JsonSerializer.Serialize
			(
				payload,
				StarnightInternalConstants.DefaultSerializerOptions
			),
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Members}/{UserId}",
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
	public async ValueTask<DiscordGuildMember> ModifyCurrentMemberAsync
	(
		Int64 guildId,
		String nickname,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}/{Members}/{Me}",
			Url = $"{Guilds}/{guildId}/{Members}/{Me}",
			Method = HttpMethod.Patch,
			Payload =
			$$"""
			{ "nick": "{{nickname}}" }
			""",
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Members}/{Me}",
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
	public async ValueTask<Boolean> AddGuildMemberRoleAsync
	(
		Int64 guildId,
		Int64 userId,
		Int64 roleId,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}/{Members}/{UserId}/{Roles}/{RoleId}",
			Url = $"{Guilds}/{guildId}/{Members}/{userId}/{Roles}/{roleId}",
			Method = HttpMethod.Put,
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Members}/{UserId}/{Roles}/{RoleId}",
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
	public async ValueTask<Boolean> RemoveGuildMemberRoleAsync
	(
		Int64 guildId,
		Int64 userId,
		Int64 roleId,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}/{Members}/{UserId}/{Roles}/{RoleId}",
			Url = $"{Guilds}/{guildId}/{Members}/{userId}/{Roles}/{roleId}",
			Method = HttpMethod.Delete,
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Members}/{UserId}/{Roles}/{RoleId}",
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
	public async ValueTask<Boolean> RemoveGuildMemberAsync
	(
		Int64 guildId,
		Int64 userId,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}/{Members}/{UserId}",
			Url = $"{Guilds}/{guildId}/{Members}/{userId}",
			Method = HttpMethod.Delete,
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Members}/{UserId}",
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
	public async ValueTask<IEnumerable<DiscordGuildBan>> GetGuildBansAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}/{Bans}",
			Url = $"{Guilds}/{guildId}/{Bans}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Bans}",
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

		return JsonSerializer.Deserialize<IEnumerable<DiscordGuildBan>>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordGuildBan> GetGuildBanAsync
	(
		Int64 guildId,
		Int64 userId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Bans}/{UserId}",
			Url = $"{Guilds}/{guildId}/{Bans}/{userId}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Bans}/{UserId}",
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

		return JsonSerializer.Deserialize<DiscordGuildBan>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask BanMemberAsync
	(
		Int64 guildId,
		Int64 userId,
		Int32 deleteMessageDays = 0,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}/{Bans}/{UserId}",
			Url = $"{Guilds}/{guildId}/{Bans}/{userId}",
			Method = HttpMethod.Put,
			Payload =
			$$"""
			{ "delete_message_days": "{{deleteMessageDays}}" }
			""",
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Bans}/{UserId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		_ = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);
	}

	/// <inheritdoc/>
	public async ValueTask<Boolean> UnbanMemberAsync
	(
		Int64 guildId,
		Int64 userId,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}/{Bans}/{UserId}",
			Url = $"{Guilds}/{guildId}/{Bans}/{userId}",
			Method = HttpMethod.Delete,
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Bans}/{UserId}",
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
	public async ValueTask<IEnumerable<DiscordRole>> GetRolesAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}/{Roles}",
			Url = $"{Guilds}/{guildId}/{Roles}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Roles}",
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

		return JsonSerializer.Deserialize<IEnumerable<DiscordRole>>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordRole> CreateRoleAsync
	(
		Int64 guildId,
		CreateGuildRoleRequestPayload payload,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}/{Roles}",
			Url = $"{Guilds}/{guildId}/{Roles}",
			Payload = JsonSerializer.Serialize
			(
				payload,
				StarnightInternalConstants.DefaultSerializerOptions
			),
			Method = HttpMethod.Post,
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Roles}",
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

		return JsonSerializer.Deserialize<DiscordRole>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<IEnumerable<DiscordRole>> ModifyRolePositionsAsync
	(
		Int64 guildId,
		IEnumerable<ModifyRolePositionRequestPayload> payload,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}/{Roles}",
			Url = $"{Guilds}/{guildId}/{Roles}",
			Payload = JsonSerializer.Serialize
			(
				payload,
				StarnightInternalConstants.DefaultSerializerOptions
			),
			Method = HttpMethod.Patch,
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Roles}",
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

		return JsonSerializer.Deserialize<IEnumerable<DiscordRole>>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordRole> ModifyRoleAsync
	(
		Int64 guildId,
		Int64 roleId,
		ModifyGuildRoleRequestPayload payload,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}/{Roles}/{RoleId}",
			Url = $"{Guilds}/{guildId}/{Roles}/{roleId}",
			Payload = JsonSerializer.Serialize
			(
				payload,
				StarnightInternalConstants.DefaultSerializerOptions
			),
			Method = HttpMethod.Patch,
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Roles}/{RoleId}",
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

		return JsonSerializer.Deserialize<DiscordRole>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordGuildMultiFactorAuthLevel> ModifyGuildMFALevelAsync
	(
		Int64 guildId,
		DiscordGuildMultiFactorAuthLevel level,
		String? reason,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}/{MFA}",
			Url = $"{Guilds}/{guildId}/{MFA}",
			Payload = $$"""
			{ "level" : {{(Int32)level}} }
			""",
			Method = HttpMethod.Post,
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{MFA}",
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

		return JsonSerializer.Deserialize<DiscordGuildMultiFactorAuthLevel>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<Boolean> DeleteRoleAsync
	(
		Int64 guildId,
		Int64 roleId,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}/{Roles}/{RoleId}",
			Url = $"{Guilds}/{guildId}/{Roles}/{roleId}",
			Method = HttpMethod.Delete,
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Roles}/{RoleId}",
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
	public async ValueTask<Int32> GetGuildPruneCountAsync
	(
		Int64 guildId,
		Int32? days = null,
		String? roles = null,
		CancellationToken ct = default
	)
	{
		QueryBuilder builder = new
		(
			$"{Guilds}/{guildId}/{Prune}"
		);

		_ = builder.AddParameter
			(
				"days",
				days.ToString()
			)
			.AddParameter
			(
				"include_roles",
				roles
			);

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}/{Prune}",
			Url = builder.Build(),
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Prune}",
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

		return JsonDocument
			.Parse
			(
				await response.Content.ReadAsStringAsync
				(
					ct
				)
			)
			.RootElement
			.GetProperty
			(
				"pruned"
			)
			.GetInt32();
	}

	/// <inheritdoc/>
	public async ValueTask<Int32?> BeginGuildPruneAsync
	(
		Int64 guildId,
		Int32? days = null,
		String? roles = null,
		Boolean? computeCount = null,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		QueryBuilder builder = new
		(
			$"{Guilds}/{guildId}/{Prune}"
		);

		_ = builder.AddParameter
			(
				"days",
				days.ToString()
			)
			.AddParameter
			(
				"include_roles",
				roles
			)
			.AddParameter
			(
				"compute_prune_count",
				computeCount.ToString()
			);

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}/{Prune}",
			Url = builder.Build(),
			Method = HttpMethod.Post,
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Prune}",
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

		return computeCount == true
			? JsonDocument.Parse
			  (
			  	  await response.Content.ReadAsStringAsync
			  	  (
			  	  	  ct
			  	  )
			  )
			  .RootElement
			  .GetProperty
			  (
			  	  "pruned"
			  )
			  .GetInt32()
			: null;
	}

	/// <inheritdoc/>
	public async ValueTask<IEnumerable<DiscordVoiceRegion>> GetGuildVoiceRegionsAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}/{Voice}",
			Url = $"{Guilds}/{guildId}/{Voice}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Voice}",
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

		return JsonSerializer.Deserialize<IEnumerable<DiscordVoiceRegion>>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<IEnumerable<DiscordInvite>> GetGuildInvitesAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}/{Invites}",
			Url = $"{Guilds}/{guildId}/{Invites}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Invites}",
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

		return JsonSerializer.Deserialize<IEnumerable<DiscordInvite>>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<IEnumerable<DiscordGuildIntegration>> GetGuildIntegrationsAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}/{Integrations}",
			Url = $"{Guilds}/{guildId}/{Integrations}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Integrations}",
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

		return JsonSerializer.Deserialize<IEnumerable<DiscordGuildIntegration>>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	///<inheritdoc/>
	public async ValueTask<Boolean> DeleteGuildIntegrationAsync
	(
		Int64 guildId,
		Int64 integrationId,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}/{Integrations}/{IntegrationId}",
			Url = $"{Guilds}/{guildId}/{Integrations}/{integrationId}",
			Method = HttpMethod.Delete,
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Integrations}/{IntegrationId}",
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
	public async ValueTask<DiscordGuildWidgetSettings> GetGuildWidgetSettingsAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}/{Widget}",
			Url = $"{Guilds}/{guildId}/{Widget}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Widget}",
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

		return JsonSerializer.Deserialize<DiscordGuildWidgetSettings>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordGuildWidget> ModifyGuildWidgetSettingsAsync
	(
		Int64 guildId,
		DiscordGuildWidgetSettings settings,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}/{Widget}",
			Url = $"{Guilds}/{guildId}/{Widget}",
			Payload = JsonSerializer.Serialize
			(
				settings,
				StarnightInternalConstants.DefaultSerializerOptions
			),
			Method = HttpMethod.Patch,
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Widget}",
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

		return JsonSerializer.Deserialize<DiscordGuildWidget>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordGuildWidget> GetGuildWidgetAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}/{WidgetJson}",
			Url = $"{Guilds}/{guildId}/{WidgetJson}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{WidgetJson}",
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

		return JsonSerializer.Deserialize<DiscordGuildWidget>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordInvite> GetGuildVanityInviteAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}/{VanityUrl}",
			Url = $"{Guilds}/{guildId}/{VanityUrl}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{VanityUrl}",
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

		return JsonSerializer.Deserialize<DiscordInvite>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<Stream> GetGuildWidgetImageAsync
	(
		Int64 guildId,
		String? style = null,
		CancellationToken ct = default
	)
	{
		QueryBuilder builder = new
		(
			$"{Guilds}/{guildId}/{WidgetPng}"
		);

		_ = builder.AddParameter
		(
			"style",
			style
		);

		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}/{WidgetPng}",
			Url = builder.Build(),
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{WidgetPng}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = true,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return await response.Content.ReadAsStreamAsync
		(
			ct
		);
	}

	/// <inheritdoc/>
	public async ValueTask<Boolean> ModifyCurrentUserVoiceStateAsync
	(
		Int64 guildId,
		ModifyCurrentUserVoiceStateRequestPayload payload,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}/{VoiceStates}/{Me}",
			Url = $"{Guilds}/{guildId}/{VoiceStates}/{Me}",
			Payload = JsonSerializer.Serialize
			(
				payload,
				StarnightInternalConstants.DefaultSerializerOptions
			),
			Method = HttpMethod.Patch,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{VoiceStates}/{Me}",
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
	public async ValueTask ModifyUserVoiceStateAsync
	(
		Int64 guildId,
		Int64 userId,
		ModifyUserVoiceStateRequestPayload payload,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{guildId}/{VoiceStates}/{UserId}",
			Url = $"{Guilds}/{guildId}/{VoiceStates}/{userId}",
			Payload = JsonSerializer.Serialize
			(
				payload,
				StarnightInternalConstants.DefaultSerializerOptions
			),
			Method = HttpMethod.Patch,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{VoiceStates}/{UserId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		_ = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);
	}
}

namespace Starnight.Internal.Rest.Resources.Discord;

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using Starnight.Caching.Abstractions;
using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Rest.Payloads.Emojis;

using static DiscordApiConstants;

using HttpMethodEnum = HttpMethod;

/// <summary>
/// Represents a wrapper for all requests to the emoji rest resource.
/// </summary>
public class DiscordEmojiRestResource : AbstractRestResource, IDiscordEmojiRestResource
{
	private readonly RestClient __rest_client;

	/// <inheritdoc/>
	public DiscordEmojiRestResource
	(
		RestClient client,
		ICacheService cache
	)
		: base(cache)
		=> this.__rest_client = client;

	/// <inheritdoc/>
	public async ValueTask<IEnumerable<DiscordEmoji>> ListGuildEmojisAsync
	(
		Int64 guildId
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Emojis}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Emojis}"),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Emojis}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<IEnumerable<DiscordEmoji>>(await response.Content.ReadAsStringAsync())!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordEmoji> GetGuildEmojiAsync
	(
		Int64 guildId,
		Int64 emojiId
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Emojis}/{EmojiId}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Emojis}/{emojiId}"),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Emojis}/{EmojiId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordEmoji>(await response.Content.ReadAsStringAsync())!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordEmoji> CreateGuildEmojiAsync
	(
		Int64 guildId,
		CreateGuildEmojiRequestPayload payload,
		String? reason = null
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Emojis}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Emojis}"),
			Payload = JsonSerializer.Serialize(payload),
			Method = HttpMethodEnum.Post,
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Emojis}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordEmoji>(await response.Content.ReadAsStringAsync())!;
	}

	///<inheritdoc/>
	public async ValueTask<DiscordEmoji> ModifyGuildEmojiAsync
	(
		Int64 guildId,
		Int64 emojiId,
		ModifyGuildEmojiRequestPayload payload,
		String? reason = null
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Emojis}/{EmojiId}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Emojis}/{emojiId}"),
			Payload = JsonSerializer.Serialize(payload),
			Method = HttpMethodEnum.Patch,
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Emojis}/{EmojiId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordEmoji>(await response.Content.ReadAsStringAsync())!;
	}

	/// <inheritdoc/>
	public async ValueTask<Boolean> DeleteGuildEmojiAsync
	(
		Int64 guildId,
		Int64 emojiId,
		String? reason = null
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Emojis}/{EmojiId}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Emojis}/{emojiId}"),
			Method = HttpMethodEnum.Delete,
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Emojis}/{EmojiId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return response.StatusCode == HttpStatusCode.NoContent;
	}
}

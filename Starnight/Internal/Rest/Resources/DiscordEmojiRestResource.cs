namespace Starnight.Internal.Rest.Resources;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Memory;

using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Rest.Payloads.Emojis;

using static DiscordApiConstants;

using HttpMethodEnum = Starnight.Internal.Rest.HttpMethod;

/// <summary>
/// Represents a wrapper for all requests to the emoji rest resource.
/// </summary>
public class DiscordEmojiRestResource : AbstractRestResource
{
	private readonly RestClient __rest_client;

	public DiscordEmojiRestResource(RestClient client, IMemoryCache cache)
		: base(cache) => this.__rest_client = client;

	/// <summary>
	/// Fetches a list of emojis for the given guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	public async ValueTask<IEnumerable<DiscordEmoji>> ListGuildEmojisAsync(Int64 guildId)
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
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<IEnumerable<DiscordEmoji>>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Returns the specified emoji.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild owning the emoji.</param>
	/// <param name="emojiId">Snowflake identifier of the emoji in question.</param>
	public async ValueTask<DiscordEmoji> GetGuildEmojiAsync(Int64 guildId, Int64 emojiId)
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
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordEmoji>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Creates a new guild emoji in the specified guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="payload">Payload for this request.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <returns>The newly created emoji.</returns>
	public async ValueTask<DiscordEmoji> CreateGuildEmojiAsync(Int64 guildId, CreateGuildEmojiRequestPayload payload,
		String? reason = null)
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
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordEmoji>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Modifies the given emoji.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild owning the emoji.</param>
	/// <param name="emojiId">Snowflake identifier of the emoji in question.</param>
	/// <param name="payload">Payload for this request.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <returns>The newly updated emoji.</returns>
	public async ValueTask<DiscordEmoji> ModifyGuildEmojiAsync(Int64 guildId, Int64 emojiId,
		ModifyGuildEmojiRequestPayload payload, String? reason = null)
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
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordEmoji>(await response.Content.ReadAsStringAsync())!;
	}
}

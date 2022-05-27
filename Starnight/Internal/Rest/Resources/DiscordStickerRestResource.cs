namespace Starnight.Internal.Rest.Resources;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Memory;

using Starnight.Internal.Entities.Stickers;
using Starnight.Internal.Rest.Payloads.Stickers;

using static DiscordApiConstants;

using HttpMethodEnum = HttpMethod;

/// <summary>
/// Represents a wrapper for all requests to discord's Sticker rest resource.
/// </summary>
public class DiscordStickerRestResource : AbstractRestResource
{
	private readonly RestClient __rest_client;

	public DiscordStickerRestResource
	(
		RestClient client,
		IMemoryCache cache
	)
		: base(cache)
		=> this.__rest_client = client;

	/// <summary>
	/// Returns the given sticker object.
	/// </summary>
	/// <param name="stickerId">Snowflake identifier of the sticker in question.</param>
	public async ValueTask<DiscordSticker> GetStickerAsync
	(
		Int64 stickerId
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Stickers}/{StickerId}",
			Url = new($"{BaseUri}/{Stickers}/{stickerId}"),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Stickers}/{StickerId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordSticker>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Returns the list of sticker packs available to nitro subscribers.
	/// </summary>
	public async ValueTask<ListNitroStickerPacksResponsePayload> ListNitroStickerPacksAsync()
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{StickerPacks}",
			Url = new($"{BaseUri}/{StickerPacks}"),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{StickerPacks}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<ListNitroStickerPacksResponsePayload>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Returns the sticker objects for the given guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	public async ValueTask<IEnumerable<DiscordSticker>> ListGuildStickersAsync
	(
		Int64 guildId
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Stickers}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Stickers}"),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Stickers}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<IEnumerable<DiscordSticker>>(await response.Content.ReadAsStringAsync())!;
	}
}

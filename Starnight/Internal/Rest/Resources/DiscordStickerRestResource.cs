namespace Starnight.Internal.Rest.Resources;

using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Memory;

using Starnight.Internal.Entities.Stickers;

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
			Path = $"/{StageInstances}/{StickerId}",
			Url = new($"{BaseUri}/{StageInstances}/{stickerId}"),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{StageInstances}/{StickerId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordSticker>(await response.Content.ReadAsStringAsync())!;
	}
}

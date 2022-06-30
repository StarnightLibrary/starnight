namespace Starnight.Internal.Rest.Resources;

using System;
using System.Buffers;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Starnight.Caching.Abstractions;
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
		ICacheService cache
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

	/// <summary>
	/// Returns the specified guild sticker.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild owning this sticker.</param>
	/// <param name="stickerId">Snowflake identifier of the sticker in question.</param>
	public async ValueTask<DiscordSticker> GetGuildStickerAsync
	(
		Int64 guildId,
		Int64 stickerId
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Stickers}/{StickerId}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Stickers}/{stickerId}"),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Stickers}/{StickerId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordSticker>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Creates a sticker in the specified guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="payload">Request payload, containing the raw, unencoded image.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <returns>The newly created sticker object.</returns>
	/// <exception cref="ArgumentException">Thrown if the file could not be encoded to base64 for any reason.</exception>
	public async ValueTask<DiscordSticker> CreateGuildStickerAsync
	(
		Int64 guildId,
		CreateGuildStickerRequestPayload payload,
		String? reason = null
	)
	{
		Memory<Byte> fileContent = new Byte[Base64.GetMaxEncodedToUtf8Length(payload.File.Length)];

		OperationStatus encodingStatus = Base64.EncodeToUtf8(payload.File.Span, fileContent.Span, out Int32 _, out Int32 _);

		if(encodingStatus != OperationStatus.Done)
		{
#pragma warning disable CA2208 // we do in fact want to pass payload.File, not a method parameter
			throw new ArgumentException($"Could not encode sticker to base64: {encodingStatus}", nameof(payload.File));
#pragma warning restore CA2208
		}

		IRestRequest request = new MultipartRestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Stickers}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Stickers}"),
			Method = HttpMethodEnum.Post,
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Payload = new()
			{
				["name"] = payload.Name,
				["description"] = payload.Description,
				["tags"] = payload.Tags,
				["file"] = Encoding.UTF8.GetString(fileContent.Span)
			},
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Stickers}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordSticker>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Modifies the given sticker.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild owning the sticker.</param>
	/// <param name="stickerId">Snowflake identifier of the sticker in question.</param>
	/// <param name="payload">Request payload.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <returns>The newly updated sticker object.</returns>
	public async ValueTask<DiscordSticker> ModifyGuildStickerAsync
	(
		Int64 guildId,
		Int64 stickerId,
		ModifyGuildStickerRequestPayload payload,
		String? reason = null
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Stickers}/{StickerId}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Stickers}/{stickerId}"),
			Payload = JsonSerializer.Serialize(payload),
			Method = HttpMethodEnum.Patch,
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Stickers}/{StickerId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordSticker>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Deletes the specified sticker.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild owning the sticker.</param>
	/// <param name="stickerId">Snowflake identifier of the sticker in question.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <returns>Whether the deletion was successful.</returns>
	public async ValueTask<Boolean> DeleteGuildStickerAsync
	(
		Int64 guildId,
		Int64 stickerId,
		String? reason = null
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Guilds}/{GuildId}/{Stickers}/{StickerId}",
			Url = new($"{BaseUri}/{Guilds}/{guildId}/{Stickers}/{stickerId}"),
			Method = HttpMethodEnum.Delete,
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Stickers}/{StickerId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return response.StatusCode == HttpStatusCode.NoContent;
	}
}

namespace Starnight.Internal.Rest.Resources.Implementation;

using System;
using System.Buffers;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Starnight.Caching.Providers.Abstractions;
using Starnight.Internal.Entities.Stickers;
using Starnight.Internal.Rest.Payloads.Stickers;

using static DiscordApiConstants;

/// <inheritdoc cref="IDiscordStickerRestResource"/>
public sealed class DiscordStickerRestResource
	: AbstractRestResource, IDiscordStickerRestResource
{
	private readonly RestClient restClient;

	public DiscordStickerRestResource
	(
		RestClient client,
		ICacheProvider cache
	)
		: base(cache)
		=> this.restClient = client;

	/// <inheritdoc/>
	public async ValueTask<DiscordSticker> GetStickerAsync
	(
		Int64 stickerId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Stickers}/{stickerId}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Stickers}/{StickerId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.restClient.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<DiscordSticker>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<ListNitroStickerPacksResponsePayload> ListNitroStickerPacksAsync
	(
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{StickerPacks}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{StickerPacks}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.restClient.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<ListNitroStickerPacksResponsePayload>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<IEnumerable<DiscordSticker>> ListGuildStickersAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Guilds}/{guildId}/{Stickers}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Stickers}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.restClient.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<IEnumerable<DiscordSticker>>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordSticker> GetGuildStickerAsync
	(
		Int64 guildId,
		Int64 stickerId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Guilds}/{guildId}/{Stickers}/{stickerId}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Stickers}/{StickerId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.restClient.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<DiscordSticker>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordSticker> CreateGuildStickerAsync
	(
		Int64 guildId,
		CreateGuildStickerRequestPayload payload,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		Memory<Byte> fileContent = new Byte
		[
			Base64.GetMaxEncodedToUtf8Length
			(
				payload.File.Length
			)
		];

		OperationStatus encodingStatus = Base64.EncodeToUtf8
		(
			payload.File.Span,
			fileContent.Span,
			out Int32 _,
			out Int32 _
		);

#pragma warning disable CA2208 // we do in fact want to pass payload.File, not a method parameter
		if(encodingStatus != OperationStatus.Done)
		{
			throw new ArgumentException
			(
				$"Could not encode sticker to base64: {encodingStatus}",
				nameof(payload.File)
			);
		}
#pragma warning restore CA2208

		IRestRequest request = new MultipartRestRequest
		{
			Url = $"{Guilds}/{guildId}/{Stickers}",
			Method = HttpMethod.Post,
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
				["file"] = Encoding.UTF8.GetString
				(
					fileContent.Span
				)
			},
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Stickers}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.restClient.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<DiscordSticker>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordSticker> ModifyGuildStickerAsync
	(
		Int64 guildId,
		Int64 stickerId,
		ModifyGuildStickerRequestPayload payload,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Guilds}/{guildId}/{Stickers}/{stickerId}",
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
				["endpoint"] = $"/{Guilds}/{guildId}/{Stickers}/{StickerId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.restClient.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<DiscordSticker>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<Boolean> DeleteGuildStickerAsync
	(
		Int64 guildId,
		Int64 stickerId,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Guilds}/{guildId}/{Stickers}/{stickerId}",
			Method = HttpMethod.Delete,
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{Guilds}/{guildId}/{Stickers}/{StickerId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.restClient.MakeRequestAsync
		(
			request,
			ct
		);

		return response.StatusCode == HttpStatusCode.NoContent;
	}
}

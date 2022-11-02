namespace Starnight.Internal.Rest.Resources.Discord;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Starnight.Caching.Abstractions;
using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Channels.Threads;
using Starnight.Internal.Entities.Guilds.Invites;
using Starnight.Internal.Entities.Messages;
using Starnight.Internal.Entities.Users;
using Starnight.Internal.Rest.Payloads.Channels;

using static Starnight.Internal.DiscordApiConstants;

/// <inheritdoc cref="IDiscordChannelRestResource"/>
public sealed class DiscordChannelRestResource
	: AbstractRestResource, IDiscordChannelRestResource
{
	private readonly RestClient __rest_client;

	/// <inheritdoc/>
	public DiscordChannelRestResource
	(
		RestClient client,
		ICacheService cache
	)
		: base(cache)
		=> this.__rest_client = client;

	/// <inheritdoc/>
	public async ValueTask<DiscordChannel> GetChannelAsync
	(
		Int64 channelId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Channels}/{channelId}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}",
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
	public async ValueTask<DiscordChannel> ModifyChannelAsync
	(
		Int64 channelId,
		ModifyGroupDMRequestPayload payload,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Channels}/{channelId}",
			Payload = JsonSerializer.Serialize
			(
				payload,
				StarnightInternalConstants.DefaultSerializerOptions
			),
			Method = HttpMethod.Patch,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}",
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

	///<inheritdoc/>
	public async ValueTask<DiscordChannel> ModifyChannelAsync
	(
		Int64 channelId,
		ModifyGuildChannelRequestPayload payload,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Channels}/{channelId}",
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
				["endpoint"] = $"/{Channels}/{channelId}",
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
	public async ValueTask<DiscordChannel> ModifyChannelAsync
	(
		Int64 channelId,
		ModifyThreadChannelRequestPayload payload,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Channels}/{channelId}",
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
				["endpoint"] = $"/{Channels}/{channelId}",
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
	public async ValueTask<DiscordChannel> DeleteChannelAsync
	(
		Int64 channelId,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Channels}/{channelId}",
			Method = HttpMethod.Delete,
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}",
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
	public async ValueTask<IEnumerable<DiscordMessage>> GetChannelMessagesAsync
	(
		Int64 channelId,
		Int32 count,
		Int64? around = null,
		Int64? before = null,
		Int64? after = null,
		CancellationToken ct = default
	)
	{
		QueryBuilder builder = new
		(
			$"{Channels}/{channelId}/{Messages}"
		);

		_ = builder.AddParameter
			(
				"limit",
				count.ToString()
			)
			.AddParameter
			(
				"around",
				around.ToString()
			)
			.AddParameter
			(
				"before",
				before.ToString()
			)
			.AddParameter
			(
				"after",
				after.ToString()
			);

		IRestRequest request = new RestRequest
		{
			Url = builder.Build(),
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Messages}",
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

		return JsonSerializer.Deserialize<IEnumerable<DiscordMessage>>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordMessage> GetChannelMessageAsync
	(
		Int64 channelId,
		Int64 messageId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Channels}/{channelId}/{Messages}/{messageId}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Messages}/{MessageId}",
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

		return JsonSerializer.Deserialize<DiscordMessage>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordMessage> CreateMessageAsync
	(
		Int64 channelId,
		CreateMessageRequestPayload payload,
		CancellationToken ct = default
	)
	{
		String payloadBody = JsonSerializer.Serialize
		(
			payload,
			StarnightInternalConstants.DefaultSerializerOptions
		);

		IRestRequest request =

			payload.Files is null ?

				new RestRequest
				{
					Url = $"{Channels}/{channelId}/{Messages}",
					Payload = payloadBody,
					Method = HttpMethod.Post,
					Context = new()
					{
						["endpoint"] = $"/{Channels}/{channelId}/{Messages}",
						["cache"] = this.RatelimitBucketCache,
						["exempt-from-global-limit"] = false,
						["is-webhook-request"] = false
					}
				} :

				new MultipartRestRequest
				{
					Url = $"{Channels}/{channelId}/{Messages}",
					Payload = String.IsNullOrWhiteSpace(payloadBody)
						? new()
						: new()
						{
							["payload_json"] = payloadBody
						},
					Method = HttpMethod.Post,
					Files = payload.Files.ToList(),
					Context = new()
					{
						["endpoint"] = $"/{Channels}/{channelId}/{MessageId}",
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

		return JsonSerializer.Deserialize<DiscordMessage>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordMessage> CrosspostMessageAsync
	(
		Int64 channelId,
		Int64 messageId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Channels}/{channelId}/{Messages}/{messageId}/{Crosspost}",
			Method = HttpMethod.Post,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Messages}/{MessageId}/{Crosspost}",
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

		return JsonSerializer.Deserialize<DiscordMessage>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<Boolean> CreateReactionAsync
	(
		Int64 channelId,
		Int64 messageId,
		String emoji,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Channels}/{channelId}/{Messages}/{messageId}/{Reactions}/{emoji}/{Me}",
			Method = HttpMethod.Put,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Messages}/{MessageId}/{Reactions}/{Emoji}/{Me}",
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
	public async ValueTask<Boolean> DeleteOwnReactionAsync
	(
		Int64 channelId,
		Int64 messageId,
		String emoji,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Channels}/{channelId}/{Messages}/{messageId}/{Reactions}/{emoji}/{Me}",
			Method = HttpMethod.Delete,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Messages}/{MessageId}/{Reactions}/{Emoji}/{Me}",
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
	public async ValueTask<Boolean> DeleteUserReactionAsync
	(
		Int64 channelId,
		Int64 messageId,
		Int64 userId,
		String emoji,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Channels}/{channelId}/{Messages}/{messageId}/{Reactions}/{emoji}/{userId}",
			Method = HttpMethod.Delete,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Messages}/{MessageId}/{Reactions}/{Emoji}/{UserId}",
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
	public async ValueTask<IEnumerable<DiscordUser>> GetReactionsAsync
	(
		Int64 channelId,
		Int64 messageId,
		String emoji,
		Int64? after = null,
		Int32? limit = null,
		CancellationToken ct = default
	)
	{
		QueryBuilder builder = new
		(
			$"{Channels}/{channelId}/{Messages}/{messageId}/{Reactions}/{emoji}"
		);

		_ = builder.AddParameter
			(
				"after",
				after.ToString()
			)
			.AddParameter
			(
				"limit",
				limit.ToString()
			);

		IRestRequest request = new RestRequest
		{
			Url = builder.Build(),
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Messages}/{MessageId}/{Reactions}/{Emoji}",
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

		return JsonSerializer.Deserialize<IEnumerable<DiscordUser>>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask DeleteAllReactionsAsync
	(
		Int64 channelId,
		Int64 messageId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Channels}/{channelId}/{Messages}/{messageId}/{Reactions}",
			Method = HttpMethod.Delete,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Messages}/{MessageId}/{Reactions}",
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
	public async ValueTask DeleteEmojiReactionsAsync
	(
		Int64 channelId,
		Int64 messageId,
		String emoji,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Channels}/{channelId}/{Messages}/{messageId}/{Reactions}/{emoji}",
			Method = HttpMethod.Delete,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Messages}/{MessageId}/{Reactions}/{Emoji}",
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
	public async ValueTask<DiscordMessage> EditMessageAsync
	(
		Int64 channelId,
		Int64 messageId,
		EditMessageRequestPayload payload,
		CancellationToken ct = default
	)
	{
		String payloadBody = JsonSerializer.Serialize
		(
			payload,
			StarnightInternalConstants.DefaultSerializerOptions
		);

		IRestRequest request =

			payload.Files is null ?

				new RestRequest
				{
					Url = $"{Channels}/{channelId}/{Messages}",
					Payload = payloadBody,
					Method = HttpMethod.Patch,
					Context = new()
					{
						["endpoint"] = $"/{Channels}/{channelId}/{Messages}",
						["cache"] = this.RatelimitBucketCache,
						["exempt-from-global-limit"] = false,
						["is-webhook-request"] = false
					}
				} :

				new MultipartRestRequest
				{
					Url = $"{Channels}/{channelId}/{Messages}",
					Payload = String.IsNullOrWhiteSpace
					(
						payloadBody
					)
						? new()
						: new()
						{
							["payload_json"] = payloadBody
						},
					Method = HttpMethod.Patch,
					Files = payload.Files.ToList(),
					Context = new()
					{
						["endpoint"] = $"/{Channels}/{channelId}/{MessageId}",
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

		return JsonSerializer.Deserialize<DiscordMessage>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<Boolean> DeleteMessageAsync
	(
		Int64 channelId,
		Int64 messageId,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Channels}/{channelId}/{Messages}/{messageId}",
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Method = HttpMethod.Delete,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Messages}/{MessageId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return message.StatusCode == HttpStatusCode.NoContent;
	}

	/// <inheritdoc/>
	public async ValueTask<Boolean> BulkDeleteMessagesAsync
	(
		Int64 channelId,
		IEnumerable<Int64> messageIds,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Channels}/{channelId}/{Messages}/{BulkDelete}",
			Payload = JsonSerializer.Serialize
			(
				messageIds,
				StarnightInternalConstants.DefaultSerializerOptions
			),
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Method = HttpMethod.Delete,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Messages}/{BulkDelete}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return message.StatusCode == HttpStatusCode.NoContent;
	}

	/// <inheritdoc/>
	public async ValueTask<Boolean> EditChannelPermissionsAsync
	(
		Int64 channelId,
		Int64 overwriteId,
		EditChannelPermissionsRequestPayload payload,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Channels}/{channelId}/{Permissions}/{overwriteId}",
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
			Method = HttpMethod.Put,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Permissions}/{OverwriteId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return message.StatusCode == HttpStatusCode.NoContent;
	}

	/// <inheritdoc/>
	public async ValueTask<IEnumerable<DiscordInvite>> GetChannelInvitesAsync
	(
		Int64 channelId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Channels}/{channelId}/{Invites}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Invites}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<IEnumerable<DiscordInvite>>
		(
			await message.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordInvite> CreateChannelInviteAsync
	(
		Int64 channelId,
		CreateChannelInviteRequestPayload payload,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		String serializedPayload = JsonSerializer.Serialize
		(
			payload,
			StarnightInternalConstants.DefaultSerializerOptions
		);

		// always pass an empty json object
		if(String.IsNullOrWhiteSpace(serializedPayload))
		{
			serializedPayload = "{}";
		}

		IRestRequest request = new RestRequest
		{
			Url = $"{Channels}/{channelId}/{Invites}",
			Method = HttpMethod.Post,
			Payload = serializedPayload,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Invites}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<DiscordInvite>
		(
			await message.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<Boolean> DeleteChannelPermissionOverwriteAsync
	(
		Int64 channelId,
		Int64 overwriteId,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Channels}/{channelId}/{Permissions}/{overwriteId}",
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Method = HttpMethod.Delete,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Permissions}/{OverwriteId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return message.StatusCode == HttpStatusCode.NoContent;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordFollowedChannel> FollowNewsChannelAsync
	(
		Int64 channelId,
		Int64 targetChannelId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Channels}/{channelId}/{Followers}",
			Payload =
			$$"""
			{ "webhook_channel_id": "{{targetChannelId}}" }
			""",
			Method = HttpMethod.Post,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Followers}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<DiscordFollowedChannel>
		(
			await message.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask TriggerTypingIndicatorAsync
	(
		Int64 channelId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Channels}/{channelId}/{Typing}",
			Method = HttpMethod.Post,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Typing}",
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
	public async ValueTask<IEnumerable<DiscordMessage>> GetPinnedMessagesAsync
	(
		Int64 channelId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Channels}/{channelId}/{Pins}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Pins}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<IEnumerable<DiscordMessage>>
		(
			await message.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<Boolean> PinMessageAsync
	(
		Int64 channelId,
		Int64 messageId,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Channels}/{channelId}/{Pins}/{messageId}",
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Method = HttpMethod.Post,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Pins}/{MessageId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return message.StatusCode == HttpStatusCode.NoContent;
	}

	/// <inheritdoc/>
	public async ValueTask<Boolean> UnpinMessageAsync
	(
		Int64 channelId,
		Int64 messageId,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Channels}/{channelId}/{Pins}/{messageId}",
			Headers = reason is not null ? new()
			{
				["X-Audit-Log-Reason"] = reason
			}
			: new(),
			Method = HttpMethod.Delete,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Pins}/{MessageId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return message.StatusCode == HttpStatusCode.NoContent;
	}

	/// <inheritdoc/>
	public async ValueTask AddGroupDMRecipientAsync
	(
		Int64 channelId,
		Int64 userId,
		AddGroupDMRecipientRequestPayload payload,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Channels}/{channelId}/{Recipients}/{userId}",
			Payload = JsonSerializer.Serialize
			(
				payload,
				StarnightInternalConstants.DefaultSerializerOptions
			),
			Method = HttpMethod.Put,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Recipients}/{UserId}",
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
	public async ValueTask RemoveGroupDMRecipientAsync
	(
		Int64 channelId,
		Int64 userId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Channels}/{channelId}/{Recipients}/{userId}",
			Method = HttpMethod.Delete,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Recipients}/{UserId}",
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
	public async ValueTask<DiscordChannel> StartThreadFromMessageAsync
	(
		Int64 channelId,
		Int64 messageId,
		StartThreadFromMessageRequestPayload payload,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Channels}/{channelId}/{Messages}/{messageId}/{Threads}",
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
			Method = HttpMethod.Post,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Messages}/{MessageId}/{Threads}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<DiscordChannel>
		(
			await message.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordChannel> StartThreadWithoutMessageAsync
	(
		Int64 channelId,
		StartThreadWithoutMessageRequestPayload payload,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Channels}/{channelId}/{Threads}",
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
			Method = HttpMethod.Post,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Threads}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<DiscordChannel>
		(
			await message.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordChannel> StartThreadInForumChannelAsync
	(
		Int64 channelId,
		StartThreadInForumChannelRequestPayload payload,
		String? reason = null,
		CancellationToken ct = default
	)
	{
		String payloadBody = JsonSerializer.Serialize
		(
			payload,
			StarnightInternalConstants.DefaultSerializerOptions
		);

		IRestRequest request =

			payload.Files is null ?

				new RestRequest
				{
					Url = $"{Channels}/{channelId}/{Threads}",
					Payload = payloadBody,
					Method = HttpMethod.Post,
					Context = new()
					{
						["endpoint"] = $"/{Channels}/{channelId}/{Threads}",
						["cache"] = this.RatelimitBucketCache,
						["exempt-from-global-limit"] = false,
						["is-webhook-request"] = false
					}
				} :

				new MultipartRestRequest
				{
					Url = $"{Channels}/{channelId}/{Threads}",
					Payload = String.IsNullOrWhiteSpace
					(
						payloadBody
					)
						? new()
						: new()
						{
							["payload_json"] = payloadBody
						},
					Method = HttpMethod.Post,
					Files = payload.Files.ToList(),
					Context = new()
					{
						["endpoint"] = $"/{Channels}/{channelId}/{Threads}",
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
	public async ValueTask<Boolean> JoinThreadAsync
	(
		Int64 threadId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Channels}/{threadId}/{ThreadMembers}/{Me}",
			Method = HttpMethod.Put,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{threadId}/{ThreadMembers}/{Me}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return message.StatusCode == HttpStatusCode.NoContent;
	}

	/// <inheritdoc/>
	public async ValueTask<Boolean> AddToThreadAsync
	(
		Int64 threadId,
		Int64 userId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Channels}/{threadId}/{ThreadMembers}/{userId}",
			Method = HttpMethod.Put,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{threadId}/{ThreadMembers}/{UserId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return message.StatusCode == HttpStatusCode.NoContent;
	}

	/// <inheritdoc/>
	public async ValueTask<Boolean> LeaveThreadAsync
	(
		Int64 threadId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Channels}/{threadId}/{ThreadMembers}/{Me}",
			Method = HttpMethod.Delete,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{threadId}/{ThreadMembers}/{Me}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return message.StatusCode == HttpStatusCode.NoContent;
	}

	/// <inheritdoc/>
	public async ValueTask<Boolean> RemoveFromThreadAsync
	(
		Int64 threadId,
		Int64 userId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Channels}/{threadId}/{ThreadMembers}/{userId}",
			Method = HttpMethod.Delete,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{threadId}/{ThreadMembers}/{UserId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return message.StatusCode == HttpStatusCode.NoContent;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordThreadMember> GetThreadMemberAsync
	(
		Int64 threadId,
		Int64 userId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Channels}/{threadId}/{ThreadMembers}/{userId}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{threadId}/{ThreadMembers}/{UserId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<DiscordThreadMember>
		(
			await message.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<IEnumerable<DiscordThreadMember>> ListThreadMembersAsync
	(
		Int64 threadId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Channels}/{threadId}/{ThreadMembers}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{threadId}/{ThreadMembers}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<IEnumerable<DiscordThreadMember>>
		(
			await message.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<ListArchivedThreadsResponsePayload> ListPublicArchivedThreadsAsync
	(
		Int64 channelId,
		DateTimeOffset? before = null,
		Int32? limit = null,
		CancellationToken ct = default
	)
	{
		QueryBuilder builder = new
		(
			$"{Channels}/{channelId}/{Threads}/{Archived}/{Public}"
		);

		_ = builder.AddParameter
			(
				"before",
				before.ToString()
			)
			.AddParameter
			(
				"limit",
				limit.ToString()
			);

		IRestRequest request = new RestRequest
		{
			Url = builder.Build(),
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Threads}/{Archived}/{Public}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<ListArchivedThreadsResponsePayload>
		(
			await message.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<ListArchivedThreadsResponsePayload> ListPrivateArchivedThreadsAsync
	(
		Int64 channelId,
		DateTimeOffset? before = null,
		Int32? limit = null,
		CancellationToken ct = default
	)
	{
		QueryBuilder builder = new
		(
			$"{Channels}/{channelId}/{Threads}/{Archived}/{Private}"
		);

		_ = builder.AddParameter
			(
				"before",
				before.ToString()
			)
			.AddParameter
			(
				"limit",
				limit.ToString()
			);

		IRestRequest request = new RestRequest
		{
			Url = builder.Build(),
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Threads}/{Archived}/{Private}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<ListArchivedThreadsResponsePayload>
		(
			await message.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<ListArchivedThreadsResponsePayload> ListJoinedPrivateArchivedThreadsAsync
	(
		Int64 channelId,
		DateTimeOffset? before = null,
		Int32? limit = null,
		CancellationToken ct = default
	)
	{
		QueryBuilder builder = new
		(
			$"{Channels}/{channelId}/{Threads}/{Archived}/{Private}"
		);

		_ = builder.AddParameter
			(
				"before",
				before.ToString()
			)
			.AddParameter
			(
				"limit",
				limit.ToString()
			);

		IRestRequest request = new RestRequest
		{
			Url = builder.Build(),
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Channels}/{channelId}/{Users}/{Me}/{Threads}/{Archived}/{Private}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<ListArchivedThreadsResponsePayload>
		(
			await message.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}
}

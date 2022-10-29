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
using Starnight.Internal.Entities.Interactions.ApplicationCommands;
using Starnight.Internal.Entities.Messages;
using Starnight.Internal.Rest.Payloads.ApplicationCommands;

using static Starnight.Internal.DiscordApiConstants;

/// <inheritdoc cref="IDiscordApplicationCommandsRestResource"/>
public sealed class DiscordApplicationCommandsRestResource
	: AbstractRestResource, IDiscordApplicationCommandsRestResource
{
	private readonly RestClient __rest_client;

	/// <inheritdoc/>
	public DiscordApplicationCommandsRestResource
	(
		RestClient client,
		ICacheService cache
	)
		: base(cache)
		=> this.__rest_client = client;

	/// <inheritdoc/>
	public async ValueTask<IEnumerable<DiscordApplicationCommand>> GetGlobalApplicationCommandsAsync
	(
		Int64 applicationId,
		Boolean? withLocalizations = null,
		String? locale = null,
		CancellationToken ct = default
	)
	{
		QueryBuilder builder = new
		(
			$"{Channels}/{applicationId}/{Commands}"
		);

		_ = builder.AddParameter
		(
			"with_localizations",
			withLocalizations.ToString()
		);

		IRestRequest request = new RestRequest
		{
			Path = $"/{Applications}/{AppId}/{Commands}",
			Url = builder.Build(),
			Headers = locale is not null ? new()
			{
				["X-Discord-Locale"] = locale
			}
			: new(),
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Applications}/{AppId}/{Commands}",
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

		return JsonSerializer.Deserialize<IEnumerable<DiscordApplicationCommand>>
		(
			await message.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordApplicationCommand> CreateGlobalApplicationCommandAsync
	(
		Int64 applicationId,
		CreateApplicationCommandRequestPayload payload,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Applications}/{AppId}/{Commands}",
			Url = $"{Channels}/{applicationId}/{Commands}",
			Payload = JsonSerializer.Serialize
			(
				payload,
				StarnightInternalConstants.DefaultSerializerOptions
			),
			Method = HttpMethod.Post,
			Context = new()
			{
				["endpoint"] = $"/{Applications}/{AppId}/{Commands}",
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

		return JsonSerializer.Deserialize<DiscordApplicationCommand>
		(
			await message.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordApplicationCommand> GetGlobalApplicationCommandAsync
	(
		Int64 applicationId,
		Int64 commandId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Applications}/{AppId}/{Commands}/{CommandId}",
			Url = $"{Channels}/{applicationId}/{Commands}/{commandId}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Applications}/{AppId}/{Commands}/{CommandId}",
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

		return JsonSerializer.Deserialize<DiscordApplicationCommand>
		(
			await message.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordApplicationCommand> EditGlobalApplicationCommandAsync
	(
		Int64 applicationId,
		Int64 commandId,
		EditApplicationCommandRequestPayload payload,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Applications}/{AppId}/{Commands}/{CommandId}",
			Url = $"{Channels}/{applicationId}/{Commands}/{commandId}",
			Payload = JsonSerializer.Serialize
			(
				payload,
				StarnightInternalConstants.DefaultSerializerOptions
			),
			Method = HttpMethod.Patch,
			Context = new()
			{
				["endpoint"] = $"/{Applications}/{AppId}/{Commands}/{CommandId}",
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

		return JsonSerializer.Deserialize<DiscordApplicationCommand>
		(
			await message.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<Boolean> DeleteGlobalApplicationCommandAsync
	(
		Int64 applicationId,
		Int64 commandId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Applications}/{AppId}/{Commands}/{CommandId}",
			Url = $"{Channels}/{applicationId}/{Commands}/{commandId}",
			Method = HttpMethod.Delete,
			Context = new()
			{
				["endpoint"] = $"/{Applications}/{AppId}/{Commands}/{CommandId}",
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
	public async ValueTask<IEnumerable<DiscordApplicationCommand>> BulkOverwriteGlobalApplicationCommandsAsync
	(
		Int64 applicationId,
		IEnumerable<CreateApplicationCommandRequestPayload> payload,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Applications}/{AppId}/{Commands}",
			Url = $"{Channels}/{applicationId}/{Commands}",
			Payload = JsonSerializer.Serialize
			(
				payload,
				StarnightInternalConstants.DefaultSerializerOptions
			),
			Method = HttpMethod.Put,
			Context = new()
			{
				["endpoint"] = $"/{Applications}/{AppId}/{Commands}",
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

		return JsonSerializer.Deserialize<IEnumerable<DiscordApplicationCommand>>
		(
			await message.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<IEnumerable<DiscordApplicationCommand>> GetGuildApplicationCommandsAsync
	(
		Int64 applicationId,
		Int64 guildId,
		Boolean? withLocalizations = null,
		CancellationToken ct = default
	)
	{
		QueryBuilder builder = new
		(
			$"{Channels}/{applicationId}/{Guilds}/{guildId}/{Commands}"
		);

		_ = builder.AddParameter
		(
			"with_localizations",
			withLocalizations.ToString()
		);

		IRestRequest request = new RestRequest
		{
			Path = $"/{Applications}/{AppId}/{Guilds}/{GuildId}/{Commands}",
			Url = builder.Build(),
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Applications}/{AppId}/{Guilds}/{GuildId}/{Commands}",
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

		return JsonSerializer.Deserialize<IEnumerable<DiscordApplicationCommand>>
		(
			await message.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordApplicationCommand> CreateGuildApplicationCommandAsync
	(
		Int64 applicationId,
		Int64 guildId,
		CreateApplicationCommandRequestPayload payload,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Applications}/{AppId}/{Guilds}/{GuildId}/{Commands}",
			Url = $"{Channels}/{applicationId}/{Guilds}/{guildId}/{Commands}",
			Payload = JsonSerializer.Serialize
			(
				payload,
				StarnightInternalConstants.DefaultSerializerOptions
			),
			Method = HttpMethod.Post,
			Context = new()
			{
				["endpoint"] = $"/{Applications}/{AppId}/{Guilds}/{GuildId}/{Commands}",
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

		return JsonSerializer.Deserialize<DiscordApplicationCommand>
		(
			await message.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordApplicationCommand> GetGuildApplicationCommandAsync
	(
		Int64 applicationId,
		Int64 guildId,
		Int64 commandId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Applications}/{AppId}/{Guilds}/{GuildId}/{Commands}/{CommandId}",
			Url = $"{Channels}/{applicationId}/{Guilds}/{guildId}/{Commands}/{commandId}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Applications}/{AppId}/{Guilds}/{GuildId}/{Commands}/{CommandId}",
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

		return JsonSerializer.Deserialize<DiscordApplicationCommand>
		(
			await message.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordApplicationCommand> EditGuildApplicationCommandAsync
	(
		Int64 applicationId,
		Int64 guildId,
		Int64 commandId,
		EditApplicationCommandRequestPayload payload,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Applications}/{AppId}/{Guilds}/{GuildId}/{Commands}/{CommandId}",
			Url = $"{Channels}/{applicationId}/{Guilds}/{guildId}/{Commands}/{commandId}",
			Payload = JsonSerializer.Serialize
			(
				payload,
				StarnightInternalConstants.DefaultSerializerOptions
			),
			Method = HttpMethod.Patch,
			Context = new()
			{
				["endpoint"] = $"/{Applications}/{AppId}/{Guilds}/{GuildId}/{Commands}/{CommandId}",
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

		return JsonSerializer.Deserialize<DiscordApplicationCommand>
		(
			await message.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<Boolean> DeleteGuildApplicationCommandAsync
	(
		Int64 applicationId,
		Int64 guildId,
		Int64 commandId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Applications}/{AppId}/{Guilds}/{GuildId}/{Commands}/{CommandId}",
			Url = $"{Channels}/{applicationId}/{Guilds}/{guildId}/{Commands}/{commandId}",
			Method = HttpMethod.Delete,
			Context = new()
			{
				["endpoint"] = $"/{Applications}/{AppId}/{Guilds}/{GuildId}/{Commands}/{CommandId}",
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
	public async ValueTask<IEnumerable<DiscordApplicationCommand>> BulkOverwriteGuildApplicationCommandsAsync
	(
		Int64 applicationId,
		Int64 guildId,
		IEnumerable<CreateApplicationCommandRequestPayload> payload,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Applications}/{AppId}/{Guilds}/{GuildId}/{Commands}",
			Url = $"{Channels}/{applicationId}/{Guilds}/{guildId}/{Commands}",
			Payload = JsonSerializer.Serialize
			(
				payload,
				StarnightInternalConstants.DefaultSerializerOptions
			),
			Method = HttpMethod.Patch,
			Context = new()
			{
				["endpoint"] = $"/{Applications}/{AppId}/{Guilds}/{GuildId}/{Commands}",
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

		return JsonSerializer.Deserialize<IEnumerable<DiscordApplicationCommand>>
		(
			await message.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<IEnumerable<DiscordApplicationCommandPermissions>> GetGuildApplicationCommandPermissionsAsync
	(
		Int64 applicationId,
		Int64 guildId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Applications}/{AppId}/{Guilds}/{GuildId}/{Commands}/{Permissions}",
			Url = $"{Channels}/{applicationId}/{Guilds}/{guildId}/{Commands}/{Permissions}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Applications}/{AppId}/{Guilds}/{GuildId}/{Commands}/{Permissions}",
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

		return JsonSerializer.Deserialize<IEnumerable<DiscordApplicationCommandPermissions>>
		(
			await message.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordApplicationCommandPermissions> GetApplicationCommandPermissionsAsync
	(
		Int64 applicationId,
		Int64 guildId,
		Int64 commandId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Applications}/{AppId}/{Guilds}/{GuildId}/{Commands}/{CommandId}/{Permissions}",
			Url = $"{Channels}/{applicationId}/{Guilds}/{guildId}/{Commands}/{commandId}/{Permissions}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Applications}/{AppId}/{Guilds}/{GuildId}/{Commands}/{CommandId}/{Permissions}",
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

		return JsonSerializer.Deserialize<DiscordApplicationCommandPermissions>
		(
			await message.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<Boolean> CreateInteractionResponseAsync
	(
		Int64 interactionId,
		String interactionToken,
		CreateInteractionCallbackRequestPayload payload,
		CancellationToken ct = default
	)
	{
		IRestRequest request = payload.Files == null

			? new RestRequest
			{
				Path = $"/{Interactions}/{InteractionId}/{InteractionToken}/{Callback}",
				Url = $"{Interactions}/{interactionId}/{interactionToken}/{Callback}",
				Method = HttpMethod.Post,
				Payload = JsonSerializer.Serialize
				(
					payload,
					StarnightInternalConstants.DefaultSerializerOptions
				),
				Context = new()
				{
					["endpoint"] = $"/{Interactions}/{InteractionId}/{InteractionToken}/{Callback}",
					["cache"] = this.RatelimitBucketCache,
					["exempt-from-global-limit"] = true,
					["is-webhook-request"] = true
				}
			}
			: new MultipartRestRequest
			{
				Path = $"/{Interactions}/{InteractionId}/{InteractionToken}/{Callback}",
				Url = $"{Interactions}/{interactionId}/{interactionToken}/{Callback}",
				Payload = new()
				{
					["payload_json"] = JsonSerializer.Serialize
					(
						payload,
						StarnightInternalConstants.DefaultSerializerOptions
					),
				},
				Method = HttpMethod.Post,
				Files = payload.Files.ToList(),
				Context = new()
				{
					["endpoint"] = $"/{Interactions}/{InteractionId}/{InteractionToken}/{Callback}",
					["cache"] = this.RatelimitBucketCache,
					["exempt-from-global-limit"] = true,
					["is-webhook-request"] = true
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
	public async ValueTask<DiscordMessage> GetOriginalResponseAsync
	(
		Int64 applicationId,
		String interactionToken,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Webhooks}/{AppId}/{InteractionToken}/{Messages}/@{Original}",
			Url = $"{Webhooks}/{applicationId}/{interactionToken}/{Messages}/@{Original}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Webhooks}/{AppId}/{InteractionToken}/{Messages}/@{Original}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = true,
				["is-webhook-request"] = true
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<DiscordMessage>
		(
			await message.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordMessage> EditOriginalResponseAsync
	(
		Int64 applicationId,
		Int64 interactionToken,
		EditOriginalResponseRequestPayload payload,
		CancellationToken ct = default
	)
	{
		IRestRequest request = payload.Files == null

			? new RestRequest
			{
				Path = $"/{Interactions}/{AppId}/{InteractionToken}/{Messages}/@{Original}",
				Url = $"{AppId}/{applicationId}/{interactionToken}/{Messages}/@{Original}",
				Method = HttpMethod.Post,
				Payload = JsonSerializer.Serialize
				(
					payload,
					StarnightInternalConstants.DefaultSerializerOptions
				),
				Context = new()
				{
					["endpoint"] = $"/{Interactions}/{AppId}/{InteractionToken}/{Messages}/@{Original}",
					["cache"] = this.RatelimitBucketCache,
					["exempt-from-global-limit"] = true,
					["is-webhook-request"] = true
				}
			}
			: new MultipartRestRequest
			{
				Path = $"/{Interactions}/{AppId}/{InteractionToken}/{Messages}/@{Original}",
				Url = $"{Interactions}/{applicationId}/{interactionToken}/{Messages}/@{Original}",
				Payload = new()
				{
					["payload_json"] = JsonSerializer.Serialize
					(
						payload,
						StarnightInternalConstants.DefaultSerializerOptions
					),
				},
				Method = HttpMethod.Post,
				Files = payload.Files.ToList(),
				Context = new()
				{
					["endpoint"] = $"/{Interactions}/{AppId}/{InteractionToken}/{Messages}/@{Original}",
					["cache"] = this.RatelimitBucketCache,
					["exempt-from-global-limit"] = true,
					["is-webhook-request"] = true
				}
			};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<DiscordMessage>
		(
			await message.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<Boolean> DeleteOriginalInteractionResponseAsync
	(
		Int64 applicationId,
		String interactionToken,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Webhooks}/{AppId}/{InteractionToken}/{Messages}/@{Original}",
			Url = $"{Webhooks}/{applicationId}/{interactionToken}/{Messages}/@{Original}",
			Method = HttpMethod.Delete,
			Context = new()
			{
				["endpoint"] = $"/{Webhooks}/{AppId}/{InteractionToken}/{Messages}/@{Original}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = true,
				["is-webhook-request"] = true
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
	public async ValueTask<DiscordMessage> CreateFollowupMessageAsync
	(
		Int64 applicationId,
		String interactionToken,
		CreateFollowupMessageRequestPayload payload,
		CancellationToken ct = default
	)
	{
		IRestRequest request = payload.Files == null

			? new RestRequest
			{
				Path = $"/{Interactions}/{AppId}/{InteractionToken}",
				Url = $"{AppId}/{applicationId}/{interactionToken}",
				Method = HttpMethod.Post,
				Payload = JsonSerializer.Serialize
				(
					payload,
					StarnightInternalConstants.DefaultSerializerOptions
				),
				Context = new()
				{
					["endpoint"] = $"/{Interactions}/{AppId}/{InteractionToken}",
					["cache"] = this.RatelimitBucketCache,
					["exempt-from-global-limit"] = true,
					["is-webhook-request"] = true
				}
			}
			: new MultipartRestRequest
			{
				Path = $"/{Interactions}/{AppId}/{InteractionToken}",
				Url = $"{Interactions}/{applicationId}/{interactionToken}",
				Payload = new()
				{
					["payload_json"] = JsonSerializer.Serialize
					(
						payload,
						StarnightInternalConstants.DefaultSerializerOptions
					),
				},
				Method = HttpMethod.Post,
				Files = payload.Files.ToList(),
				Context = new()
				{
					["endpoint"] = $"/{Interactions}/{AppId}/{InteractionToken}",
					["cache"] = this.RatelimitBucketCache,
					["exempt-from-global-limit"] = true,
					["is-webhook-request"] = true
				}
			};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<DiscordMessage>
		(
			await message.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordMessage> GetFollowupMessageAsync
	(
		Int64 applicationId,
		String interactionToken,
		Int64 messageId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Webhooks}/{AppId}/{InteractionToken}/{Messages}/{MessageId}",
			Url = $"{Webhooks}/{applicationId}/{interactionToken}/{Messages}/{messageId}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Webhooks}/{AppId}/{InteractionToken}/{Messages}/{MessageId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = true,
				["is-webhook-request"] = true
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<DiscordMessage>
		(
			await message.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<DiscordMessage> EditFollowupMessageAsync
	(
		Int64 applicationId,
		Int64 interactionToken,
		Int64 messageId,
		EditFollowupMessageRequestPayload payload,
		CancellationToken ct = default
	)
	{
		IRestRequest request = payload.Files == null

			? new RestRequest
			{
				Path = $"/{Interactions}/{AppId}/{InteractionToken}/{Messages}/{MessageId}",
				Url = $"{AppId}/{applicationId}/{interactionToken}/{Messages}/{messageId}",
				Method = HttpMethod.Post,
				Payload = JsonSerializer.Serialize
				(
					payload,
					StarnightInternalConstants.DefaultSerializerOptions
				),
				Context = new()
				{
					["endpoint"] = $"/{Interactions}/{AppId}/{InteractionToken}/{Messages}/{MessageId}",
					["cache"] = this.RatelimitBucketCache,
					["exempt-from-global-limit"] = true,
					["is-webhook-request"] = true
				}
			}
			: new MultipartRestRequest
			{
				Path = $"/{Interactions}/{AppId}/{InteractionToken}/{Messages}/{MessageId}",
				Url = $"{Interactions}/{applicationId}/{interactionToken}/{Messages}/{messageId}",
				Payload = new()
				{
					["payload_json"] = JsonSerializer.Serialize
					(
						payload,
						StarnightInternalConstants.DefaultSerializerOptions
					),
				},
				Method = HttpMethod.Post,
				Files = payload.Files.ToList(),
				Context = new()
				{
					["endpoint"] = $"/{Interactions}/{AppId}/{InteractionToken}/{Messages}/{MessageId}",
					["cache"] = this.RatelimitBucketCache,
					["exempt-from-global-limit"] = true,
					["is-webhook-request"] = true
				}
			};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return JsonSerializer.Deserialize<DiscordMessage>
		(
			await message.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<Boolean> DeleteFollowupMessageAsync
	(
		Int64 applicationId,
		String interactionToken,
		Int64 messageId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Webhooks}/{AppId}/{InteractionToken}/{Messages}/{MessageId}",
			Url = $"{Webhooks}/{applicationId}/{interactionToken}/{Messages}/{messageId}",
			Method = HttpMethod.Delete,
			Context = new()
			{
				["endpoint"] = $"/{Webhooks}/{AppId}/{InteractionToken}/{Messages}/{MessageId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = true,
				["is-webhook-request"] = true
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync
		(
			request,
			ct
		);

		return message.StatusCode == HttpStatusCode.NoContent;
	}
}

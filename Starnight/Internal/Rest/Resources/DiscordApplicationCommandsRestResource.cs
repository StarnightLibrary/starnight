namespace Starnight.Internal.Rest.Resources;

using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualBasic;

using Starnight.Internal.Entities.Interactions.ApplicationCommands;
using Starnight.Internal.Entities.Messages;
using Starnight.Internal.Rest.Payloads.ApplicationCommands;

using static System.Net.Mime.MediaTypeNames;
using static Starnight.Internal.DiscordApiConstants;

using HttpMethodEnum = HttpMethod;

/// <summary>
/// Represents a request wrapper for all requests to the Application Commands rest resource
/// </summary>
public class DiscordApplicationCommandsRestResource : AbstractRestResource
{
	private readonly RestClient __rest_client;

	public DiscordApplicationCommandsRestResource(RestClient client, IMemoryCache cache)
		: base(cache) => this.__rest_client = client;

	/// <summary>
	/// Fetches a list of application commands for the given application.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of the given application.</param>
	/// <param name="withLocalizations">Specifies whether the response should include the full localizations
	/// (see also: <seealso cref="DiscordApplicationCommand.NameLocalizations"/> and related fields).</param>
	/// <param name="locale">If <paramref name="withLocalizations"/> is false, specifies a locale to include localizations for
	/// (see also: <seealso cref="DiscordApplicationCommand.NameLocalized"/> and related fields).</param>
	public async ValueTask<IEnumerable<DiscordApplicationCommand>> GetGlobalApplicationCommandsAsync(Int64 applicationId,
		Boolean withLocalizations = false, String? locale = null)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Applications}/{AppId}/{Commands}",
			Url = new($"{BaseUri}/{Channels}/{applicationId}/{Commands}?with_localizations={withLocalizations}"),
			Headers = locale is not null ? new()
			{
				["X-Discord-Locale"] = locale
			}
			: new(),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Applications}/{AppId}/{Commands}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<IEnumerable<DiscordApplicationCommand>>(await message.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Creates a new global application command.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of your application.</param>
	/// <param name="payload">Command creation payload.</param>
	/// <returns>The newly created application command.</returns>
	public async ValueTask<DiscordApplicationCommand> CreateGlobalApplicationCommandAsync(Int64 applicationId,
		CreateApplicationCommandRequestPayload payload)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Applications}/{AppId}/{Commands}",
			Url = new($"{BaseUri}/{Channels}/{applicationId}/{Commands}"),
			Payload = JsonSerializer.Serialize(payload),
			Method = HttpMethodEnum.Post,
			Context = new()
			{
				["endpoint"] = $"/{Applications}/{AppId}/{Commands}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordApplicationCommand>(await message.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Fetches a global command.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of the command's owning application.</param>
	/// <param name="commandId">Snowflake identifier of the command itself.</param>
	public async ValueTask<DiscordApplicationCommand> GetGlobalApplicationCommandAsync(Int64 applicationId, Int64 commandId)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Applications}/{AppId}/{Commands}/{CommandId}",
			Url = new($"{BaseUri}/{Channels}/{applicationId}/{Commands}/{commandId}"),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Applications}/{AppId}/{Commands}/{CommandId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordApplicationCommand>(await message.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Overwrites a global application command.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of your application.</param>
	/// <param name="commandId">Snowflake identifier of the command you want to overwrite.</param>
	/// <param name="payload">Edit payload.</param>
	/// <returns>The new application command object.</returns>
	public async ValueTask<DiscordApplicationCommand> EditGlobalApplicationCommandAsync(Int64 applicationId, Int64 commandId,
		EditApplicationCommandRequestPayload payload)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Applications}/{AppId}/{Commands}/{CommandId}",
			Url = new($"{BaseUri}/{Channels}/{applicationId}/{Commands}/{commandId}"),
			Payload = JsonSerializer.Serialize(payload),
			Method = HttpMethodEnum.Patch,
			Context = new()
			{
				["endpoint"] = $"/{Applications}/{AppId}/{Commands}/{CommandId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordApplicationCommand>(await message.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Deletes a global application command.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of your application.</param>
	/// <param name="commandId">Snowflake identifier of the command to be deleted.</param>
	/// <returns>Whether the deletion was successful.</returns>
	public async ValueTask<Boolean> DeleteGlobalApplicationCommandAsync(Int64 applicationId, Int64 commandId)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Applications}/{AppId}/{Commands}/{CommandId}",
			Url = new($"{BaseUri}/{Channels}/{applicationId}/{Commands}/{commandId}"),
			Method = HttpMethodEnum.Delete,
			Context = new()
			{
				["endpoint"] = $"/{Applications}/{AppId}/{Commands}/{CommandId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync(request);

		return message.StatusCode == HttpStatusCode.NoContent;
	}

	/// <summary>
	/// Bulk overwrites all global application commands for your application.
	/// </summary>
	/// <remarks>
	/// If this list contains any new commands, they will count towards the daily creation limits.
	/// </remarks>
	/// <param name="applicationId">Snowflake identifier of your application.</param>
	/// <param name="payload">List of create payloads.</param>
	/// <returns>The new loadout of application commands.</returns>
	public async ValueTask<IEnumerable<DiscordApplicationCommand>> BulkOverwriteGlobalApplicationCommandsAsync(
		Int64 applicationId, IEnumerable<CreateApplicationCommandRequestPayload> payload)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Applications}/{AppId}/{Commands}",
			Url = new($"{BaseUri}/{Channels}/{applicationId}/{Commands}"),
			Payload = JsonSerializer.Serialize(payload),
			Method = HttpMethodEnum.Put,
			Context = new()
			{
				["endpoint"] = $"/{Applications}/{AppId}/{Commands}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<IEnumerable<DiscordApplicationCommand>>(await message.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Fetches the guild-specific application commands for the specified guild.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of the application in question.</param>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="withLocalizations">Whether the returned objects should include localizations.</param>
	public async ValueTask<IEnumerable<DiscordApplicationCommand>> GetGuildApplicationCommandsAsync(Int64 applicationId,
		Int64 guildId, Boolean withLocalizations = false)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Applications}/{AppId}/{Guilds}/{GuildId}/{Commands}",
			Url = new($"{BaseUri}/{Channels}/{applicationId}/{Guilds}/{guildId}/{Commands}?with_localizations={withLocalizations}"),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Applications}/{AppId}/{Guilds}/{GuildId}/{Commands}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<IEnumerable<DiscordApplicationCommand>>(await message.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Creates a new, guild-specific application command.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of your application.</param>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="payload">Creation payload.</param>
	/// <returns>The newly created application command.</returns>
	public async ValueTask<DiscordApplicationCommand> CreateGuildApplicationCommandAsync(Int64 applicationId, Int64 guildId,
		CreateApplicationCommandRequestPayload payload)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Applications}/{AppId}/{Guilds}/{GuildId}/{Commands}",
			Url = new($"{BaseUri}/{Channels}/{applicationId}/{Guilds}/{guildId}/{Commands}"),
			Payload = JsonSerializer.Serialize(payload),
			Method = HttpMethodEnum.Post,
			Context = new()
			{
				["endpoint"] = $"/{Applications}/{AppId}/{Guilds}/{GuildId}/{Commands}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordApplicationCommand>(await message.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Fetches a guild application command.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of the application in question.</param>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="commandId">Snowflake identifier of the command in question.</param>
	public async ValueTask<DiscordApplicationCommand> GetGuildApplicationCommandAsync(Int64 applicationId, Int64 guildId, Int64 commandId)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Applications}/{AppId}/{Guilds}/{GuildId}/{Commands}/{CommandId}",
			Url = new($"{BaseUri}/{Channels}/{applicationId}/{Guilds}/{guildId}/{Commands}/{commandId}"),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Applications}/{AppId}/{Guilds}/{GuildId}/{Commands}/{CommandId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordApplicationCommand>(await message.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Edits a guild application command.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of your application.</param>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="commandId">Snowflake identifier of the command in question.</param>
	/// <param name="payload">Edit payload.</param>
	/// <returns>The new application command object.</returns>
	public async ValueTask<DiscordApplicationCommand> EditGuildApplicationCommandAsync(Int64 applicationId, Int64 guildId,
		Int64 commandId, EditApplicationCommandRequestPayload payload)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Applications}/{AppId}/{Guilds}/{GuildId}/{Commands}/{CommandId}",
			Url = new($"{BaseUri}/{Channels}/{applicationId}/{Guilds}/{guildId}/{Commands}/{commandId}"),
			Payload = JsonSerializer.Serialize(payload),
			Method = HttpMethodEnum.Patch,
			Context = new()
			{
				["endpoint"] = $"/{Applications}/{AppId}/{Guilds}/{GuildId}/{Commands}/{CommandId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordApplicationCommand>(await message.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Deletes a guild application command.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of your application.</param>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="commandId">Snowflake identifier of the command in question.</param>
	/// <returns>Whether the deletion was successful.</returns>
	public async ValueTask<Boolean> DeleteGuildApplicationCommandAsync(Int64 applicationId, Int64 guildId, Int64 commandId)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Applications}/{AppId}/{Guilds}/{GuildId}/{Commands}/{CommandId}",
			Url = new($"{BaseUri}/{Channels}/{applicationId}/{Guilds}/{guildId}/{Commands}/{commandId}"),
			Method = HttpMethodEnum.Delete,
			Context = new()
			{
				["endpoint"] = $"/{Applications}/{AppId}/{Guilds}/{GuildId}/{Commands}/{CommandId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync(request);

		return message.StatusCode == HttpStatusCode.NoContent;
	}

	/// <summary>
	/// Bulk-overwrites application commands for this guild.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of your application.</param>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="payload">New commands for this guild.</param>
	/// <returns>The newly created application commands.</returns>
	public async ValueTask<IEnumerable<DiscordApplicationCommand>> BulkOverwriteGuildApplicationCommandsAsync(Int64 applicationId,
		Int64 guildId, IEnumerable<CreateApplicationCommandRequestPayload> payload)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Applications}/{AppId}/{Guilds}/{GuildId}/{Commands}",
			Url = new($"{BaseUri}/{Channels}/{applicationId}/{Guilds}/{guildId}/{Commands}"),
			Payload = JsonSerializer.Serialize(payload),
			Method = HttpMethodEnum.Patch,
			Context = new()
			{
				["endpoint"] = $"/{Applications}/{AppId}/{Guilds}/{GuildId}/{Commands}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<IEnumerable<DiscordApplicationCommand>>(await message.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Fetches command permissions for all commands for this application in the specified guild.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of the application in question.</param>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <returns>An array of permissiono objects, one for each command.</returns>
	public async ValueTask<IEnumerable<DiscordApplicationCommandPermissions>> GetGuildApplicationCommandPermissionsAsync(
		Int64 applicationId, Int64 guildId)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Applications}/{AppId}/{Guilds}/{GuildId}/{Commands}/{Permissions}",
			Url = new($"{BaseUri}/{Channels}/{applicationId}/{Guilds}/{guildId}/{Commands}/{Permissions}"),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Applications}/{AppId}/{Guilds}/{GuildId}/{Commands}/{Permissions}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<IEnumerable<DiscordApplicationCommandPermissions>>(await message.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Fetches application command permissions for the specified command in the specified guild.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of your application.</param>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="commandId">Snowflake identifier of the command in question.</param>
	public async ValueTask<DiscordApplicationCommandPermissions> GetApplicationCommandPermissionsAsync(Int64 applicationId,
		Int64 guildId, Int64 commandId)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Applications}/{AppId}/{Guilds}/{GuildId}/{Commands}/{CommandId}/{Permissions}",
			Url = new($"{BaseUri}/{Channels}/{applicationId}/{Guilds}/{guildId}/{Commands}/{commandId}/{Permissions}"),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Applications}/{AppId}/{Guilds}/{GuildId}/{Commands}/{CommandId}/{Permissions}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordApplicationCommandPermissions>(await message.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Creates an initial response to the given interaction.
	/// </summary>
	/// <param name="interactionId">Snowflake identifier of the interaction.</param>
	/// <param name="interactionToken">Response token of the interaction.</param>
	/// <param name="payload">Payload data.</param>
	/// <returns>Whether the request succeeded.</returns>
	public async ValueTask<Boolean> CreateInteractionResponseAsync(Int64 interactionId, String interactionToken,
		CreateInteractionCallbackRequestPayload payload)
	{
		IRestRequest request = payload.Data?.Files == null

			? new RestRequest
			{
				Path = $"/{Interactions}/{InteractionId}/{InteractionToken}/{Callback}",
				Url = new($"{BaseUri}/{Interactions}/{interactionId}/{interactionToken}/{Callback}"),
				Method = HttpMethodEnum.Post,
				Payload = JsonSerializer.Serialize(payload),
				Context = new()
				{
					["endpoint"] = $"/{Interactions}/{InteractionId}/{InteractionToken}/{Callback}",
					["cache"] = this.RatelimitBucketCache,
					["exempt-from-global-limit"] = true
				}
			}
			: new MultipartRestRequest
			{
				Path = $"/{Interactions}/{InteractionId}/{InteractionToken}/{Callback}",
				Url = new($"{BaseUri}/{Interactions}/{interactionId}/{interactionToken}/{Callback}"),
				Payload = new()
				{
					["payload_json"] = JsonSerializer.Serialize(payload),
				},
				Method = HttpMethodEnum.Post,
				Files = payload.Data.Files.ToList(),
				Context = new()
				{
					["endpoint"] = $"/{Interactions}/{InteractionId}/{InteractionToken}/{Callback}",
					["cache"] = this.RatelimitBucketCache,
					["exempt-from-global-limit"] = false
				}
			};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync(request);

		return message.StatusCode == HttpStatusCode.NoContent;
	}

	/// <summary>
	/// Returns the original response to this interaction.
	/// </summary>
	/// <param name="applicationId">Snowflake identifer of your application.</param>
	/// <param name="interactionToken">Interaction token for this interaction.</param>
	public async ValueTask<DiscordMessage> GetOriginalResponseAsync(Int64 applicationId, String interactionToken)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Webhooks}/{AppId}/{InteractionToken}/{Messages}/@{Original}",
			Url = new($"{BaseUri}/{Webhooks}/{applicationId}/{interactionToken}/{Messages}/@{Original}"),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Webhooks}/{AppId}/{InteractionToken}/{Messages}/@{Original}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordMessage>(await message.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Edits the original interaction response.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of your application.</param>
	/// <param name="interactionToken">Interaction token for this interaction.</param>
	/// <param name="payload">Editing payload.</param>
	/// <returns>The newly edited message.</returns>
	public async ValueTask<DiscordMessage> EditOriginalResponseAsync(Int64 applicationId, Int64 interactionToken,
		EditOriginalResponseRequestPayload payload)
	{
		IRestRequest request = payload.Files == null

			? new RestRequest
			{
				Path = $"/{Interactions}/{AppId}/{InteractionToken}/{Messages}/@{Original}",
				Url = new($"{BaseUri}/{AppId}/{applicationId}/{interactionToken}/{Messages}/@{Original}"),
				Method = HttpMethodEnum.Post,
				Payload = JsonSerializer.Serialize(payload),
				Context = new()
				{
					["endpoint"] = $"/{Interactions}/{AppId}/{InteractionToken}/{Messages}/@{Original}",
					["cache"] = this.RatelimitBucketCache,
					["exempt-from-global-limit"] = true
				}
			}
			: new MultipartRestRequest
			{
				Path = $"/{Interactions}/{AppId}/{InteractionToken}/{Messages}/@{Original}",
				Url = new($"{BaseUri}/{Interactions}/{applicationId}/{interactionToken}/{Messages}/@{Original}"),
				Payload = new()
				{
					["payload_json"] = JsonSerializer.Serialize(payload),
				},
				Method = HttpMethodEnum.Post,
				Files = payload.Files.ToList(),
				Context = new()
				{
					["endpoint"] = $"/{Interactions}/{AppId}/{InteractionToken}/{Messages}/@{Original}",
					["cache"] = this.RatelimitBucketCache,
					["exempt-from-global-limit"] = false
				}
			};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordMessage>(await message.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Deletes the original interaction response.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of your application.</param>
	/// <param name="interactionToken">Interaction token for this interaction.</param>
	/// <returns>Whether the deletion was successful.</returns>
	public async ValueTask<Boolean> DeleteOriginalInteractionResponseAsync(Int64 applicationId, String interactionToken)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Webhooks}/{AppId}/{InteractionToken}/{Messages}/@{Original}",
			Url = new($"{BaseUri}/{Webhooks}/{applicationId}/{interactionToken}/{Messages}/@{Original}"),
			Method = HttpMethodEnum.Delete,
			Context = new()
			{
				["endpoint"] = $"/{Webhooks}/{AppId}/{InteractionToken}/{Messages}/@{Original}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync(request);

		return message.StatusCode == HttpStatusCode.NoContent;
	}

	/// <summary>
	/// Creates a followup message for an interaction.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of your application.</param>
	/// <param name="interactionToken">Interaction token for this interaction.</param>
	/// <param name="payload">Message creation payload.</param>
	/// <returns>The newly created message.</returns>
	public async ValueTask<DiscordMessage> CreateFollowupMessageAsync(Int64 applicationId, String interactionToken,
		CreateFollowupMessageRequestPayload payload)
	{
		IRestRequest request = payload.Files == null

			? new RestRequest
			{
				Path = $"/{Interactions}/{AppId}/{InteractionToken}",
				Url = new($"{BaseUri}/{AppId}/{applicationId}/{interactionToken}"),
				Method = HttpMethodEnum.Post,
				Payload = JsonSerializer.Serialize(payload),
				Context = new()
				{
					["endpoint"] = $"/{Interactions}/{AppId}/{InteractionToken}",
					["cache"] = this.RatelimitBucketCache,
					["exempt-from-global-limit"] = true
				}
			}
			: new MultipartRestRequest
			{
				Path = $"/{Interactions}/{AppId}/{InteractionToken}",
				Url = new($"{BaseUri}/{Interactions}/{applicationId}/{interactionToken}"),
				Payload = new()
				{
					["payload_json"] = JsonSerializer.Serialize(payload),
				},
				Method = HttpMethodEnum.Post,
				Files = payload.Files.ToList(),
				Context = new()
				{
					["endpoint"] = $"/{Interactions}/{AppId}/{InteractionToken}",
					["cache"] = this.RatelimitBucketCache,
					["exempt-from-global-limit"] = false
				}
			};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordMessage>(await message.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Returns the followup message for this interaction.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of your application.</param>
	/// <param name="interactionToken">Interaction token for this interaction.</param>
	/// <param name="messageId">Snowflake identifier of this message.</param>
	public async ValueTask<DiscordMessage> GetFollowupMessageAsync(Int64 applicationId, String interactionToken,
		Int64 messageId)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Webhooks}/{AppId}/{InteractionToken}/{Messages}/{MessageId}",
			Url = new($"{BaseUri}/{Webhooks}/{applicationId}/{interactionToken}/{Messages}/{messageId}"),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Webhooks}/{AppId}/{InteractionToken}/{Messages}/{MessageId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordMessage>(await message.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Edits the specified followup message.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of your application.</param>
	/// <param name="interactionToken">Interaction token for this interaction.</param>
	/// <param name="messageId">Snowflake identifier of the followup message to be edited.</param>
	/// <param name="payload">Editing payload.</param>
	/// <returns>The newly edited message.</returns>
	public async ValueTask<DiscordMessage> EditFollowupMessageAsync(Int64 applicationId, Int64 interactionToken,
		Int64 messageId, EditFollowupMessageRequestPayload payload)
	{
		IRestRequest request = payload.Files == null

			? new RestRequest
			{
				Path = $"/{Interactions}/{AppId}/{InteractionToken}/{Messages}/{MessageId}",
				Url = new($"{BaseUri}/{AppId}/{applicationId}/{interactionToken}/{Messages}/{messageId}"),
				Method = HttpMethodEnum.Post,
				Payload = JsonSerializer.Serialize(payload),
				Context = new()
				{
					["endpoint"] = $"/{Interactions}/{AppId}/{InteractionToken}/{Messages}/{MessageId}",
					["cache"] = this.RatelimitBucketCache,
					["exempt-from-global-limit"] = true
				}
			}
			: new MultipartRestRequest
			{
				Path = $"/{Interactions}/{AppId}/{InteractionToken}/{Messages}/{MessageId}",
				Url = new($"{BaseUri}/{Interactions}/{applicationId}/{interactionToken}/{Messages}/{messageId}"),
				Payload = new()
				{
					["payload_json"] = JsonSerializer.Serialize(payload),
				},
				Method = HttpMethodEnum.Post,
				Files = payload.Files.ToList(),
				Context = new()
				{
					["endpoint"] = $"/{Interactions}/{AppId}/{InteractionToken}/{Messages}/{MessageId}",
					["cache"] = this.RatelimitBucketCache,
					["exempt-from-global-limit"] = false
				}
			};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordMessage>(await message.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Deletes the specified followup message for this interaction.
	/// </summary>
	/// <param name="applicationId">Snowflake identifier of your application.</param>
	/// <param name="interactionToken">Interaction token for this interaction.</param>
	/// <param name="messageId">Snowflake identifier of this message.</param>
	public async ValueTask<Boolean> DeleteFollowupMessageAsync(Int64 applicationId, String interactionToken,
		Int64 messageId)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Webhooks}/{AppId}/{InteractionToken}/{Messages}/{MessageId}",
			Url = new($"{BaseUri}/{Webhooks}/{applicationId}/{interactionToken}/{Messages}/{messageId}"),
			Method = HttpMethodEnum.Delete,
			Context = new()
			{
				["endpoint"] = $"/{Webhooks}/{AppId}/{InteractionToken}/{Messages}/{MessageId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false
			}
		};

		HttpResponseMessage message = await this.__rest_client.MakeRequestAsync(request);

		return message.StatusCode == HttpStatusCode.NoContent;
	}
}

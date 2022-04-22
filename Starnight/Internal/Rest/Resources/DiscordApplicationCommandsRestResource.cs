namespace Starnight.Internal.Rest.Resources;

using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Memory;

using Starnight.Internal.Entities.Interactions.ApplicationCommands;
using Starnight.Internal.Rest.Payloads.ApplicationCommands;

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
	public async Task<IEnumerable<DiscordApplicationCommand>> GetGlobalApplicationCommandsAsync(Int64 applicationId,
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
	public async Task<DiscordApplicationCommand> CreateGlobalApplicationCommandAsync(Int64 applicationId,
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
	public async Task<DiscordApplicationCommand> GetGlobalApplicationCommandAsync(Int64 applicationId, Int64 commandId)
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
	public async Task<DiscordApplicationCommand> EditGlobalApplicationCommandAsync(Int64 applicationId, Int64 commandId,
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
	public async Task<Boolean> DeleteGlobalApplicationCommandAsync(Int64 applicationId, Int64 commandId)
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
	public async Task<IEnumerable<DiscordApplicationCommand>> BulkOverwriteGlobalApplicationCommandsAsync(
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
	public async Task<IEnumerable<DiscordApplicationCommand>> GetGuildApplicationCommandsAsync(Int64 applicationId,
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
	public async Task<DiscordApplicationCommand> CreateGuildApplicationCommandAsync(Int64 applicationId, Int64 guildId,
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
}

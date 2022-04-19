namespace Starnight.Internal.Rest.Resources;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Memory;

using Starnight.Internal.Entities.Interactions.ApplicationCommand;

using static DiscordApiConstants;

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
}

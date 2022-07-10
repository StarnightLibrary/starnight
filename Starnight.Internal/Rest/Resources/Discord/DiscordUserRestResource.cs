namespace Starnight.Internal.Rest.Resources.Discord;

using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using Starnight.Caching.Abstractions;
using Starnight.Internal.Entities.Users;
using Starnight.Internal.Rest.Payloads.Users;

using static DiscordApiConstants;

using HttpMethodEnum = HttpMethod;

/// <summary>
/// Represents a wrapper for all requests to Discord's User rest resource.
/// </summary>
public class DiscordUserRestResource : AbstractRestResource
{
	private readonly RestClient __rest_client;

	/// <inheritdoc/>
	public DiscordUserRestResource
	(
		RestClient client,
		ICacheService cache
	)
		: base(cache)
		=> this.__rest_client = client;

	/// <summary>
	/// Returns the current user.
	/// </summary>
	/// <remarks>
	/// For OAuth2, this requires the <c>identify</c> scope, which will return the object without an email,
	/// and optionally the <c>email</c> scope, which will return the object with an email.
	/// </remarks>
	public async ValueTask<DiscordUser> GetCurrentUserAsync()
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Users}/{Me}",
			Url = new($"{BaseUri}/{Users}/{Me}"),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Users}/{Me}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordUser>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Returns the requested user.
	/// </summary>
	/// <param name="userId">Snowflake identifier of the user in question.</param>
	public async ValueTask<DiscordUser> GetUserAsync
	(
		Int64 userId
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Users}/{UserId}",
			Url = new($"{BaseUri}/{Users}/{userId}"),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Users}/{UserId}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordUser>(await response.Content.ReadAsStringAsync())!;
	}

	/// <summary>
	/// Modifies the current user.
	/// </summary>
	/// <param name="payload">Payload to modify the current user by.</param>
	/// <returns>The newlly modified current user.</returns>
	public async ValueTask<DiscordUser> ModifyCurrentUserAsync
	(
		ModifyCurrentUserRequestPayload payload
	)
	{
		IRestRequest request = new RestRequest
		{
			Path = $"/{Users}/{Me}",
			Url = new($"{BaseUri}/{Users}/{Me}"),
			Method = HttpMethodEnum.Get,
			Context = new()
			{
				["endpoint"] = $"/{Users}/{Me}",
				["cache"] = this.RatelimitBucketCache,
				["exempt-from-global-limit"] = false,
				["is-webhook-request"] = false
			}
		};

		HttpResponseMessage response = await this.__rest_client.MakeRequestAsync(request);

		return JsonSerializer.Deserialize<DiscordUser>(await response.Content.ReadAsStringAsync())!;
	}
}

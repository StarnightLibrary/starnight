namespace Starnight.Internal.Rest.Resources.Implementation;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Starnight.Caching.Providers.Abstractions;
using Starnight.Internal.Entities.Guilds.RoleConnections;

using static Starnight.Internal.DiscordApiConstants;

public sealed class DiscordRoleConnectionRestResource
	: AbstractRestResource, IDiscordRoleConnectionRestResource
{
	private readonly RestClient restClient;

	public DiscordRoleConnectionRestResource
	(
		RestClient client,
		ICacheProvider cache
	)
		: base(cache)
		=> this.restClient = client;

	/// <inheritdoc/>
	public async ValueTask<IEnumerable<DiscordRoleConnectionMetadata>> GetApplicationRoleConnectionMetadata
	(
		Int64 applicationId,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Applications}/{applicationId}/{RoleConnections}/{Metadata}",
			Method = HttpMethod.Get,
			Context = new()
			{
				["endpoint"] = $"/{Applications}/{AppId}/{RoleConnections}/{Metadata}",
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

		return JsonSerializer.Deserialize<IEnumerable<DiscordRoleConnectionMetadata>>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}

	/// <inheritdoc/>
	public async ValueTask<IEnumerable<DiscordRoleConnectionMetadata>> UpdateApplicationRoleConnectionMetadata
	(
		Int64 applicationId,
		IEnumerable<DiscordRoleConnectionMetadata> payload,
		CancellationToken ct = default
	)
	{
		IRestRequest request = new RestRequest
		{
			Url = $"{Applications}/{applicationId}/{RoleConnections}/{Metadata}",
			Method = HttpMethod.Put,
			Payload = JsonSerializer.Serialize
			(
				payload,
				StarnightInternalConstants.DefaultSerializerOptions
			),
			Context = new()
			{
				["endpoint"] = $"/{Applications}/{AppId}/{RoleConnections}/{Metadata}",
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

		return JsonSerializer.Deserialize<IEnumerable<DiscordRoleConnectionMetadata>>
		(
			await response.Content.ReadAsStringAsync
			(
				ct
			),
			StarnightInternalConstants.DefaultSerializerOptions
		)!;
	}
}

namespace Starnight.Internal.Rest;

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Starnight.Exceptions;
using Starnight.Internal.Exceptions;
using Starnight.Internal.Utils;

/// <summary>
/// Represents a rest client for the discord API.
/// </summary>
public sealed partial class RestClient
{
	private readonly HttpClient httpClient;
	private readonly ILogger<RestClient>? logger;
	private readonly String token;

	[GeneratedRegex(@"([0-9a-z_\-\./:]+)")]
	private static partial Regex routeRegex();

	public void SetTimeout
	(
		TimeSpan timeout
	)
		=> this.httpClient.Timeout = timeout;

	public RestClient
	(
		HttpClient client,
		ILogger<RestClient> logger,
		IOptions<RestClientOptions> options,
		IOptions<TokenContainer> container
	)
	{
		this.httpClient = client;
		this.logger = logger;
		this.token = container.Value.Token;
	}

	/// <summary>
	/// Sends a request to the Discord API.
	/// </summary>
	/// <param name="request">The request in question.</param>
	/// <param name="ct">A cancellation token for this operation.</param>
	/// <returns>The received <seealso cref="HttpResponseMessage"/>.</returns>
	/// <exception cref="StarnightRequestRejectedException">
	/// Thrown if the request route is considered to be invalid. This is a very simple check, and request routes
	/// may be invalid even if this exception is not thrown.
	/// </exception>
	/// <exception cref="StarnightSharedRatelimitHitException">
	/// Thrown if the shared ratelimit for a resource was hit. This means the request will never complete, and will
	/// usually not be able to complete for a comparatively long time, but it doesn't indicate programmer or user
	/// error and should be relatively rare.
	/// </exception>
	/// <exception cref="DiscordRestException">
	/// Thrown if Discord did not return a success status code.
	/// </exception>
	public async ValueTask<HttpResponseMessage> MakeRequestAsync
	(
		IRestRequest request,
		CancellationToken ct = default
	)
	{
		if(!routeRegex().IsMatch(request.Url))
		{
			this.logger?.LogError
			(
				LoggingEvents.RestClientRequestDenied,
				"Invalid request route. Please contact the library developers."
			);

			throw new StarnightRequestRejectedException
			(
				"Requested HTTP method not implemented for this endpoint.",
				request
			);
		}

		HttpRequestMessage message = request.Build();

		Boolean isWebhookRequest = request.Context is not null
				&& request.Context!.TryGetValue("is-webhook-request", out Object webhookRaw)
				&& (Boolean)webhookRaw;

		if(!isWebhookRequest)
		{
			message.Headers.Authorization = new AuthenticationHeaderValue("Bot", this.token);
		}

		this.logger?.LogTrace(LoggingEvents.RestClientOutgoing,
			"Outgoing HTTP payload:\n{Payload}", message.ToString());

		HttpResponseMessage response = await this.httpClient.SendAsync(message, ct);

		this.logger?.LogTrace(LoggingEvents.RestClientIncoming,
			"Incoming HTTP payload:\n{Payload}", response.ToString());

		return !response.IsSuccessStatusCode
			? throw new DiscordRestException
			(
				response.StatusCode,
				response.ReasonPhrase ?? "Unknown HTTP error",
				message,
				response
			)
			: response;
	}
}

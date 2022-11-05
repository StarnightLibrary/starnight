namespace Starnight.Internal.Rest;

using System;
using System.Net;
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
	private readonly HttpClient __http_client;
	private readonly ILogger<RestClient>? __logger;
	private readonly String __token;

	[GeneratedRegex(@"([0-9a-z_\-\./:]+)")]
	private static partial Regex routeRegex();

	public void SetTimeout
	(
		TimeSpan timeout
	)
		=> this.__http_client.Timeout = timeout;

	public RestClient
	(
		HttpClient client,
		ILogger<RestClient> logger,
		IOptions<RestClientOptions> options,
		IOptions<TokenContainer> container
	)
	{
		this.__http_client = client;
		this.__logger = logger;
		this.__token = container.Value.Token;
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
	/// <exception cref="DiscordInvalidRequestException">
	/// Thrown if Discord considers the request to be invalid.
	/// </exception>
	/// <exception cref="DiscordMissingOrInvalidTokenException">
	/// Thrown if the authentication token is either invalid or missing.
	/// </exception>
	/// <exception cref="DiscordUnauthorizedException">
	/// Thrown if the current identity is not authorized to perform the action it attempted to perform.
	/// </exception>
	/// <exception cref="DiscordNotFoundException">
	/// Thrown if Discord could not find the requested resource.
	/// </exception>
	/// <exception cref="DiscordOversizedPayloadException">
	/// Thrown if the request payload exceeded 8MB.
	/// </exception>
	/// <exception cref="DiscordRatelimitHitException">
	/// Thrown if a Discord ratelimit was hit. If this happens repeatedly, this should be reported to the
	/// library developers.
	/// </exception>
	/// <exception cref="DiscordServerErrorException">
	/// Thrown if Discord could not process the request. This should be reported to Discord.
	/// </exception>
	public async ValueTask<HttpResponseMessage> MakeRequestAsync
	(
		IRestRequest request,
		CancellationToken ct = default
	)
	{
		if(!routeRegex().IsMatch(request.Url))
		{
			this.__logger?.LogError
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
			message.Headers.Authorization = new AuthenticationHeaderValue("Bot", this.__token);
		}

		this.__logger?.LogTrace(LoggingEvents.RestClientOutgoing,
			"Outgoing HTTP payload:\n{Payload}", message.ToString());

		HttpResponseMessage response = await this.__http_client.SendAsync(message, ct);

		this.__logger?.LogTrace(LoggingEvents.RestClientIncoming,
			"Incoming HTTP payload:\n{Payload}", response.ToString());

		return response.StatusCode switch
		{
			HttpStatusCode.BadRequest => throw new DiscordInvalidRequestException
			(
				400,
				"Invalid request.",
				message,
				response
			),
			HttpStatusCode.MethodNotAllowed => throw new DiscordInvalidRequestException
			(
				405,
				"Requested HTTP method not implemented for this endpoint.",
				message,
				response
			),
			HttpStatusCode.Unauthorized => throw new DiscordMissingOrInvalidTokenException
			(
				401,
				"Authentication token missing or invalid.",
				message,
				response
			),
			HttpStatusCode.Forbidden => throw new DiscordUnauthorizedException
			(
				403,
				"Not authorized for this action.",
				message,
				response
			),
			HttpStatusCode.NotFound => throw new DiscordNotFoundException
			(
				404,
				"Not found.",
				message,
				response
			),
			HttpStatusCode.RequestEntityTooLarge => throw new DiscordOversizedPayloadException
			(
				413,
				"Oversized request payload.",
				message,
				response
			),
			HttpStatusCode.TooManyRequests => throw new DiscordRatelimitHitException
			(
				429,
				"Ratelimit hit.",
				message,
				response
			),
			HttpStatusCode.InternalServerError => throw new DiscordServerErrorException
			(
				500,
				"Internal server error.",
				message,
				response
			),
			HttpStatusCode.BadGateway => throw new DiscordServerErrorException
			(
				502,
				"Bad gateway.",
				message,
				response
			),
			HttpStatusCode.ServiceUnavailable => throw new DiscordServerErrorException
			(
				503,
				"Service unavailable.",
				message,
				response
			),
			HttpStatusCode.GatewayTimeout => throw new DiscordServerErrorException
			(
				504,
				"Gateway timeout.",
				message,
				response
			),
			_ => response,
		};
	}
}

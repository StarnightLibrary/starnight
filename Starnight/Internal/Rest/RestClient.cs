namespace Starnight.Internal.Rest;

using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Starnight.Exceptions;
using Starnight.Internal.Utils;

/// <summary>
/// Represents a rest client for the discord API.
/// </summary>
public sealed partial class RestClient
{
	private readonly HttpClient __http_client;
	private readonly ILogger? __logger;

	[RegexGenerator(@":([a-z_]+)")]
	private static partial Regex routeRegex();

	public void SetTimeout(TimeSpan timeout)
		=> this.__http_client.Timeout = timeout;

	public RestClient(HttpClient client, ILogger logger)
	{
		this.__http_client = client;
		this.__logger = logger;
	}

	public async Task<HttpResponseMessage> MakeRequestAsync(IRestRequest request)
	{
		if(!routeRegex().IsMatch(request.Path))
		{
			this.__logger?.LogError(LoggingEvents.RestClientRequestDenied,
				"Invalid request route. Please contact the library developers.");
			throw new DiscordInvalidRequestException(0, "Requested HTTP method not implemented for this endpoint.");
		}

		HttpRequestMessage message = request.Build();

		HttpResponseMessage response = await this.__http_client.SendAsync(message);

		return (Int32)response.StatusCode switch
		{
			400 => throw new DiscordInvalidRequestException(400, "Invalid request."),
			405 => throw new DiscordInvalidRequestException(405, "Requested HTTP method not implemented for this endpoint."),
			401 => throw new DiscordMissingOrInvalidTokenException(401, "Authentication token missing or invalid."),
			403 => throw new DiscordUnauthorizedException(403, "Not authorized for this action."),
			413 => throw new DiscordOversizedPayloadException(413, "Oversized request payload."),
			500 => throw new DiscordServerErrorException(500, "Internal server error."),
			502 => throw new DiscordServerErrorException(502, "Bad gateway."),
			503 => throw new DiscordServerErrorException(503, "Service unavailable."),
			504 => throw new DiscordServerErrorException(504, "Gateway timeout."),
			_ => response,
		};
	}
}

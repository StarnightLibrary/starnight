namespace Starnight.Internal.Rest;

using System;

using Starnight.Exceptions;

internal static class RestExceptionTranslator
{
	public static Exception TranslateException(Int32 starnightError, Int32 httpError)
	{
		return starnightError switch
		{
			10000 => new DiscordInvalidRequestException(httpError, "Invalid request."),
			10001 => new DiscordMissingOrInvalidTokenException(httpError, "Authentication token missing or invalid."),
			10002 => new DiscordUnauthorizedException(httpError, "Not authorized for this action."),
			10003 => new DiscordNotFoundException(httpError, "Requested resource not found."),
			10004 => new DiscordInvalidRequestException(httpError, "Requested HTTP method not implemented for this endpoint."),
			10005 => new DiscordOversizedPayloadException(httpError, "Oversized request payload."),
			10006 => new DiscordRatelimitHitException(httpError, "Ratelimit hit. Please report 10006/429 errors to the library developers."),
			10007 => new DiscordServerErrorException(httpError, "Internal server error."),
			10008 => new DiscordServerErrorException(httpError, "Bad gateway."),
			10009 => new DiscordServerErrorException(httpError, "Service unavailable."),
			10010 => new DiscordServerErrorException(httpError, "Gateway timeout."),
			10011 => new DiscordRatelimitHitException(httpError, "Internal ratelimiter hit. This is not a Discord 429 error, but rather an internal 429 error."),
			10012 => new DiscordServerErrorException(httpError, "Unknown error. Please report 10012 errors to the library developers."),
			_ => new DiscordServerErrorException(httpError, "Unknown error. Please report 10012 errors to the library developers.")
		};
	}
}

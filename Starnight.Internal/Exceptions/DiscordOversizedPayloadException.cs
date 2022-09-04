namespace Starnight.Internal.Exceptions;

using System;
using System.Net.Http;

/// <summary>
/// Thrown if the request payload was too long.
/// </summary>
public class DiscordOversizedPayloadException : AbstractDiscordException
{
	public DiscordOversizedPayloadException
	(
		Int32 responseCode,
		String message,
		HttpRequestMessage request,
		HttpResponseMessage response
	)
		: base(responseCode, message)
	{ }
}

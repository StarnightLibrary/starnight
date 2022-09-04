namespace Starnight.Internal.Exceptions;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;

/// <summary>
/// Thrown when any ratelimit is hit.
/// </summary>
public class DiscordRatelimitHitException : AbstractDiscordException
{
	[SetsRequiredMembers]
	public DiscordRatelimitHitException
	(
		Int32 responseCode,
		String message,
		HttpRequestMessage request,
		HttpResponseMessage response
	)
		: base(responseCode, message)
	{ }
}

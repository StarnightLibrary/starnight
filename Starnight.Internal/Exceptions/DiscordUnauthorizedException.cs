namespace Starnight.Internal.Exceptions;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;

/// <summary>
/// Thrown if the passed token is valid, but bot account permissions do not suffice
/// </summary>
public class DiscordUnauthorizedException : AbstractDiscordException
{
	[SetsRequiredMembers]
	public DiscordUnauthorizedException
	(
		Int32 responseCode,
		String message,
		HttpRequestMessage request,
		HttpResponseMessage response
	)
		: base(responseCode, message)
	{ }
}

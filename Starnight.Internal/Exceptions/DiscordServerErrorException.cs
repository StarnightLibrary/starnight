namespace Starnight.Internal.Exceptions;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;

/// <summary>
/// Thrown if Discord is unable to process the request for Discord reasons.
/// </summary>
public class DiscordServerErrorException : AbstractDiscordException
{
	[SetsRequiredMembers]
	public DiscordServerErrorException
	(
		Int32 responseCode,
		String message,
		HttpRequestMessage request,
		HttpResponseMessage response
	)
		: base(responseCode, message)
	{ }
}

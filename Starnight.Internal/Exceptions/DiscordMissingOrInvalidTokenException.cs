namespace Starnight.Internal.Exceptions;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;

/// <summary>
/// Represents a missing or invalid token passed to a Rest request.
/// </summary>
public class DiscordMissingOrInvalidTokenException : AbstractDiscordRestException
{
	[SetsRequiredMembers]
	public DiscordMissingOrInvalidTokenException
	(
		Int32 responseCode,
		String message,
		HttpRequestMessage request,
		HttpResponseMessage response
	)
		: base(responseCode, message, request, response)
	{ }
}

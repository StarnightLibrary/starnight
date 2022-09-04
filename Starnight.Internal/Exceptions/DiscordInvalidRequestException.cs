namespace Starnight.Internal.Exceptions;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;

/// <summary>
/// Represents an InvalidRequestException, thrown on error codes 400 and 405.
/// </summary>
public class DiscordInvalidRequestException : AbstractDiscordRestException
{
	/// <inheritdoc/>
	[SetsRequiredMembers]
	public DiscordInvalidRequestException
	(
		Int32 responseCode,
		String message,
		HttpRequestMessage request,
		HttpResponseMessage response
	)
		: base(responseCode, message, request, response)
	{ }
}

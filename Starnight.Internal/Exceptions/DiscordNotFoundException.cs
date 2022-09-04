namespace Starnight.Internal.Exceptions;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;

/// <summary>
/// Thrown if the requested resource could not be found on the server.
/// </summary>
public class DiscordNotFoundException : AbstractDiscordException
{
	[SetsRequiredMembers]
	public DiscordNotFoundException
	(
		Int32 responseCode,
		String message,
		HttpRequestMessage request,
		HttpResponseMessage response
	)
		: base(responseCode, message)
	{ }
}

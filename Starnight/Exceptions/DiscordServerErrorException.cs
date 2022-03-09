namespace Starnight.Exceptions;

using System;

/// <summary>
/// Thrown if Discord is unable to process the request for Discord reasons.
/// </summary>
public class DiscordServerErrorException : AbstractDiscordException
{
	public DiscordServerErrorException(Int32 responseCode, String message) : base(responseCode, message) { }
}

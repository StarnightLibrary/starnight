namespace Starnight.Internal.Exceptions;

using System;

/// <summary>
/// Thrown if the passed token is valid, but bot account permissions do not suffice
/// </summary>
public class DiscordUnauthorizedException : AbstractDiscordException
{
	public DiscordUnauthorizedException(Int32 responseCode, String message) : base(responseCode, message) { }
}

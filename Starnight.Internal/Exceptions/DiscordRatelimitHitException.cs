namespace Starnight.Internal.Exceptions;

using System;

/// <summary>
/// Thrown when any ratelimit is hit.
/// </summary>
public class DiscordRatelimitHitException : AbstractDiscordException
{
	public DiscordRatelimitHitException(Int32 responseCode, String message) : base(responseCode, message) { }
}

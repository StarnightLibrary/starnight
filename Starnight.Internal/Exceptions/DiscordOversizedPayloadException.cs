namespace Starnight.Internal.Exceptions;

using System;

/// <summary>
/// Thrown if the request payload was too long.
/// </summary>
public class DiscordOversizedPayloadException : AbstractDiscordException
{
	public DiscordOversizedPayloadException(Int32 responseCode, String message) : base(responseCode, message) { }
}

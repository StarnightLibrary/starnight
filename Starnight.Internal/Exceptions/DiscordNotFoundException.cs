namespace Starnight.Internal.Exceptions;

using System;

/// <summary>
/// Thrown if the requested resource could not be found on the server.
/// </summary>
public class DiscordNotFoundException : AbstractDiscordException
{
	public DiscordNotFoundException(Int32 responseCode, String message) : base(responseCode, message) { }
}

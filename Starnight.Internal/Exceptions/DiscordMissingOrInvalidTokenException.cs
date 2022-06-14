namespace Starnight.Internal.Exceptions;

using System;

/// <summary>
/// Represents a missing or invalid token passed to a Rest request.
/// </summary>
public class DiscordMissingOrInvalidTokenException : AbstractDiscordException
{
	public DiscordMissingOrInvalidTokenException(Int32 responseCode, String message) : base(responseCode, message) { }
}

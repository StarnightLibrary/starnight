namespace Starnight.Exceptions;

using System;

/// <summary>
/// Represents an InvalidRequestException, thrown on error codes 400 and 405.
/// </summary>
public class DiscordInvalidRequestException : AbstractDiscordException
{
	/// <inheritdoc/>
	public DiscordInvalidRequestException(Int32 responseCode, String message) : base(responseCode, message) {}
}

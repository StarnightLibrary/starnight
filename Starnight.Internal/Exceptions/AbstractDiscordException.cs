namespace Starnight.Internal.Exceptions;

using System;

/// <summary>
/// Represents an abstract base class for all gateway/rest exceptions.
/// </summary>
public abstract class AbstractDiscordException : Exception
{
	/// <inheritdoc/>
	public AbstractDiscordException
	(
		String message
	)
		: base(message)
	{ }
}

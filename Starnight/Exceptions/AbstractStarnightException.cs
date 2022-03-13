namespace Starnight.Exceptions;

using System;

/// <summary>
/// A base class for all Starnight-thrown exceptions.
/// </summary>
public abstract class AbstractStarnightException : Exception
{
	/// <summary>
	/// Fully qualified name of the calling method.
	/// </summary>
	public String Caller { get; private set; }

	public AbstractStarnightException(String message, String caller) : base(message)
		=> this.Caller = caller;
}

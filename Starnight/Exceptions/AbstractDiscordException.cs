namespace Starnight.Exceptions;

using System;

/// <summary>
/// Represents an abstract base class for all gateway/rest exceptions.
/// </summary>
public abstract class AbstractDiscordException : Exception
{
	/// <summary>
	/// Stores the response code sent by the gateway/rest API.
	/// </summary>
	public Int32 ResponseCode { get; set; }

	public AbstractDiscordException(Int32 responseCode, String message) : base(message)
		=> this.ResponseCode = responseCode;
}

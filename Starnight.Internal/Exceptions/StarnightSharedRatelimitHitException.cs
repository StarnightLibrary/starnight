namespace Starnight.Internal.Exceptions;

using System;

/// <summary>
/// Thrown if a request hits the resource shared ratelimit.
/// </summary>
public class StarnightSharedRatelimitHitException : AbstractStarnightException
{
	public StarnightSharedRatelimitHitException(String message, String caller)
		: base(message, caller)
	{
	}
}

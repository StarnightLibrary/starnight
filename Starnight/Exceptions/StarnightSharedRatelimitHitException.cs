namespace Starnight.Exceptions;

using System;

/// <summary>
/// Thrown if a request hits the resource shared ratelimit.
/// </summary>
public class StarnightSharedRatelimitHitException : AbstractStarnightException
{
	/// <summary>
	/// Stores which API resource returned the shared ratelimit.
	/// </summary>
	public String ResourceName { get; private set; }

	/// <summary>
	/// Indicates when requests to this resource may be resumed.
	/// </summary>
	public DateTimeOffset ResumeAt { get; private set; }

	public StarnightSharedRatelimitHitException(String message, String caller, String resourceName, DateTimeOffset resumeAt)
		: base(message, caller)
	{
		this.ResourceName = resourceName;
		this.ResumeAt = resumeAt;
	}

	public StarnightSharedRatelimitHitException(String caller, String resourceName, DateTimeOffset resumeAt)
		: base("Shared ratelimit hit.", caller)
	{
		this.ResourceName = resourceName;
		this.ResumeAt = resumeAt;
	}
}

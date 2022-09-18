namespace Starnight.Exceptions;

using System;
using System.Runtime.CompilerServices;

using Starnight.Internal.Gateway.Payloads;

/// <summary>
/// An exception thrown if Starnight refused to connect to the gateway.
/// </summary>
public class StarnightGatewayConnectionRefusedException : AbstractStarnightException
{
	/// <summary>
	/// The session start limit received from Discord.
	/// </summary>
	public DiscordSessionStartLimit? SessionStartLimit { get; set; }

	public StarnightGatewayConnectionRefusedException
	(
		String message,
		DiscordSessionStartLimit? sessionStartLimit = null,

		[CallerMemberName]
		String caller = ""
	)
		: base(message, caller)
		=> this.SessionStartLimit = sessionStartLimit;
}

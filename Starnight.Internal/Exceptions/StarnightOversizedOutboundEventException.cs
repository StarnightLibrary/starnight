namespace Starnight.Internal.Exceptions;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

using Starnight.Internal.Gateway;

/// <summary>
/// An exception thrown if Starnight encountered an outbound event too large to send to Discord.
/// </summary>
public class StarnightOversizedOutboundEventException : AbstractStarnightException
{
	/// <summary>
	/// The event whose serialized form exceeded 4096 bytes.
	/// </summary>
	public required IDiscordGatewayEvent Event { get; set; }

	[SetsRequiredMembers]
	public StarnightOversizedOutboundEventException
	(
		String message,
		IDiscordGatewayEvent @event,

		[CallerMemberName]
		String caller = ""
	)
		: base(message, caller)
		=> this.Event = @event;
}

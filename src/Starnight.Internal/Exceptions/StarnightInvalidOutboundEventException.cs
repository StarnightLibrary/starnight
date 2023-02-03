namespace Starnight.Exceptions;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

using Starnight.Internal.Gateway.Events;

/// <summary>
/// Thrown if an event is considered invalid by the Starnight gateway client.
/// </summary>
public class StarnightInvalidOutboundEventException : AbstractStarnightException
{
	/// <summary>
	/// The event which was invalid in one way or another.
	/// </summary>
	public required IDiscordGatewayEvent Event { get; set; }

	[SetsRequiredMembers]
	public StarnightInvalidOutboundEventException
	(
		String message,
		IDiscordGatewayEvent @event,

		[CallerMemberName]
		String caller = ""
	)
		: base(message, caller)
		=> this.Event = @event;
}

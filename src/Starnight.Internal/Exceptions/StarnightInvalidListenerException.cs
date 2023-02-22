namespace Starnight.Internal.Exceptions;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

using Starnight.Exceptions;

/// <summary>
/// An exception thrown if Starnight encountered an outbound event too large to send to Discord.
/// </summary>
public class StarnightInvalidListenerException : AbstractStarnightException
{
	/// <summary>
	/// The event whose serialized form exceeded 4096 bytes.
	/// </summary>
	public required Type ListenerType { get; set; }

	[SetsRequiredMembers]
	public StarnightInvalidListenerException
	(
		String message,
		Type listenerType,

		[CallerMemberName]
		String caller = ""
	)
		: base(message, caller)
		=> this.ListenerType = listenerType;
}

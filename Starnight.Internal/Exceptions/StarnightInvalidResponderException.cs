namespace Starnight.Exceptions;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

/// <summary>
/// An exception thrown if Starnight encountered an outbound event too large to send to Discord.
/// </summary>
public class StarnightInvalidResponderException : AbstractStarnightException
{
	/// <summary>
	/// The event whose serialized form exceeded 4096 bytes.
	/// </summary>
	public required Type ResponderType { get; set; }

	[SetsRequiredMembers]
	public StarnightInvalidResponderException
	(
		String message,
		Type responderType,

		[CallerMemberName]
		String caller = ""
	)
		: base(message, caller)
		=> this.ResponderType = responderType;
}

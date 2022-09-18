namespace Starnight.Exceptions;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

/// <summary>
/// Thrown if any connection information was invalid.
/// </summary>
public class StarnightInvalidConnectionException : AbstractStarnightException
{
	[SetsRequiredMembers]
	public StarnightInvalidConnectionException
	(
		String message,

		[CallerMemberName]
		String caller = ""
	)
		: base(message, caller)
	{ }
}

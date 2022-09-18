namespace Starnight.Exceptions;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

using Starnight.Internal.Rest;

/// <summary>
/// Thrown if Starnight rejected a request from being made.
/// </summary>
public class StarnightRequestRejectedException : AbstractStarnightException
{
	/// <summary>
	/// The request Starnight rejected.
	/// </summary>
	public required IRestRequest Request { get; set; }

	[SetsRequiredMembers]
	public StarnightRequestRejectedException
	(
		String message,
		IRestRequest request,

		[CallerMemberName]
		String caller = ""
	)
		: base(message, caller)
		=> this.Request = request;
}

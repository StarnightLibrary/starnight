namespace Starnight.Internal.Exceptions;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;

/// <summary>
/// A base class for all exceptions thrown in Starnight's implementation of Discord's REST API.
/// </summary>
public abstract class AbstractDiscordRestException : AbstractDiscordException
{
	/// <summary>
	/// The request message which generated the error.
	/// </summary>
	public required HttpRequestMessage RequestMessage { get; set; }

	/// <summary>
	/// The response message.
	/// </summary>
	public required HttpResponseMessage ResponseMessage { get; set; }

	/// <inheritdoc/>
	[SetsRequiredMembers]
	public AbstractDiscordRestException
	(
		Int32 responseCode,
		String message,
		HttpRequestMessage request,
		HttpResponseMessage response
	)
		: base(responseCode, message)
	{
		this.RequestMessage = request;
		this.ResponseMessage = response;
	}
}

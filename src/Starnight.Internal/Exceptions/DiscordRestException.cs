namespace Starnight.Internal.Exceptions;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;

/// <summary>
/// A base class for all exceptions thrown in Starnight's implementation of Discord's REST API.
/// </summary>
public class DiscordRestException : AbstractDiscordException
{
	/// <summary>
	/// The request message which generated the error.
	/// </summary>
	public required HttpRequestMessage RequestMessage { get; init; }

	/// <summary>
	/// The response message.
	/// </summary>
	public required HttpResponseMessage ResponseMessage { get; init; }

	/// <summary>
	/// The returned status code.
	/// </summary>
	public required HttpStatusCode StatusCode { get; init; }

	/// <inheritdoc/>
	[SetsRequiredMembers]
	public DiscordRestException
	(
		HttpStatusCode statusCode,
		String message,
		HttpRequestMessage request,
		HttpResponseMessage response
	)
		: base(message)
	{
		this.RequestMessage = request;
		this.ResponseMessage = response;
		this.StatusCode = statusCode;
	}
}

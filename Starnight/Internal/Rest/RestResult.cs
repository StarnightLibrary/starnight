namespace Starnight.Internal.Rest;

using System.Net;
using System;

public struct RestResult : IRestResult
{
	/// <summary>
	/// The status code returned by Discord's API.
	/// </summary>
	public HttpStatusCode StatusCode { get; init; }

	/// <summary>
	/// Returns whether this request was processed successfully by Discord.
	/// </summary>
	public Boolean IsSuccessful()
		=> !(this.IsInvalidRequest() || this.ReturnedServerError());

	/// <summary>
	/// Returns whether this request was faulty.
	/// </summary>
	public Boolean IsInvalidRequest()
	{
		return this.StatusCode == HttpStatusCode.BadRequest ||
			this.StatusCode == HttpStatusCode.Unauthorized ||
			this.StatusCode == HttpStatusCode.Forbidden ||
			this.StatusCode == HttpStatusCode.NotFound ||
			this.StatusCode == HttpStatusCode.MethodNotAllowed ||
			this.StatusCode == HttpStatusCode.RequestEntityTooLarge ||
			this.StatusCode == HttpStatusCode.TooManyRequests;
	}

	/// <summary>
	/// Returns whether this request was valid but could not be processed.
	/// </summary>
	public Boolean ReturnedServerError()
	{
		return this.StatusCode == HttpStatusCode.InternalServerError ||
			this.StatusCode == HttpStatusCode.NotImplemented ||
			this.StatusCode == HttpStatusCode.BadGateway ||
			this.StatusCode == HttpStatusCode.ServiceUnavailable ||
			this.StatusCode == HttpStatusCode.GatewayTimeout;
	}
}

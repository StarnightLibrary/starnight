namespace Starnight.Internal.Rest.Exceptions;

using System;
using System.Net.Http;

public class GlobalRatelimitExceededException : Exception
{
	public RatelimitBucket Bucket { get; init; }

	public HttpResponseMessage Response { get; init; }

	public GlobalRatelimitExceededException(String message, RatelimitBucket bucket, HttpResponseMessage response) : base(message)
	{
		this.Bucket = bucket;
		this.Response = response;
	}
}

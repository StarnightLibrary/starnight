namespace Starnight.Internal.Rest.Exceptions;

using System;
using System.Net.Http;

public class RatelimitExceededException : Exception
{
	public RatelimitBucket Bucket { get; init; }

	public HttpResponseMessage Response { get; init; }

	public RatelimitExceededException(String message, RatelimitBucket bucket, HttpResponseMessage response) : base(message)
	{
		this.Bucket = bucket;
		this.Response = response;
	}
}

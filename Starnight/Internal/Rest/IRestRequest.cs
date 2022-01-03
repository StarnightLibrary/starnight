namespace Starnight.Internal.Rest;

using System;
using System.Collections.Generic;

/// <summary>
/// Represents a request contract for both kinds of rest requests (single-request and multipart request).
/// </summary>
public unsafe interface IRestRequest
{
	public HttpMethod Method { get; }

	public String Route { get; }

	public Uri Url { get; }

	public Dictionary<String, String> Headers { get; }

	public RateLimitBucket* Bucket { get; }

	public IRestResult Request();
}

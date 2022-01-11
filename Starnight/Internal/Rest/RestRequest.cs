namespace Starnight.Internal.Rest;

using System;
using System.Collections.Generic;

/// <summary>
/// Represents a single-part REST request.
/// </summary>
public struct RestRequest : IRestRequest
{
	public HttpMethod Method { get; init; }

	public String Route { get; init; }

	public Uri Url { get; init; }

	public Dictionary<String, String> Headers { get; init; }

	public RatelimitBucket Bucket { get; init; }

	public String Payload { get; init; }

	public IRestResult Request() => throw new NotImplementedException();
}

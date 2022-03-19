namespace Starnight.Internal.Rest;

using System;
using System.Collections.Generic;
using System.Net.Http;

/// <summary>
/// Represents a request contract for both kinds of rest requests (single-request and multipart request).
/// </summary>
public interface IRestRequest
{
	public HttpMethod Method { get; }

	public String Path { get; }

	public String Route { get; }

	public Uri Url { get; }

	public Dictionary<String, String> Headers { get; }

	public String? Token { get; }

	public HttpRequestMessage Build();
}

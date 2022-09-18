namespace Starnight.Internal.Rest;

using System;
using System.Collections.Generic;
using System.Net.Http;

using Polly;

/// <summary>
/// Represents a request contract for both kinds of rest requests (single-request and multipart request).
/// </summary>
public interface IRestRequest
{
	public HttpMethod Method { get; }

	public String Path { get; }

	public String Url { get; }

	public Dictionary<String, String> Headers { get; }

	public Context? Context { get; }

	public HttpRequestMessage Build();
}

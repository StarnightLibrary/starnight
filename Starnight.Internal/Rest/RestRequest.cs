namespace Starnight.Internal.Rest;

using System;
using System.Collections.Generic;
using System.Net.Http;

using Polly;

/// <summary>
/// Represents a single-part REST request.
/// </summary>
public struct RestRequest : IRestRequest
{
	public RestRequest()
	{ }

	public HttpMethod Method { get; init; } = HttpMethod.Get;

	public required String Path { get; init; } 

	public required String Url { get; init; } 

	public Dictionary<String, String> Headers { get; init; } = new();

	public String? Payload { get; init; }

	public required Context Context { get; init; }

	public HttpRequestMessage Build()
	{
		HttpRequestMessage message = new(this.Method, this.Url);

		foreach(KeyValuePair<String, String> kv in this.Headers)
		{
			message.Headers.Add(kv.Key, kv.Value);
		}

		if(this.Payload is not null)
		{
			message.Content = new StringContent(this.Payload);
		}

		message.SetPolicyExecutionContext(this.Context);

		return message;
	}
}

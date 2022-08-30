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

	public String Path { get; init; } = null!;

	public Uri Url { get; init; } = null!;

	public Dictionary<String, String> Headers { get; init; } = new();

	public String? Payload { get; init; } = null;

	public Context Context { get; init; } = null!;

	public HttpRequestMessage Build()
	{
		System.Net.Http.HttpMethod method = new(this.Method switch
		{
			HttpMethod.Delete => "DELETE",
			HttpMethod.Get => "GET",
			HttpMethod.Head => "HEAD",
			HttpMethod.Patch => "PATCH",
			HttpMethod.Post => "POST",
			HttpMethod.Put => "PUT",
			_ => throw new InvalidOperationException("Invalid HTTP method specified")
		});

		HttpRequestMessage message = new(method, this.Url);

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

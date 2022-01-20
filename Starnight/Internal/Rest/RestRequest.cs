namespace Starnight.Internal.Rest;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

/// <summary>
/// Represents a single-part REST request.
/// </summary>
public struct RestRequest : IRestRequest
{
	public HttpMethod Method { get; init; }

	public String Route { get; init; }

	public Uri Url { get; init; }

	public Dictionary<String, String> Headers { get; init; }

	public String Payload { get; init; }

	public String Token { get; init; }

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

		message.Headers.Authorization = new AuthenticationHeaderValue(this.Token);
		foreach(KeyValuePair<String, String> kv in this.Headers)
		{
			message.Headers.Add(kv.Key, kv.Value);
		}

		message.Content = new StringContent(this.Payload);

		return message;
	}
}

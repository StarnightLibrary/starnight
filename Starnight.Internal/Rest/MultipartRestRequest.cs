namespace Starnight.Internal.Rest;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

using Polly;

using Starnight.Internal.Rest.Payloads;

/// <summary>
/// Represents a multipart rest rewuest.
/// </summary>
public struct MultipartRestRequest : IRestRequest
{
	public MultipartRestRequest()
	{ }

	public HttpMethod Method { get; init; } = HttpMethod.Get;

	public required String Path { get; init; }

	public required String Url { get; init; }

	public Dictionary<String, String> Headers { get; init; } = new();

	public Dictionary<String, String> Payload { get; set; } = new();

	public List<DiscordAttachmentFile> Files { get; set; } = new();

	public required Context Context { get; init; }

	public HttpRequestMessage Build()
	{
		HttpRequestMessage message = new(this.Method, this.Url);

		foreach(KeyValuePair<String, String> kv in this.Headers)
		{
			message.Headers.Add(kv.Key, kv.Value);
		}

		// multipart time D:

		String boundary = $"-----------------------{DateTimeOffset.Now.Ticks:x}-----------------------";

		message.Headers.Add("Connection", "keep-alive");
		message.Headers.Add("Keep-Alive", "1200");

		MultipartFormDataContent content = new(boundary);

		content.Headers.ContentType = new("multipart/form-data");

		foreach(KeyValuePair<String, String> kv in this.Payload)
		{
			StringContent sc = new(kv.Value);
			sc.Headers.ContentDisposition = new($"form-data; name=\"{kv.Key}\"");
			sc.Headers.ContentType = new("application/json");

			content.Add(sc);
		}

		for(Int32 i = 0; i < this.Files.Count; i++)
		{
			StreamContent streamContent = new(this.Files[i].FileStream);

			if(this.Files[i].ContentType is not null)
			{
				streamContent.Headers.ContentType = new MediaTypeHeaderValue(this.Files[i].ContentType!);
			}

			content.Add(streamContent, $"file[{i}]", this.Files[i].Filename);
		}

		message.Content = content;

		message.SetPolicyExecutionContext(this.Context);

		return message;
	}
}

namespace Starnight.Internal.Rest;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

using Starnight.Internal.Rest.Payloads;

/// <summary>
/// Represents a multipart rest rewuest.
/// </summary>
public struct MultipartRestRequest : IRestRequest
{
	private Uri __url = null!;

	public MultipartRestRequest()
	{ }

	public HttpMethod Method { get; init; } = HttpMethod.Get;

	public String Path { get; init; } = null!;

	public String Route { get; init; } = null!;

	public Uri Url
	{
		get => this.__url;
		init
		{
			this.__url = value;
			this.Route = value.OriginalString.Split('?')[0];
		}
	}

	public Dictionary<String, String> Headers { get; init; } = new();

	public Dictionary<String, String> Payload { get; set; } = new();

	public List<DiscordAttachmentFile> Files { get; set; } = new();

	public String? Token { get; init; } = null;

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

		if(this.Token != null)
		{
			message.Headers.Authorization = new AuthenticationHeaderValue(this.Token);
		}

		foreach(KeyValuePair<String, String> kv in this.Headers)
		{
			message.Headers.Add(kv.Key, kv.Value);
		}

		// multipart time D:

		String boundary = $"-----------------------{DateTimeOffset.Now.Ticks:x}";

		message.Headers.Add("Connection", "keep-alive");
		message.Headers.Add("Keep-Alive", "1200");

		MultipartFormDataContent content = new(boundary);

		foreach(KeyValuePair<String, String> kv in this.Payload)
		{
			content.Add(new StringContent(kv.Value), kv.Key);
		}

		for(Int32 i = 0; i < this.Files.Count; i++)
		{
			StreamContent streamContent = new(this.Files[i].FileStream);

			if(this.Files[i].ContentType != null)
			{
				streamContent.Headers.ContentType = new MediaTypeHeaderValue(this.Files[i].ContentType!);
			}

			content.Add(streamContent, $"file{i}", this.Files[i].Filename);
		}

		message.Content = content;

		return message;
	}
}

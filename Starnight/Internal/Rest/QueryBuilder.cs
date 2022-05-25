namespace Starnight.Internal.Rest;

using System;
using System.Collections.Generic;
using System.Linq;

internal class QueryBuilder
{
	private readonly List<KeyValuePair<String, String>> __parameters = new();

	public Uri RootUri { get; set; }

	public QueryBuilder(String root)
		=> this.RootUri = new(root);

	public QueryBuilder AddParameter(String key, String? value)
	{
		if(value is null)
		{
			return this;
		}

		if(value.ToLowerInvariant() == "null")
		{
			return this;
		}

		this.__parameters.Add(new(key, value));
		return this;
	}

	public Uri Build()
	{
		return new UriBuilder(this.RootUri)
		{
			Query = String.Join("&", this.__parameters.Select(e => Uri.EscapeDataString(e.Key) + '=' + Uri.EscapeDataString(e.Value)))
		}
		.Uri;
	}
}

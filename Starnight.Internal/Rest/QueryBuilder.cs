namespace Starnight.Internal.Rest;

using System;
using System.Collections.Generic;
using System.Linq;

internal class QueryBuilder
{
	private readonly List<KeyValuePair<String, String>> parameters = new();

	public String RootUri { get; set; }

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

		this.parameters.Add(new(key, value));
		return this;
	}

	public String Build()
		=> this.RootUri + String.Join("&", this.parameters.Select(e => Uri.EscapeDataString(e.Key) + '=' + Uri.EscapeDataString(e.Value)));
}

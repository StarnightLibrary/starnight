namespace Starnight.SourceGenerators;

using System;
using System.Collections.Generic;

using Microsoft.CodeAnalysis;

internal static class NamespaceSymbolExtensions
{
	public static String GetFullNamespace(this INamespaceSymbol symbol)
	{
		INamespaceSymbol @namespace = symbol;

		List<String> names = new()
		{
			@namespace.Name
		};

		while(!@namespace.ContainingNamespace.IsGlobalNamespace)
		{
			@namespace = @namespace.ContainingNamespace;
			names.Add(@namespace.Name);
		}

		names.Reverse();
		return String.Join(".", names);
	}
}

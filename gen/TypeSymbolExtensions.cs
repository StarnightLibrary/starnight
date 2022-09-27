namespace Starnight.SourceGenerators;

using System;

using Microsoft.CodeAnalysis;

internal static class TypeSymbolExtensions
{
	public static String GetFullyQualifiedName(this ITypeSymbol symbol)
		=> $"global::{symbol.ContainingNamespace.GetFullNamespace()}.{symbol.Name}";

	public static String GetFullyQualifiedName(this INamedTypeSymbol symbol)
		=> $"global::{symbol.ContainingNamespace.GetFullNamespace()}.{symbol.Name}";
}

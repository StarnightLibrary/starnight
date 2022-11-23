namespace Starnight.SourceGenerators;

using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;

internal static class TypeSymbolExtensions
{
	public static String GetFullyQualifiedName(this ITypeSymbol symbol)
		=> $"global::{symbol.ContainingNamespace.GetFullNamespace()}.{symbol.Name}";

	public static String GetFullyQualifiedName(this INamedTypeSymbol symbol)
		=> $"global::{symbol.ContainingNamespace.GetFullNamespace()}.{symbol.Name}";

	public static IEnumerable<IPropertySymbol> GetPublicProperties
	(
		this ITypeSymbol type
	)
	{
		IEnumerable<IPropertySymbol> symbols = type.GetMembers()
		.Where
		(
			xm => xm is IPropertySymbol
			{
				DeclaredAccessibility: Accessibility.Public,
				SetMethod: not null
			}
		)
		.Cast<IPropertySymbol>();

		if(type.BaseType is not null)
		{
			symbols = symbols.Concat
			(
				type.BaseType.GetPublicProperties()
			);
		}

		return symbols;
	}
}

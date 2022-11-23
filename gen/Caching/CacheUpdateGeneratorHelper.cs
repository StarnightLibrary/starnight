namespace Starnight.SourceGenerators.Caching;

using System;
using System.Linq;

using Microsoft.CodeAnalysis;

internal class CacheUpdateGeneratorHelper
{
	public const String Attribute = """
		// auto-generated code
		namespace Starnight.SourceGenerators.Caching;

		[global::System.AttributeUsage(global::System.AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
		[global::System.CodeDom.Compiler.GeneratedCode("starnight-cache-update-generator", "0.2.0")]
		internal sealed class GenerateCacheUpdaterAttribute : global::System.Attribute
		{ }
		""";

	public static CacheUpdaterMetadata? ExtractMetadata
	(
		IMethodSymbol symbol
	)
	{
		return symbol.ReturnsVoid || !symbol.ReturnType.Equals(symbol.Parameters.First().Type, SymbolEqualityComparer.Default)
			? null
			: new()
			{
				ContainingTypeName = symbol.ContainingType.Name,
				ContainingNamespaceName = symbol.ContainingNamespace.GetFullNamespace(),
				MethodName = symbol.Name,
				ReturnType = symbol.ReturnType,
				FirstParameterType = symbol.Parameters.First().Type,
				SecondParameterType = symbol.Parameters.Last().Type,
				FirstParameter = symbol.Parameters.First().Name,
				SecondParameter = symbol.Parameters.Last().Name,
				MethodAccessibility = symbol.DeclaredAccessibility
			};
	}
}

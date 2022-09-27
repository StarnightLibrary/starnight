namespace Starnight.SourceGenerators.Caching;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;

internal class CacheUpdateGeneratorHelper
{
	public const String Attribute = """
		// auto-generated code
		namespace Starnight.SourceGenerators.Caching;

		[global::System.AttributeUsage(global::System.AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
		[global::System.CodeDom.Compiler.GeneratedCode("starnight-cache-update-generator", "0.1.0")]
		internal sealed class CacheUpdateMethodAttribute : global::System.Attribute
		{ }
		""";

	public static IEnumerable<CacheUpdateMethodMetadata> GetCacheMetadata
	(
		Compilation compilation,
		ImmutableArray<MethodDeclarationSyntax> methods,
		SourceProductionContext ctx
	)
	{
		List<CacheUpdateMethodMetadata> metadata = new();

		INamedTypeSymbol? attribute = compilation.GetTypeByMetadataName("Starnight.SourceGenerators.Caching.CacheUpdateMethodAttribute");

		if(attribute is null)
		{
			// carthage is burning
			return metadata;
		}

		foreach(MethodDeclarationSyntax methodDeclaration in methods)
		{
			ctx.CancellationToken.ThrowIfCancellationRequested();

			SemanticModel model = compilation.GetSemanticModel(methodDeclaration.SyntaxTree);

			if(model is not IMethodSymbol method)
			{
				continue;
			}

			if(method.Arity != 0)
			{
				continue;
			}

			if(method.Parameters.Length != 1)
			{
				continue;
			}

			CacheUpdateMethodMetadata current = new()
			{
				ContainingTypeName = $"global::{method.ContainingNamespace.GetFullNamespace()}.{method.ContainingType.Name}",
				MethodName = method.Name,
				CachedType = method.Parameters.First().Type
			};

			metadata.Add(current);
		}

		return metadata;
	}
}

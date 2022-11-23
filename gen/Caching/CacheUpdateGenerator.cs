namespace Starnight.SourceGenerators.Caching;

using System;
using System.Collections.Immutable;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

[Generator]
public class CacheUpdateGenerator : IIncrementalGenerator
{
	public void Initialize
	(
		IncrementalGeneratorInitializationContext context
	)
	{
		context.RegisterPostInitializationOutput
		(
			xm => xm.AddSource
			(
				"GenerateCacheUpdaterAttribute.generated.cs",
				CacheUpdateGeneratorHelper.Attribute
			)
		);

		IncrementalValuesProvider<MethodDeclarationSyntax> methodDeclarations =
			context.SyntaxProvider.CreateSyntaxProvider
			(
				predicate: (s, _) => this.isMethodTargetForGeneration(s),
				transform: (ctx, _) => this.getTargetForGeneration(ctx)
			)
			.Where(static xm => xm is not null)!;

		IncrementalValueProvider<(Compilation, ImmutableArray<MethodDeclarationSyntax>)> compilation =
			context.CompilationProvider.Combine(methodDeclarations.Collect());

		context.RegisterSourceOutput
		(
			source: compilation,
			action: (source, ctx) => this.execute(ctx.Item1, ctx.Item2, source)
		);
	}

	private Boolean isMethodTargetForGeneration
	(
		SyntaxNode node
	)
	{
		return node is MethodDeclarationSyntax
		{
			AttributeLists.Count: > 0,
			ParameterList.Parameters.Count: 2,
			Arity: 0
		};
	}

	private MethodDeclarationSyntax? getTargetForGeneration
	(
		GeneratorSyntaxContext ctx
	)
	{
		MethodDeclarationSyntax declaration = (MethodDeclarationSyntax)ctx.Node;

		foreach(AttributeListSyntax attributeList in declaration.AttributeLists)
		{
			foreach(AttributeSyntax attribute in attributeList.Attributes)
			{
				if(ctx.SemanticModel.GetSymbolInfo(attribute).Symbol is not IMethodSymbol attributeSymbol)
				{
					continue;
				}

				INamedTypeSymbol attributeTypeSymbol = attributeSymbol.ContainingType;

				if(attributeTypeSymbol.ToDisplayString() == "Starnight.SourceGenerators.Caching.GenerateCacheUpdaterAttribute")
				{
					return declaration;
				}
			}
		}

		return null;
	}

	private void execute
	(
		Compilation compilation,
		ImmutableArray<MethodDeclarationSyntax> methods,
		SourceProductionContext ctx
	)
	{
		if(methods.IsDefaultOrEmpty)
		{
			return;
		}

		foreach(MethodDeclarationSyntax method in methods)
		{
			SemanticModel model = compilation.GetSemanticModel
			(
				method.SyntaxTree,
				true
			);

			if(model.GetDeclaredSymbol(method) is not IMethodSymbol methodSymbol)
			{
				continue;
			}

			try
			{
				CacheUpdaterMetadata metadata = CacheUpdateGeneratorHelper.ExtractMetadata
				(
					methodSymbol
				)
				?? default;

				if(metadata == default)
				{
					continue;
				}

				ctx.AddSource
				(
					$"Update_{metadata.ContainingTypeName}_{metadata.MethodName}.generated.cs",
					CacheUpdateEmitter.Emit
					(
						metadata
					)
				);
			}
			catch(Exception e)
			{
				ctx.ReportDiagnostic
				(
					Diagnostic.Create
					(
						new DiagnosticDescriptor
						(
							"SNC1000",
							"Unknown error",
							"An unknown error occurred: {0}: {1}",
							"unknown-error",
							DiagnosticSeverity.Error,
							true
						),
						methodSymbol.Locations.FirstOrDefault(),
						e.GetType(),
						e.Message
					)
				);
				throw;
			}
		}
	}
}

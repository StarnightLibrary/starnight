namespace Starnight.SourceGenerators.Caching;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

[Generator]
public class CacheUpdateGenerator : IIncrementalGenerator
{
	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		context.RegisterPostInitializationOutput
		(
			xm => xm.AddSource
			(
				"CacheUpdateMethodAttribute.generated.cs",
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

	private Boolean isMethodTargetForGeneration(SyntaxNode node)
		=> node is MethodDeclarationSyntax { AttributeLists.Count: > 0 };

	private MethodDeclarationSyntax? getTargetForGeneration(GeneratorSyntaxContext ctx)
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

				if(attributeTypeSymbol.ToDisplayString() == "Starnight.SourceGenerators.Caching.CacheUpdateMethodAttribute")
				{
					return declaration;
				}
			}
		}

		return null;
	}

	private void execute(Compilation compilation, ImmutableArray<MethodDeclarationSyntax> methods, SourceProductionContext ctx)
	{
		if(!Debugger.IsAttached)
		{
			// _ = Debugger.Launch();
		}
		 
		try
		{
			if(methods.IsDefaultOrEmpty)
			{
				return;
			}

			IEnumerable<CacheUpdateMethodMetadata> metadata = CacheUpdateGeneratorHelper.GetCacheMetadata(compilation, methods, ctx);

			foreach(CacheUpdateMethodMetadata m in metadata)
			{
				String result = CacheUpdateEmitter.Emit(m);
				ctx.AddSource($"Deserialize_{m.ContainingTypeName}_{m.MethodName}.generated.cs", result);
			}
		}
		catch(Exception e)
		{
			Console.WriteLine($"{e}: {e.Message}\n{e.StackTrace}");
			throw;
		}
	}
}

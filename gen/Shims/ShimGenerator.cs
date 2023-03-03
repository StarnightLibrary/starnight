namespace Starnight.SourceGenerators.Shims;

using System;
using System.Collections.Immutable;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

[Generator]
public class ShimGenerator : IIncrementalGenerator
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
				"ShimAttribute.generated.cs",
				ShimGeneratorHelper.Attribute
			)
		);

		IncrementalValuesProvider<ClassDeclarationSyntax> classDeclarations =
			context.SyntaxProvider.CreateSyntaxProvider
			(
				predicate: (s, _) => this.isSyntaxTargetForGeneration(s),
				transform: (ctx, _) => this.getTargetForGeneration(ctx)
			)
			.Where(xm => xm is not null)!;

		IncrementalValueProvider<(Compilation, ImmutableArray<ClassDeclarationSyntax>)> compilation =
			context.CompilationProvider.Combine(classDeclarations.Collect());

		context.RegisterSourceOutput
		(
			source: compilation,
			action: (source, ctx) => this.execute(ctx.Item1, ctx.Item2, source)
		);
	}

	private Boolean isSyntaxTargetForGeneration
	(
		SyntaxNode node
	)
	{
		return node is ClassDeclarationSyntax
		{
			AttributeLists.Count: > 0,
			Arity: 0
		};
	}

	private ClassDeclarationSyntax? getTargetForGeneration
	(
		GeneratorSyntaxContext ctx
	)
	{
		ClassDeclarationSyntax declaration = (ClassDeclarationSyntax)ctx.Node;

		foreach(AttributeListSyntax attributeList in declaration.AttributeLists)
		{
			foreach(AttributeSyntax attribute in attributeList.Attributes)
			{
				if(ctx.SemanticModel.GetSymbolInfo(attribute).Symbol is not IMethodSymbol attributeSymbol)
				{
					continue;
				}

				INamedTypeSymbol attributeTypeSymbol = attributeSymbol.ContainingType;

				if(attributeTypeSymbol.MetadataName == "ShimAttribute`1")
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
		ImmutableArray<ClassDeclarationSyntax> classes,
		SourceProductionContext ctx
	)
	{
		if(classes.IsDefaultOrEmpty)
		{
			return;
		}

		foreach(ClassDeclarationSyntax data in classes)
		{
			SemanticModel model = compilation.GetSemanticModel
			(
				data.SyntaxTree,
				true
			);

			if(model.GetDeclaredSymbol(data) is not INamedTypeSymbol shimSymbol)
			{
				continue;
			}

			try
			{
				ctx.AddSource
				(
					$"{shimSymbol.Name}.Redirects.generated.cs",
					ShimEmitter.Emit
					(
						shimSymbol
					)
				);
			}
			catch (Exception e)
			{
				ctx.ReportDiagnostic
				(
					Diagnostic.Create
					(
						new DiagnosticDescriptor
						(
							"SNS1000",
							"Unknown error",
							"An unknown error occurred: {0}: {1}",
							"unknown-error",
							DiagnosticSeverity.Error,
							true
						),
						shimSymbol.Locations.FirstOrDefault(),
						e.GetType(),
						e.Message
					)
				);
				throw;
			}
		}
	}
}

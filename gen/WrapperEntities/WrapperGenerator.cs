namespace Starnight.SourceGenerators.WrapperEntities;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

[Generator]
public class WrapperGenerator : IIncrementalGenerator
{
	public void Initialize
	(
		IncrementalGeneratorInitializationContext context
	)
	{
		IncrementalValuesProvider<ClassDeclarationSyntax> classDeclarations =
			context.SyntaxProvider.CreateSyntaxProvider
			(
				predicate: (s, _) => this.isClassTargetForGeneration(s),
				transform: (ctx, _) => this.getTargetForGeneration(ctx)
			)
			.Where(static xm => xm is not null)!;

		IncrementalValueProvider<(Compilation, ImmutableArray<ClassDeclarationSyntax>)> compilation =
			context.CompilationProvider.Combine(classDeclarations.Collect());

		context.RegisterSourceOutput
		(
			source: compilation,
			action: (source, ctx) => this.execute(ctx.Item1, ctx.Item2, source)
		);
	}

	private Boolean isClassTargetForGeneration
	(
		SyntaxNode node
	)
	{
		return node is ClassDeclarationSyntax
		{
			Arity: 0
		};
	}

	private ClassDeclarationSyntax? getTargetForGeneration
	(
		GeneratorSyntaxContext context
	)
	{
		if
		(
			context.SemanticModel.GetDeclaredSymbol
			(
				context.Node
			) is not INamedTypeSymbol symbol
		)
		{
			return null;
		}

		foreach(INamedTypeSymbol implementedInterface in symbol.AllInterfaces)
		{
			if(implementedInterface.MetadataName == "IStarnightEntity`2")
			{
				return context.Node as ClassDeclarationSyntax;
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

		foreach(ClassDeclarationSyntax classDeclaration in classes)
		{
			SemanticModel model = compilation.GetSemanticModel
			(
				classDeclaration.SyntaxTree,
				true
			);

			if(model.GetDeclaredSymbol(classDeclaration) is not INamedTypeSymbol symbol)
			{
				continue;
			}

			try
			{
				WrapperMetadata? metadata = this.extractMetadata
				(
					symbol,
					compilation
				);

				if(metadata is null)
				{
					continue;
				}

				ctx.AddSource
				(
					$"{metadata.TypeName}.generated.cs",
					WrapperEmitter.Emit(metadata)
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
							"SNS2000",
							"Unknown error",
							"An unknown error occurred: {0}: {1}",
							"unknown-error",
							DiagnosticSeverity.Error,
							true
						),
						symbol.Locations.FirstOrDefault(),
						e.GetType(),
						e.Message
					)
				);
				throw;
			}
		}
	}

	private WrapperMetadata? extractMetadata
	(
		INamedTypeSymbol symbol,
		Compilation compilation
	)
	{
		// get attributes
		List<String> disabledTransformations = new();
		Dictionary<String, String> renames = new();
		Boolean generateImplementation = false;

		INamedTypeSymbol? generateInterfaceImplementationSymbol = compilation.GetTypeByMetadataName
		(
			"Starnight.Infrastructure.TransformationServices.GenerateInterfaceImplementationAttribute"
		);

		if(generateInterfaceImplementationSymbol is null)
		{
			return null;
		}

		INamedTypeSymbol? disableCollectionTransformationSymbol = compilation.GetTypeByMetadataName
		(
			"Starnight.Infrastructure.TransformationServices.DisableCollectionTransformationAttribute"
		);

		if(disableCollectionTransformationSymbol is null)
		{
			return null;
		}

		INamedTypeSymbol? renameTransformedPropertySymbol = compilation.GetTypeByMetadataName
		(
			"Starnight.Infrastructure.TransformationServices.RenameTransformedPropertyAttribute"
		);

		if(renameTransformedPropertySymbol is null)
		{
			return null;
		}

		foreach(AttributeData attributeData in symbol.GetAttributes())
		{
			if
			(
				generateInterfaceImplementationSymbol.Equals
				(
					attributeData.AttributeClass,
					SymbolEqualityComparer.Default
				)
			)
			{
				generateImplementation = attributeData.ConstructorArguments.Length != 0
					? Unsafe.Unbox<Boolean>(attributeData.ConstructorArguments.First().Value)
					: Unsafe.Unbox<Boolean>(attributeData.NamedArguments.First().Value.Value);
			}
			else if
			(
				disableCollectionTransformationSymbol.Equals
				(
					attributeData.AttributeClass,
					SymbolEqualityComparer.Default
				)
			)
			{
				disabledTransformations.Add
				(
					attributeData.ConstructorArguments.Length != 0
						? (attributeData.ConstructorArguments[0].Value as String)!
						: (attributeData.NamedArguments[0].Value.Value as String)!
				);
			}
			else if
			(
				renameTransformedPropertySymbol.Equals
				(
					attributeData.AttributeClass,
					SymbolEqualityComparer.Default
				)
			)
			{
				if(attributeData.ConstructorArguments.Length != 0)
				{
					renames.Add
					(
						(attributeData.ConstructorArguments[0].Value as String)!,
						(attributeData.ConstructorArguments[1].Value as String)!
					);
				}
				else
				{
					String original = attributeData.NamedArguments
						.Where(x => x.Key == "InternalName")
						.Select(x => (x.Value.Value as String)!)
						.First();

					String overwrite = attributeData.NamedArguments
						.Where(x => x.Key == "WrapperName")
						.Select(x => (x.Value.Value as String)!)
						.First();

					renames.Add
					(
						original,
						overwrite
					);
				}
			}
		}

		// get the internal type
		INamedTypeSymbol type = null!;

		foreach(INamedTypeSymbol interfaceType in symbol.AllInterfaces)
		{
			if(interfaceType.MetadataName != "IStarnightEntity`2")
			{
				continue;
			}

			type = (INamedTypeSymbol)interfaceType.TypeArguments[1];
		}

		return new WrapperMetadata
		{
			TypeName = symbol.Name,
			ContainingNamespace = symbol.ContainingNamespace.GetFullNamespace(),
			DisabledTransformations = disabledTransformations,
			Renames = renames,
			GenerateInterfaceImplementation = generateImplementation,
			InternalType = type,
		};
	}
}

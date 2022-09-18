namespace Starnight.SourceGenerators.GatewayEvents;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

[Generator]
public class GatewayEventDeserializationGenerator : IIncrementalGenerator
{
	private const String __attribute =
@"// auto-generated code
namespace Starnight.SourceGenerators.GatewayEvents;

[global::System.AttributeUsage(global::System.AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
[global::System.CodeDom.Compiler.GeneratedCode(""starnight-gateway-events-generator"", ""0.1.0"")]
internal sealed class GatewayEventAttribute : global::System.Attribute
{
	public required global::System.String EventName { get; set; }
	public required global::System.Type EventType { get; set; }
}";

	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		context.RegisterPostInitializationOutput
		(
			ctx => ctx.AddSource
			(
				"GatewayEventAttribute.generated.cs",
				__attribute
			)
		);

		IncrementalValuesProvider<ClassDeclarationSyntax> classDeclarations =
			context.SyntaxProvider.CreateSyntaxProvider
			(
				predicate: (s, _) => this.isSyntaxTargetForGeneration(s),
				transform: (ctx, _) => this.getSemanticTargetForGeneration(ctx)
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

	private Boolean isSyntaxTargetForGeneration(SyntaxNode node)
		=> node is ClassDeclarationSyntax { AttributeLists.Count: > 0 };

	private ClassDeclarationSyntax? getSemanticTargetForGeneration(GeneratorSyntaxContext ctx)
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
				if(attributeTypeSymbol.ToDisplayString() == "Starnight.SourceGenerators.GatewayEvents.GatewayEventAttribute")
				{
					return declaration;
				}
			}
		}

		return null;
	}

	private void execute(Compilation compilation, ImmutableArray<ClassDeclarationSyntax> classes, SourceProductionContext ctx)
	{
		try
		{
			if(classes.IsDefaultOrEmpty)
			{
				return;
			}

			IEnumerable<GatewayEventMetadata> registeredEvents = this.getRegisteredEvents(compilation, classes, ctx);

			foreach(GatewayEventMetadata metadata in registeredEvents)
			{
				String result = this.generate(metadata);
				ctx.AddSource($"Deserialize_{metadata.EventName}.generated.cs", result);
			}
		}
		catch(Exception e)
		{
			Console.WriteLine($"{e}: {e.Message}\n{e.StackTrace}");
			throw;
		}
	}

	private IEnumerable<GatewayEventMetadata> getRegisteredEvents(Compilation compilation, ImmutableArray<ClassDeclarationSyntax> classes, SourceProductionContext ctx)
	{
		List<GatewayEventMetadata> events = new();

		INamedTypeSymbol? attribute = compilation.GetTypeByMetadataName("Starnight.SourceGenerators.GatewayEvents.GatewayEventAttribute");

		if(attribute is null)
		{
			// rome is burning
			return events;
		}

		foreach(ClassDeclarationSyntax classDeclaration in classes)
		{
			ctx.CancellationToken.ThrowIfCancellationRequested();

			SemanticModel semanticModel = compilation.GetSemanticModel(classDeclaration.SyntaxTree);

			if(semanticModel.GetDeclaredSymbol(classDeclaration) is not INamedTypeSymbol classSymbol)
			{
				continue;
			}

			String payloadType = "";
			String eventName = "";
			String eventType = "";

			foreach(AttributeData attributeData in classSymbol.GetAttributes())
			{
				if(!attribute.Equals(attributeData.AttributeClass, SymbolEqualityComparer.Default))
				{
					continue;
				}

				foreach(KeyValuePair<String, TypedConstant> argument in attributeData.NamedArguments)
				{
					if(argument.Key == "EventName")
					{
						eventName = argument.Value.Value!.ToString();
					}
					else if(argument.Key == "EventType")
					{
						INamedTypeSymbol eventTypeSymbol = (INamedTypeSymbol)argument.Value.Value!;
						eventType = $"{eventTypeSymbol.ContainingNamespace.GetFullNamespace()}.{eventTypeSymbol.Name}";

						INamedTypeSymbol propertyTypeSymbol = (INamedTypeSymbol)eventTypeSymbol.GetMembers()
							.Where(xm => xm is IPropertySymbol property && property.Name == "Data")
							.Select(xm => (xm as IPropertySymbol)!.Type)
							.First();

						payloadType = $"{propertyTypeSymbol.ContainingNamespace.GetFullNamespace()}.{propertyTypeSymbol.Name}";
					}
					else
					{
						continue;
					}
				}

				events.Add(new()
				{
					EventName = eventName,
					EventType = eventType,
					PayloadType = payloadType,
					DeclaringClass = classSymbol
				});
			}
		}

		return events;
	}

	private String generate(GatewayEventMetadata metadata)
	{
		return
$@"// auto-generated code
namespace {metadata.DeclaringClass.ContainingNamespace.GetFullNamespace()};

partial class {metadata.DeclaringClass.Name}
{{
	[global::System.CodeDom.Compiler.GeneratedCode(""starnight-gateway-events-generator"", ""0.2.0"")]
	private static global::Starnight.Internal.Gateway.IDiscordGatewayEvent {this.getMethodName(metadata.EventName)}(global::System.Text.Json.JsonElement element)
	{{
		global::Starnight.Internal.Gateway.DiscordGatewayOpcode opcode = (global::Starnight.Internal.Gateway.DiscordGatewayOpcode)element.GetProperty(""op"").GetInt32();
		global::System.String name = element.GetProperty(""t"").GetString()!;
		global::System.Int32 sequence = element.GetProperty(""s"").GetInt32();
		global::{metadata.PayloadType} data = global::System.Text.Json.JsonSerializer.Deserialize<global::{metadata.PayloadType}>(element.GetProperty(""d""), global::Starnight.Internal.StarnightInternalConstants.DefaultSerializerOptions)!;
		return new global::{metadata.EventType}
		{{
			Opcode = opcode,
			EventName = name,
			Sequence = sequence,
			Data = data
		}};
	}}
}}
";
	}

	private String getMethodName(String eventName)
	{
		String[] segments = eventName.Split('_');

		StringBuilder builder = new("deserialize");

		foreach(String s in segments)
		{
			_ = builder.Append(s[0]).Append(s.Substring(1).ToLowerInvariant());
		}

		return builder.ToString();
	}
}

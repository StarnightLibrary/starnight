namespace Starnight.SourceGenerators.GatewayEvents;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

[Generator]
public class GatewayEventDeserializationGenerator : ISourceGenerator
{
	private const String __attribute =
@"// auto-generated code
namespace Starnight.SourceGenerators.GatewayEvents;

[global::System.AttributeUsage(global::System.AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
[global::System.CodeDom.Compiler.GeneratedCode(""starnight-gateway-events-generator"", ""0.1.0"")]
internal sealed class GatewayEventAttribute : global::System.Attribute
{
	public global::System.String EventName { get; set; }
	public global::System.Type EventType { get; set; }

	public GatewayEventAttribute(global::System.String name, global::System.Type type)
	{
		this.EventName = name;
		this.EventType = type;
	}
}";


	public void Initialize(GeneratorInitializationContext context)
	{ 
		context.RegisterForPostInitialization(i => i.AddSource("GatewayEventAttribute.generated.cs", __attribute));

		context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
	}

	public void Execute(GeneratorExecutionContext context)
	{
		if(context.SyntaxContextReceiver is not SyntaxReceiver receiver)
		{
			return;
		}

		INamedTypeSymbol attributeSymbol = context.Compilation.GetTypeByMetadataName("Starnight.SourceGenerators.GatewayEvents.GatewayEventAttribute")!;

		foreach(ITypeSymbol type in receiver.SourceTypes)
		{
			foreach(AttributeData v in type.GetAttributes().Where(xm => xm.GetType() == attributeSymbol.GetType()))
			{
				String eventName = Unsafe.As<String>(v.ConstructorArguments.First().Value!);
				Type eventType = Unsafe.As<Type>(v.ConstructorArguments.Last().Value!);

				String source = this.processClassAttribute(type, eventName, eventType);
				context.AddSource($"{type.Name}_{eventName}.generated.cs", source);
			}
		}
	}

	private String processClassAttribute(ITypeSymbol type, String eventName, Type eventType)
	{
		if(type.ContainingType is not null)
		{
			return " ";
		}

		StringBuilder code = new();

		String eventTypeName = "global::" + eventType.Namespace + "." + eventType.Name;

		String payloadTypeName = "global::" + eventType.Namespace + "." +eventType.GetProperty("Data")!.PropertyType.Name;

		_ = code.Append("// auto-generated code")
			.Append($"namespace {type.ContainingType!.Name};\n")
			.Append($"partial class {type.Name}\n")
			.Append("{\n");

		_ = code.Append(@"[global::System.CodeDom.Compiler.GeneratedCode(""starnight-gateway-events-generator"", ""0.1.0"")]");
		_ = code.Append($"\tprivate static global::Starnight.Internal.Gateway.IDiscordGatewayEvent {this.getMethodName(eventName)}(global::System.Text.Json.JsonElement element)")
			.Append("\t{")
			.Append(
@$"		   global::Starnight.Internal.Gateway.DiscordGatewayOpcode opcode = (global::Starnight.Internal.Gateway.DiscordGateway)element.GetProperty(""op"").GetInt32();
		global::System.String name = element.GetProperty(""t"").GetString()!;
		global::System.Int32 sequence = element.GetProperty(""s"").GetInt32();
		{payloadTypeName} data = global::System.Text.Json.JsonSerializer.Deserialize<{payloadTypeName}>(element.GetProperty(""d""), global::Starnight.Internal.StarnightConstants.DefaultSerializerOptions)!;
		return new {eventTypeName}
		{{
			Opcode = opcode,
			EventName = name,
			Sequence = sequence,
			Data = data
		}};
");

		_ = code.Append("\t}")
			.Append("}");

		return code.ToString();
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

internal class SyntaxReceiver : ISyntaxContextReceiver
{
	public List<ITypeSymbol> SourceTypes { get; } = new List<ITypeSymbol>();

	public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
	{
		if(context.Node is TypeDeclarationSyntax typeDeclarationSyntax && typeDeclarationSyntax.AttributeLists.Count > 0)
		{
			ITypeSymbol typeSymbol = (ITypeSymbol)context.SemanticModel.GetDeclaredSymbol(typeDeclarationSyntax)!;

			if(typeSymbol.GetAttributes().Any(xm => xm.AttributeClass?.ToDisplayString() == "Starnight.SourceGenerator.GatewayEvents.GatewayEventAttribute"))
			{
				this.SourceTypes.Add(typeSymbol);
			}
		}
	}
}

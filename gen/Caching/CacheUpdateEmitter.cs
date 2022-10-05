namespace Starnight.SourceGenerators.Caching;

using System;
using System.Linq;
using System.Text;

using Microsoft.CodeAnalysis;

internal static class CacheUpdateEmitter
{
	public static String Emit(CacheUpdateMethodMetadata metadata)
	{
		StringBuilder codeBuilder = new();

		_ = codeBuilder.Append($$"""
// auto-generated code
namespace {{metadata.ContainingNamespaceName}};

partial class {{metadata.ContainingTypeName}}
{
	[global::System.CodeDom.Compiler.GeneratedCode("starnight-cache-update-generator", "0.1.0")]
	public static partial {{metadata.CachedType.GetFullyQualifiedName()}} {{metadata.MethodName}}
	(
		{{metadata.CachedType.GetFullyQualifiedName()}} {{metadata.Parameter1}},
		{{metadata.CachedType.GetFullyQualifiedName()}} {{metadata.Parameter2}}
	)
	{
		{{metadata.CachedType.GetFullyQualifiedName()}} value = {{metadata.Parameter1}};

""");

		foreach(IPropertySymbol symbol in metadata.CachedType.GetMembers()
			.Where(xm => xm is IPropertySymbol s && s.DeclaredAccessibility == Accessibility.Public)
			.Cast<IPropertySymbol>())
		{
			Boolean isOptional;
			INamedTypeSymbol namedType = (INamedTypeSymbol)symbol.Type;

			isOptional = namedType.Arity == 1 &&
				namedType.MetadataName == "Optional`1" &&
				namedType.ContainingNamespace.GetFullNamespace() == "Starnight";

			if(isOptional)
			{
				// replace only if they're different. we don't want the extra allocation
				_ = codeBuilder.Append($$"""
		if(!value.{{symbol.Name}}.HasValue && {{metadata.Parameter2}}.{{symbol.Name}}.HasValue)
		{
			value = value with { {{symbol.Name}} = {{metadata.Parameter2}}.{{symbol.Name}} };
		}
		else if
		(
			value.{{symbol.Name}}.HasValue &&
			{{metadata.Parameter2}}.{{symbol.Name}}.HasValue &&
			value.{{symbol.Name}} != {{metadata.Parameter2}}.{{symbol.Name}}
		)
		{
			value = value with { {{symbol.Name}} = {{metadata.Parameter2}}.{{symbol.Name}} };
		}

""");
			}
			else
			{
				_ = codeBuilder.Append($$"""
		if(value.{{symbol.Name}} != {{metadata.Parameter2}}.{{symbol.Name}})
		{
			value = value with { {{symbol.Name}} = {{metadata.Parameter2}}.{{symbol.Name}} };
		}

""");
			}
		}

		_ = codeBuilder.Append("""
		return value;
	}
}
""");
		return codeBuilder.ToString();
	}
}

namespace Starnight.SourceGenerators.Caching;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

internal static class CacheUpdateEmitter
{
	public static String Emit
	(
		CacheUpdaterMetadata metadata
	)
	{
		return metadata.FirstParameterType.Equals
		(
			metadata.SecondParameterType,
			SymbolEqualityComparer.Default
		)
			? emitIdenticalTypes(metadata)
			: emitDifferentTypes(metadata);
	}

	private static String emitIdenticalTypes
	(
		CacheUpdaterMetadata metadata
	)
	{
		StringBuilder builder = new();

		_ = builder.Append($$"""
// auto-generated code

#nullable enable

namespace {{metadata.ContainingNamespaceName}};

partial class {{metadata.ContainingTypeName}}
{
	{{SyntaxFacts.GetText(metadata.MethodAccessibility)}} partial {{metadata.ReturnType.GetFullyQualifiedName()}} {{metadata.MethodName}}
	(
		{{metadata.FirstParameterType.GetFullyQualifiedName()}} {{metadata.FirstParameter}},
		{{metadata.SecondParameterType.GetFullyQualifiedName()}} {{metadata.SecondParameter}}
	)
	{
		{{metadata.TrackingStruct}} tracking = {{metadata.TrackingStruct}}.ConvertToTrackingStruct
		(
			{{metadata.FirstParameter}}
		);

""");

		foreach
		(
			IPropertySymbol symbol in metadata.FirstParameterType.GetPublicProperties()
		)
		{
			INamedTypeSymbol named = (INamedTypeSymbol)symbol.Type;

			Boolean optional = named.Arity == 1
				&& named.MetadataName == "Optional`1"
				&& named.ContainingNamespace.GetFullNamespace() == "Starnight";

#pragma warning disable IDE0045 // already unreadable as is
			if(optional)
			{
				_ = builder.Append($$"""

		if({{metadata.SecondParameter}}.{{symbol.Name}}.HasValue)
		{
			tracking = tracking with { {{symbol.Name}} = {{metadata.SecondParameter}}.{{symbol.Name}} };
		}

""");
			}
			else
			{
				_ = builder.Append($$"""

		tracking = tracking with { {{symbol.Name}} = {{metadata.SecondParameter}}.{{symbol.Name}} };

""");
			}
#pragma warning restore IDE0045
		}

		_ = builder.Append($$"""

		return {{metadata.TrackingStruct}}.ConvertToRecord
		(
			tracking
		);
	}
}

""");

		emitTrackingStruct
		(
			metadata,
			builder
		);

		return builder.ToString();
	}

	private static String emitDifferentTypes
	(
		CacheUpdaterMetadata metadata
	)
	{
		StringBuilder builder = new();

		_ = builder.Append($$"""
// auto-generated code

#nullable enable

namespace {{metadata.ContainingNamespaceName}};

partial class {{metadata.ContainingTypeName}}
{
	{{SyntaxFacts.GetText(metadata.MethodAccessibility)}} partial {{metadata.ReturnType.GetFullyQualifiedName()}} {{metadata.MethodName}}
	(
		{{metadata.FirstParameterType.GetFullyQualifiedName()}} {{metadata.FirstParameter}},
		{{metadata.SecondParameterType.GetFullyQualifiedName()}} {{metadata.SecondParameter}}
	)
	{
		{{metadata.TrackingStruct}} tracking = {{metadata.TrackingStruct}}.ConvertToTrackingStruct
		(
			{{metadata.FirstParameter}}
		);

""");

		foreach
		(
			IPropertySymbol symbol in metadata.FirstParameterType.GetPublicProperties()
			.Intersect
			(
				metadata.SecondParameterType.GetMembers()
				.Where
				(
					xm => xm is IPropertySymbol
					{
						DeclaredAccessibility: Accessibility.Public
					}
				),
				SymbolEqualityComparer.Default
			)
			.Cast<IPropertySymbol>()
		)
		{
			INamedTypeSymbol named = (INamedTypeSymbol)symbol.Type;

			Boolean optional = named.Arity == 1
				&& named.MetadataName == "Optional`1"
				&& named.ContainingNamespace.GetFullNamespace() == "Starnight";

#pragma warning disable IDE0045 // already unreadable as is
			if(optional)
			{
				_ = builder.Append($$"""

		if({{metadata.SecondParameter}}.{{symbol.Name}}.HasValue)
		{
			tracking = tracking with { {{symbol.Name}} = {{metadata.SecondParameter}}.{{symbol.Name}} };
		}
		
""");
			}
			else
			{
				_ = builder.Append($$"""

		tracking = tracking with { {{symbol.Name}} = {{metadata.SecondParameter}}.{{symbol.Name}} };

""");
			}
#pragma warning restore IDE0045
		}

		_ = builder.Append($$"""

		return {{metadata.TrackingStruct}}.ConvertToRecord
		(
			tracking
		);
	}
}

""");

		emitTrackingStruct
		(
			metadata,
			builder
		);

		return builder.ToString();
	}

	private static void emitTrackingStruct
	(
		CacheUpdaterMetadata metadata,
		StringBuilder builder
	)
	{
		_ = builder.Append($$"""

[global::System.CodeDom.Compiler.GeneratedCode("starnight-cache-update-generator", "0.2.0")]
file record struct {{metadata.TrackingStruct}}
{
""");

		IEnumerable<IPropertySymbol> symbols = metadata.ReturnType.GetPublicProperties();

		// properties
		foreach(IPropertySymbol symbol in symbols)
		{
			_ = builder.Append($$"""

	public {{symbol.Type.ToDisplayString()}} {{symbol.Name}} { get; set; }

""");
		}

		// functions
		//
		// cast from record to tracking struct
		{
			_ = builder.Append($$"""


	public static {{metadata.TrackingStruct}} ConvertToTrackingStruct
	(
		{{metadata.ReturnType.GetFullyQualifiedName()}} value
	)
	{
		return new()
		{

""");

			foreach(IPropertySymbol symbol in symbols)
			{
				_ = builder.Append($$"""

			{{symbol.Name}} = value.{{symbol.Name}},

""");
			}

			_ = builder.Append($$"""

		};
	}

""");
		}

		// cast from tracking struct to record
		{
			_ = builder.Append($$"""


	public static {{metadata.ReturnType.GetFullyQualifiedName()}} ConvertToRecord
	(
		{{metadata.TrackingStruct}} value
	)
	{
		return new()
		{

""");

			foreach(IPropertySymbol symbol in symbols)
			{
				_ = builder.Append($$"""

			{{symbol.Name}} = value.{{symbol.Name}},

""");
			}

			_ = builder.Append($$"""

		};
	}

""");
		}

		_ = builder.Append($$"""

}
""");
	}
}

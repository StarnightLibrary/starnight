namespace Starnight.SourceGenerators.WrapperEntities;

using System;
using System.Collections.Generic;
using System.Text;

internal static class WrapperEmitter
{
	public static String Emit
	(
		WrapperMetadata metadata
	)
	{
		StringBuilder builder = new();

		// we might need System.Linq for some extension methods later on
		_ = builder.Append(
$$"""
// auto-generated code

namespace {{metadata.ContainingNamespace}};

using System.Collections.Immutable;
using System.Linq;

#nullable enable
#pragma warning disable IDE0005
#pragma warning disable IDE0045

partial class {{metadata.TypeName}}
{
	private {{metadata.InternalType.GetFullyQualifiedName()}} internalObject;


""");

		Dictionary<String, WrapperPropertyMetadata> extractedProperties = PropertyMetadataExtractor.ExtractMetadataForType
		(
			metadata.InternalType,
			metadata.DisabledTransformations,
			metadata.Renames
		);

		// emit properties, applying transformations and renames
		foreach(KeyValuePair<String, WrapperPropertyMetadata> property in extractedProperties)
		{
			_ = builder.Append(
$$"""
	/// <inheritdoc cref="{{metadata.InternalType.GetFullyQualifiedName()}}.{{property.Key}}" />
	public {{property.Value.TypeDeclaration}} {{property.Value.NewName}} { get; internal set; }


""");
		}

		// emit to internal entity conversions (unconditionally)
		_ = builder.Append(
$$"""
	/// <inheritdoc/>
	public static explicit operator {{metadata.InternalType.GetFullyQualifiedName()}}
	(
		global::{{metadata.ContainingNamespace}}.{{metadata.TypeName}} entity
	)
		=> entity.internalObject;

	/// <inheritdoc/>
	public {{metadata.InternalType.GetFullyQualifiedName()}} ToInternalEntity()
		=> this.internalObject;


""");

		// emit from internal entity initial conversion
		_ = builder.Append(
$$"""
	/// <summary>
	/// Initializes all directly-translated properties from an internal object.
	/// <summary/>
	private void initializeFromInternalEntity
	(
		global::Starnight.StarnightClient client,
		{{metadata.InternalType.GetFullyQualifiedName()}} entity
	)
	{


""");

		foreach(KeyValuePair<String, WrapperPropertyMetadata> property in extractedProperties)
		{
			_ = builder.Append(TransformationEmitterHelper.EmitSingleProperty(property.Key, property.Value));
		}

		_ = builder.Append(
$$"""
	}


""");

		if(metadata.GenerateInterfaceImplementation)
		{
			_ = builder.Append(
$$"""
	/// <inheritdoc/>
	public static {{metadata.InternalType}} FromInternalEntity
	(
		global::Starnight.StarnightClient client,
		global::{{metadata.ContainingNamespace}}.{{metadata.TypeName}} entity
	)
	{
		global::{{metadata.ContainingNamespace}}.{{metadata.TypeName}} obj = new();

		obj.initializeFromInternalEntity
		(
			client,
			entity
		);

		return obj;
	}


""");
		}

		_ = builder.Append(
"""
}
""");

		return builder.ToString();
	}
}

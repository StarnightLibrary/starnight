namespace Starnight.SourceGenerators.WrapperEntities;

using System.Text;
using System;
using System.Collections.Generic;

using Microsoft.CodeAnalysis;

internal static class WrapperEmitter
{
	public static String Emit
	(
		WrapperMetadata metadata
	)
	{
		Dictionary<String, WrapperTransformationTypes> appliedTransformations = new();
		StringBuilder builder = new();

		_ = builder.Append(
$$"""
// auto-generated code

#nullable enable

namespace {{metadata.ContainingNamespace}};

partial class {{metadata.TypeName}}
{
	private {{metadata.InternalType.GetFullyQualifiedName()}} internalObject;

""");

		// emit properties, applying transformations and renames
		foreach(IPropertySymbol property in metadata.InternalType.GetPublicProperties())
		{
			// type transformations
			StringBuilder type = new();

			buildTypeString
			(
				type,
				(property.Type as INamedTypeSymbol)!,
				property.Name
			);

			String emittedPropertyName = metadata.Renames.TryGetValue(property.Name, out String a)
				? a
				: property.Name;

			_ = builder.Append(
$$"""
	/// <inheritdoc cref="{{metadata.InternalType.GetFullyQualifiedName()}}.{{property.Name}}" />
	public {{type}} {{emittedPropertyName}} { get; internal set; }

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

		return builder.ToString();

		// --- local helper functions --- //

		// applies type transformations as appropriate and registers them to the transformation registry
		// so we can later implement logic for them
		void buildTypeString
		(
			StringBuilder builder,
			INamedTypeSymbol symbol,
			String propertyName
		)
		{
			// transformation I: optional folding
			// do this for all optional-contained types EXCEPT Object, which is a special case
			if(symbol.MetadataName == "Optional`1" && symbol.TypeArguments[0].MetadataName != "Object")
			{
				buildTypeString
				(
					builder,
					(symbol.TypeArguments[0] as INamedTypeSymbol)!,
					propertyName
				);

				_ = builder.Append("?");

				addTransformationFlag
				(
					propertyName,
					WrapperTransformationTypes.OptionalFolding
				);

				return;
			}

			// transformation II: IEnumerable to IImmutableList
			if(symbol.MetadataName == "IEnumerable`1")
			{
				if(metadata.DisabledTransformations.Contains(propertyName))
				{
					_ = builder.Append("global::System.Collections.Generic.IEnumerable<");

					buildTypeString
					(
						builder,
						(symbol.TypeArguments[0] as INamedTypeSymbol)!,
						propertyName
					);

					_ = builder.Append(">");

					return;
				}

				_ = builder.Append("global::System.Collections.Immutable.IImmutableList<");

				buildTypeString
				(
					builder,
					(symbol.TypeArguments[0] as INamedTypeSymbol)!,
					propertyName
				);

				_ = builder.Append(">");

				addTransformationFlag
				(
					propertyName,
					WrapperTransformationTypes.ImmutableList
				);

				return;
			}

			// transformation III: IDictionary to IImmutableDictionary
			// this is actually rather complicated, because both the key and the value can be transformed
			// however, the only valid key transformation is Int64 to Snowflake, in all other cases
			// we can copy the type over
			if(symbol.MetadataName == "IDictionary`1")
			{
				if(metadata.DisabledTransformations.Contains(propertyName))
				{
					_ = builder.Append
					(
						"global::System.Collections.Generic.IDictionary<" +
						symbol.TypeArguments[0].GetFullyQualifiedName() +
						","
					);

					buildTypeString
					(
						builder,
						(symbol.TypeArguments[1] as INamedTypeSymbol)!,
						propertyName
					);

					_ = builder.Append(">");

					return;
				}

				_ = builder.Append("global::System.Collections.Immutable.IImmutableDictionary<");

				if(symbol.TypeArguments[0].MetadataName == "Int64")
				{
					_ = builder.Append("global::Starnight.Snowflake,");

					addTransformationFlag
					(
						propertyName,
						WrapperTransformationTypes.DictionaryKeyTransformation
					);
				}
				else
				{
					_ = builder.Append(symbol.TypeArguments[0].GetFullyQualifiedName() + ",");
				}

				buildTypeString
				(
					builder,
					(symbol.TypeArguments[1] as INamedTypeSymbol)!,
					propertyName
				);

				_ = builder.Append(">");

				addTransformationFlag
				(
					propertyName,
					WrapperTransformationTypes.ImmutableDictionary
				);

				return;
			}

			// transformation IV: Int64 to Snowflake
			if(symbol.MetadataName == "Int64")
			{
				_ = builder.Append("global::Starnight.Snowflake");

				addTransformationFlag
				(
					propertyName,
					WrapperTransformationTypes.SnowflakeConversion
				);

				return;
			}

			// transformation V: record-type Discord-prefixed objects to their Starnight equivalent
			if(symbol.MetadataName.StartsWith("Discord") && symbol.IsRecord)
			{
				_ = builder.Append($"global::Starnight.Entities.{symbol.Name.Replace("Discord", "Starnight")}");

				addTransformationFlag
				(
					propertyName,
					WrapperTransformationTypes.EntityTransformation
				);

				if(symbol.NullableAnnotation == NullableAnnotation.Annotated)
				{
					_ = builder.Append("?");
				}

				return;
			}

			// transformation VI: discord's atrocious optional booleans
			if(symbol.MetadataName == "Optional`1" && symbol.TypeArguments[0].MetadataName == "Object")
			{
				_ = builder.Append("global::System.Boolean");

				addTransformationFlag
				(
					propertyName,
					WrapperTransformationTypes.NullBoolean
				);

				return;
			}

			// conservation I: NVT
			if(symbol.MetadataName == "Nullable`1")
			{
				_ = builder.Append($"{symbol.TypeArguments[0].GetFullyQualifiedName()}?");

				return;
			}

			// conservation II: everything else
			if(symbol.IsValueType)
			{
				_ = symbol.NullableAnnotation == NullableAnnotation.Annotated
					? builder.Append($"{symbol.GetFullyQualifiedName()}?")
					: builder.Append(symbol.GetFullyQualifiedName());
			}
		}

		void addTransformationFlag
		(
			String propertyName,
			WrapperTransformationTypes type
		)
		{
			if(appliedTransformations.ContainsKey(propertyName))
			{
				appliedTransformations[propertyName] |= type;
			}
			else
			{
				appliedTransformations.Add
				(
					propertyName,
					type
				);
			}
		}
	}
}

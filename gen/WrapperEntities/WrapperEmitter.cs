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

		foreach(IPropertySymbol property in metadata.InternalType.GetPublicProperties())
		{
			String propertyName = metadata.Renames.TryGetValue(property.Name, out String a)
				? a
				: property.Name;

			// no transformations applied
			if(!appliedTransformations.TryGetValue(property.Name, out WrapperTransformationTypes transformations))
			{
				_ = builder.Append(
$$"""
		this.{{propertyName}} = entity.{{property.Name}} ?? null!;

""");
			}

			// only optional transformation applied or optional + snowflake transformations applied
			if
			(
				transformations == WrapperTransformationTypes.OptionalFolding
				|| transformations ==
				(
					WrapperTransformationTypes.SnowflakeConversion
					| WrapperTransformationTypes.OptionalFolding
				)
			)
			{
				_ = builder.Append(
$$"""
		this.{{propertyName}} = entity.{{property.Name}}.HasValue
			? entity.{{property.Name}}.Value
			: null;

""");	
			}

			// pure snowflake transformation
			if(transformations == WrapperTransformationTypes.SnowflakeConversion)
			{
				_ = builder.Append(
$$"""
		this.{{propertyName}} = entity.{{property.Name}} ?? null!;

""");
			}

			// discords weird booleans
			if(transformations == WrapperTransformationTypes.NullBoolean)
			{
				_ = builder.Append(
$$"""
		this.{{propertyName}} = entity.{{property.Name}}.HasValue;

""");
			}

			// pure entity transformation
			if(transformations == WrapperTransformationTypes.EntityTransformation)
			{
				_ = builder.Append(
$$"""
		this.{{propertyName}} = global::Starnight.Entities.{{metadata.InternalType.Name.Replace("Discord", "Starnight")}}.FromInternalEntity
		(
			client,
			entity.{{property.Name}}
		);

""");
			}

			// optional + entity transformation
			if
			(
				transformations ==
				(
					WrapperTransformationTypes.EntityTransformation
					| WrapperTransformationTypes.OptionalFolding
				)
			)
			{
				_ = builder.Append(
$$"""
		this.{{propertyName}} = entity.{{property.Name}}.IsDefined
			? global::Starnight.Entities.{{metadata.InternalType.Name.Replace("Discord", "Starnight")}}.FromInternalEntity
			(
				client,
				entity.{{property.Name}}.Value
			)
			: null;

""");
			}

			// immutable list
			if(transformations == WrapperTransformationTypes.ImmutableList)
			{
				_ = builder.Append(
$$"""
		if(entity.{{property.Name}} is not null)
		{
			this.{{propertyName}} = client.CollectionTransformer.TransformImmutableList
			<
				{{metadata.InternalType.Name}},
				{{metadata.TypeName}}
			>
			(
				entity.{{property.Name}}
			);
		}
		else
		{
			this.{{propertyName}} = null;
		}

""");
			}

			// immutable dictionary
			if
			(
				transformations == WrapperTransformationTypes.ImmutableDictionary
				|| transformations ==
				(
					WrapperTransformationTypes.ImmutableDictionary
					| WrapperTransformationTypes.DictionaryKeyTransformation
				)
			)
			{
				// calculate the output dictionary type first
				StringBuilder output = new();

				buildTypeString
				(
					output,
					((property.Type as INamedTypeSymbol)!.TypeArguments[1] as INamedTypeSymbol)!,
					// String.Empty because we don't actually care to register any flags at this point
					// in the process
					String.Empty
				);

				_ = builder.Append(
$$"""
		if(entity.{{property.Name}} is not null)
		{
			this.{{propertyName}} = client.CollectionTransformer.TransformImmutableDictionary
			<
				{{(property.Type as INamedTypeSymbol)!.TypeArguments[0].GetFullyQualifiedName()}},
				{{(property.Type as INamedTypeSymbol)!.TypeArguments[1].GetFullyQualifiedName()}},
				{{(
					transformations.HasFlag(WrapperTransformationTypes.DictionaryKeyTransformation)
						? "global::Starnight.Snowflake"
						: (property.Type as INamedTypeSymbol)!.TypeArguments[0].GetFullyQualifiedName()
				)}},
				{{output}}
			>
			(
				entity.{{property.Name}}
			);
		}
		else
		{
			this.{{propertyName}} = null;
		}

""");
			}

			// optional immutable list
			if(transformations == (WrapperTransformationTypes.OptionalFolding | WrapperTransformationTypes.ImmutableList))
			{
				_ = builder.Append(
$$"""
		if(entity.{{property.Name}}.IsDefined)
		{
			this.{{propertyName}} = client.CollectionTransformer.TransformImmutableList
			<
				{{metadata.InternalType.Name}},
				{{metadata.TypeName}}
			>
			(
				entity.{{property.Name}}.Value
			);
		}
		else
		{
			this.{{propertyName}} = null;
		}

""");
			}

			// optional immutable dictionary
			if(transformations == (WrapperTransformationTypes.OptionalFolding | WrapperTransformationTypes.ImmutableDictionary))
			{
				// calculate the output dictionary type first
				StringBuilder output = new();

				buildTypeString
				(
					output,
					((property.Type as INamedTypeSymbol)!.TypeArguments[1] as INamedTypeSymbol)!,
					// String.Empty because we don't actually care to register any flags at this point
					// in the process
					String.Empty
				);

				_ = builder.Append(
$$"""
		if(entity.{{property.Name}}.IsDefined)
		{
			this.{{propertyName}} = client.CollectionTransformer.TransformImmutableDictionary
			<
				{{(property.Type as INamedTypeSymbol)!.TypeArguments[0].GetFullyQualifiedName()}},
				{{(property.Type as INamedTypeSymbol)!.TypeArguments[1].GetFullyQualifiedName()}},
				{{(
					transformations.HasFlag(WrapperTransformationTypes.DictionaryKeyTransformation)
						? "global::Starnight.Snowflake"
						: (property.Type as INamedTypeSymbol)!.TypeArguments[0].GetFullyQualifiedName()
				)}},
				{{output}}
			>
			(
				entity.{{property.Name}}.Value
			);
		}
		else
		{
			this.{{propertyName}} = null;
		}

""");
			}
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

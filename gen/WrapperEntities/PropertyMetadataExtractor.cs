namespace Starnight.SourceGenerators.WrapperEntities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.CodeAnalysis;

internal static class PropertyMetadataExtractor
{
	public static Dictionary<String, WrapperPropertyMetadata> ExtractMetadataForType
	(
		INamedTypeSymbol type,
		IEnumerable<String> disabledTransformations,
		IDictionary<String, String> renames
	)
	{
		Dictionary<String, WrapperPropertyMetadata> dict = new();

		foreach(IPropertySymbol property in type.GetPublicProperties())
		{
			StringBuilder typename = new(), intermediary = new();
			WrapperTransformations transformations = new();

			extractRoot
			(
				typename,
				intermediary,
				transformations,
				(property.Type as INamedTypeSymbol)!,
				disabledTransformations.Contains(property.Name)
			);

			String newName = renames.TryGetValue(property.Name, out String name)
				? name
				: property.Name;

			transformations.Reset();

			WrapperPropertyMetadata metadata = new()
			{
				AppliedTransformations = transformations,
				TypeDeclaration = typename.ToString(),
				IntermediaryTypeDeclaration = intermediary.ToString(),
				NewName = newName,
				InternalType = property.Type.GetFullyQualifiedName()
			};

			if
			(
				(
					transformations[0] == WrapperTransformationType.ImmutableDictionary
					|| transformations[1] == WrapperTransformationType.ImmutableDictionary
				)
				&&
				(
					transformations[1] == WrapperTransformationType.Records
					|| transformations[2] == WrapperTransformationType.Records
					|| transformations[3] == WrapperTransformationType.Records
				)
			)
			{
				ITypeSymbol firstType = (property.Type as INamedTypeSymbol)!.TypeArguments[0];
				ITypeSymbol secondType = (property.Type as INamedTypeSymbol)!.TypeArguments[1];

				DictionaryTransformationMetadata dictionaryMetadata = new()
				{
					InternalKey = firstType.GetFullyQualifiedName(),
					InternalValue = secondType.GetFullyQualifiedName(),
					WrapperKey = firstType.Name == "Int64"
						? "global::Starnight.Snowflake"
						: secondType.GetFullyQualifiedName(),
					WrapperValue = secondType.Name == "Int64"
						? "global::Starnight.Snowflake"
						: secondType.IsRecord
						&& secondType.Name.StartsWith("Discord")
							? "global::Starnight.Entities." +
							secondType.Name.Replace
							(
								"Discord",
								"Starnight"
							)
							: secondType.GetFullyQualifiedName()
				};

				metadata.DictionaryMetadata = dictionaryMetadata;
			}


			dict.Add
			(
				property.Name,
				metadata
			);
		}

		return dict;
	}

	private static void extractRoot
	(
		StringBuilder typename,
		StringBuilder intermediary,
		WrapperTransformations transformations,
		INamedTypeSymbol symbol,
		Boolean collectionsDisabled
	)
	{
		if(symbol.MetadataName == "Optional`1" && symbol.TypeArguments[0].Name != "Object")
		{
			extractOptionalFolding
			(
				typename,
				intermediary,
				transformations,
				symbol,
				collectionsDisabled
			);
		}
		else if(symbol.MetadataName == "IEnumerable`1")
		{
			extractEnumerable
			(
				typename,
				intermediary,
				transformations,
				symbol,
				collectionsDisabled
			);
		}
		else if(symbol.MetadataName == "IDictionary`1")
		{
			extractDictionary
			(
				typename,
				intermediary,
				transformations,
				symbol,
				collectionsDisabled
			);
		}
		else if(symbol.MetadataName == "Int64")
		{
			extractSnowflake
			(
				typename,
				intermediary,
				transformations,
				symbol
			);
		}
		else if(symbol.MetadataName == "Optional`1")
		{
			extractNullableBoolean
			(
				typename,
				transformations
			);
		}
		else if(symbol.IsRecord && symbol.Name.Contains("Discord"))
		{
			extractRecord
			(
				typename,
				intermediary,
				transformations,
				symbol
			);
		}
		else if(symbol.MetadataName == "Nullable`1")
		{
			conserveNullableValueType
			(
				typename,
				intermediary,
				transformations,
				(symbol.TypeArguments[0] as INamedTypeSymbol)!
			);
		}
		else
		{
			conserveGeneral
			(
				typename,
				intermediary,
				transformations,
				symbol
			);
		}
	}

	private static void extractOptionalFolding
	(
		StringBuilder typename,
		StringBuilder intermediary,
		WrapperTransformations transformations,
		INamedTypeSymbol symbol,
		Boolean collectionsDisabled
	)
	{
		transformations.Current = WrapperTransformationType.OptionalFolding;
		transformations.MoveNext();

		extractRoot
		(
			typename,
			intermediary,
			transformations,
			(symbol.TypeArguments[0] as INamedTypeSymbol)!,
			collectionsDisabled
		);

		if(typename.ToString().Last() != '?')
		{
			_ = typename.Append("?");
		}
	}

	private static void extractEnumerable
	(
		StringBuilder typename,
		StringBuilder intermediary,
		WrapperTransformations transformations,
		INamedTypeSymbol symbol,
		Boolean collectionsDisabled
	)
	{
		if(collectionsDisabled)
		{
			transformations.Current = WrapperTransformationType.ConservationEnumerable;
			transformations.MoveNext();

			_ = typename.Append("global::System.Collections.Generic.IReadOnlyList<");

			extractRoot
			(
				typename,
				intermediary,
				transformations,
				(symbol.TypeArguments[0] as INamedTypeSymbol)!,
				collectionsDisabled
			);

			_ = typename.Append(">");

			return;
		}

		transformations.Current = WrapperTransformationType.ImmutableList;
		transformations.MoveNext();

		// we use list as a backing type for intermediary conversions and call .ToImmutableList later
		_ = typename.Append("global::System.Collections.Immutable.IImmutableList<");

		extractRoot
		(
			typename,
			intermediary,
			transformations,
			(symbol.TypeArguments[0] as INamedTypeSymbol)!,
			collectionsDisabled
		);

		_ = typename.Append(">");
	}

	private static void extractDictionary
	(
		StringBuilder typename,
		StringBuilder intermediary,
		WrapperTransformations transformations,
		INamedTypeSymbol symbol,
		Boolean collectionsDisabled
	)
	{
		if(collectionsDisabled)
		{
			transformations.Current = WrapperTransformationType.ConservationDictionary;
			transformations.MoveNext();

			_ = typename.Append("global::System.Collections.Generic.IDictionary<");

			if(symbol.TypeArguments[0].Name == "Int64")
			{
				transformations.Current = WrapperTransformationType.DictionaryKey;
				transformations.MoveNext();

				_ = typename.Append("global::Starnight.Snowflake, ");
			}
			else
			{
				_ = typename.Append(symbol.TypeArguments[0].GetFullyQualifiedName() + ",");
			}

			extractRoot
			(
				typename,
				intermediary,
				transformations,
				(symbol.TypeArguments[1] as INamedTypeSymbol)!,
				collectionsDisabled
			);

			_ = typename.Append(">");

			return;
		}

		transformations.Current = WrapperTransformationType.ImmutableDictionary;
		transformations.MoveNext();

		_ = typename.Append("global::System.Collections.Generic.IImmutableDictionary<");

		if(symbol.TypeArguments[0].Name == "Int64")
		{
			transformations.Current = WrapperTransformationType.DictionaryKey;
			transformations.MoveNext();

			_ = typename.Append("global::Starnight.Snowflake, ");
		}
		else
		{
			_ = typename.Append(symbol.TypeArguments[0].GetFullyQualifiedName() + ", ");
		}

		extractRoot
		(
			typename,
			intermediary,
			transformations,
			(symbol.TypeArguments[1] as INamedTypeSymbol)!,
			collectionsDisabled
		);

		_ = typename.Append(">");

		return;
	}

	private static void extractSnowflake
	(
		StringBuilder typename,
		StringBuilder intermediary,
		WrapperTransformations transformations,
		INamedTypeSymbol symbol
	)
	{
		transformations.Current = WrapperTransformationType.Snowflake;
		transformations.MoveNext();

		_ = typename.Append("global::Starnight.Snowflake");
		_ = intermediary.Append("global::Starnight.Snowflake");

		if(symbol.NullableAnnotation == NullableAnnotation.Annotated)
		{
			_ = typename.Append("?");
			_ = intermediary.Append("?");
		}
	}

	private static void extractNullableBoolean
	(
		StringBuilder typename,
		WrapperTransformations transformations
	)
	{
		transformations.Current = WrapperTransformationType.NullableBoolean;
		transformations.MoveNext();

		_ = typename.Append("global::System.Boolean");
	}

	private static void extractRecord
	(
		StringBuilder typename,
		StringBuilder intermediary,
		WrapperTransformations transformations,
		INamedTypeSymbol symbol
	)
	{
		transformations.Current = WrapperTransformationType.Records;
		transformations.MoveNext();

		_ = typename.Append
		(
			"global::Starnight.Entities." +
			symbol.Name.Replace
			(
				"Discord",
				"Starnight"
			)
		);
		_ = intermediary.Append
		(
			"global::Starnight.Entities." +
			symbol.Name.Replace
			(
				"Discord",
				"Starnight"
			)
		);
	}

	private static void conserveNullableValueType
	(
		StringBuilder typename,
		StringBuilder intermediary,
		WrapperTransformations transformations,
		INamedTypeSymbol symbol
	)
	{
		transformations.Current = WrapperTransformationType.ConservationNullableValueType;
		transformations.MoveNext();

		extractRoot
		(
			typename,
			intermediary,
			transformations,
			symbol,
			false
		);

		_ = typename.Append("?");
		_ = intermediary.Append("?");
	}

	private static void conserveGeneral
	(
		StringBuilder typename,
		StringBuilder intermediary,
		WrapperTransformations transformations,
		INamedTypeSymbol symbol
	)
	{
		transformations.Current = WrapperTransformationType.ConservationGeneral;
		transformations.MoveNext();

		String type = symbol.NullableAnnotation == NullableAnnotation.Annotated
			? symbol.GetFullyQualifiedName() + "?"
			: symbol.GetFullyQualifiedName();

		_ = typename.Append(type);
		_ = intermediary.Append(type);
	}
}

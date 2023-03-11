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
				ref transformations,
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
		ref WrapperTransformations transformations,
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
				ref transformations,
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
				ref transformations,
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
				ref transformations,
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
				ref transformations,
				symbol
			);
		}
		else if(symbol.MetadataName == "Optional`1")
		{
			extractNullableBoolean
			(
				typename,
				ref transformations
			);
		}
		else if(symbol.IsRecord && symbol.Name.Contains("Discord"))
		{
			extractRecord
			(
				typename,
				intermediary,
				ref transformations,
				symbol
			);
		}
		else if(symbol.MetadataName == "Nullable`1")
		{
			conserveNullableValueType
			(
				typename,
				intermediary,
				ref transformations,
				(symbol.TypeArguments[0] as INamedTypeSymbol)!
			);
		}
		else
		{
			conserveGeneral
			(
				typename,
				intermediary,
				ref transformations,
				symbol
			);
		}
	}

	private static void extractOptionalFolding
	(
		StringBuilder typename,
		StringBuilder intermediary,
		ref WrapperTransformations transformations,
		INamedTypeSymbol symbol,
		Boolean collectionsDisabled
	)
	{

		_ = transformations.MoveNext();
		transformations.Current = WrapperTransformationType.OptionalFolding;

		extractRoot
		(
			typename,
			intermediary,
			ref transformations,
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
		ref WrapperTransformations transformations,
		INamedTypeSymbol symbol,
		Boolean collectionsDisabled
	)
	{
		if(collectionsDisabled)
		{
			_ = transformations.MoveNext();
			transformations.Current = WrapperTransformationType.ConservationEnumerable;

			_ = typename.Append("global::System.Collections.Generic.IReadOnlyList<");

			extractRoot
			(
				typename,
				intermediary,
				ref transformations,
				(symbol.TypeArguments[0] as INamedTypeSymbol)!,
				collectionsDisabled
			);

			_ = typename.Append(">");

			return;
		}

		_ = transformations.MoveNext();
		transformations.Current = WrapperTransformationType.ImmutableList;

		// we use list as a backing type for intermediary conversions and call .ToImmutableList later
		_ = typename.Append("global::System.Collections.Immutable.IImmutableList<");
		_ = intermediary.Append("global::System.Collections.Generic.List<");

		extractRoot
		(
			typename,
			intermediary,
			ref transformations,
			(symbol.TypeArguments[0] as INamedTypeSymbol)!,
			collectionsDisabled
		);

		_ = typename.Append(">");
		_ = intermediary.Append(">");
	}

	private static void extractDictionary
	(
		StringBuilder typename,
		StringBuilder intermediary,
		ref WrapperTransformations transformations,
		INamedTypeSymbol symbol,
		Boolean collectionsDisabled
	)
	{
		if(collectionsDisabled)
		{
			_ = transformations.MoveNext();
			transformations.Current = WrapperTransformationType.ConservationDictionary;

			_ = typename.Append("global::System.Collections.Generic.IDictionary<");
			_ = intermediary.Append("global::System.Collections.Generic.Dictionary<");

			if(symbol.TypeArguments[0].Name == "Int64")
			{
				_ = transformations.MoveNext();
				transformations.Current = WrapperTransformationType.DictionaryKey;

				_ = typename.Append("global::Starnight.Snowflake, ");
				_ = intermediary.Append("global::Starnight.Snowflake, ");
			}
			else
			{
				_ = typename.Append(symbol.TypeArguments[0].GetFullyQualifiedName() + ",");
				_ = intermediary.Append(symbol.TypeArguments[0].GetFullyQualifiedName() + ",");
			}

			extractRoot
			(
				typename,
				intermediary,
				ref transformations,
				(symbol.TypeArguments[1] as INamedTypeSymbol)!,
				collectionsDisabled
			);

			_ = typename.Append(">");
			_ = typename.Append(">");

			return;
		}

		_ = transformations.MoveNext();
		transformations.Current = WrapperTransformationType.ImmutableDictionary;

		_ = typename.Append("global::System.Collections.Generic.IImmutableDictionary<");
		_ = intermediary.Append("global::System.Collections.Generic.Dictionary<");

		if(symbol.TypeArguments[0].Name == "Int64")
		{
			_ = transformations.MoveNext();
			transformations.Current = WrapperTransformationType.DictionaryKey;

			_ = typename.Append("global::Starnight.Snowflake, ");
			_ = intermediary.Append("global::Starnight.Snowflake, ");
		}
		else
		{
			_ = typename.Append(symbol.TypeArguments[0].GetFullyQualifiedName() + ", ");
			_ = intermediary.Append(symbol.TypeArguments[0].GetFullyQualifiedName() + ", ");
		}

		extractRoot
		(
			typename,
			intermediary,
			ref transformations,
			(symbol.TypeArguments[1] as INamedTypeSymbol)!,
			collectionsDisabled
		);

		_ = typename.Append(">");
		_ = typename.Append(">");

		return;
	}

	private static void extractSnowflake
	(
		StringBuilder typename,
		StringBuilder intermediary,
		ref WrapperTransformations transformations,
		INamedTypeSymbol symbol
	)
	{
		_ = transformations.MoveNext();
		transformations.Current = WrapperTransformationType.Snowflake;

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
		ref WrapperTransformations transformations
	)
	{
		_ = transformations.MoveNext();
		transformations.Current = WrapperTransformationType.NullableBoolean;

		_ = typename.Append("global::System.Boolean");
	}

	private static void extractRecord
	(
		StringBuilder typename,
		StringBuilder intermediary,
		ref WrapperTransformations transformations,
		INamedTypeSymbol symbol
	)
	{
		_ = transformations.MoveNext();
		transformations.Current = WrapperTransformationType.Records;

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
		ref WrapperTransformations transformations,
		INamedTypeSymbol symbol
	)
	{
		_ = transformations.MoveNext();
		transformations.Current = WrapperTransformationType.ConservationNullableValueType;

		_ = typename.Append(symbol.GetFullyQualifiedName() + "?");
		_ = intermediary.Append(symbol.GetFullyQualifiedName() + "?");
	}

	private static void conserveGeneral
	(
		StringBuilder typename,
		StringBuilder intermediary,
		ref WrapperTransformations transformations,
		INamedTypeSymbol symbol
	)
	{
		_ = transformations.MoveNext();
		transformations.Current = WrapperTransformationType.ConservationGeneral;

		String type = symbol.NullableAnnotation == NullableAnnotation.Annotated
			? symbol.GetFullyQualifiedName() + "?"
			: symbol.GetFullyQualifiedName();

		_ = typename.Append(type);
		_ = intermediary.Append(type);
	}
}

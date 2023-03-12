namespace Starnight.SourceGenerators.WrapperEntities;

using System;
using System.Text;

#pragma warning disable IDE0045

internal static class TransformationEmitterHelper
{
	public static String EmitSingleProperty
	(
		String internalName,
		WrapperPropertyMetadata metadata
	)
	{
		StringBuilder builder = new();

		emitPropertyRoot
		(
			internalName,
			metadata,
			builder
		);

		return builder.ToString();
	}

	private static void emitPropertyRoot
	(
		String internalName,
		WrapperPropertyMetadata metadata,
		StringBuilder output
	)
	{
		switch(metadata.AppliedTransformations[0])
		{
			case WrapperTransformationType.ConservationGeneral:
			case WrapperTransformationType.ConservationNullableValueType:
			case WrapperTransformationType.Snowflake:

				emitSingleConservation
				(
					internalName,
					metadata,
					output
				);
				break;

			case WrapperTransformationType.NullableBoolean:

				emitNullableBoolean
				(
					internalName,
					metadata,
					output
				);
				break;

			case WrapperTransformationType.OptionalFolding:

				emitOptionalFolding
				(
					internalName,
					metadata,
					output
				);
				break;

			case WrapperTransformationType.ImmutableList:

				emitImmutableList
				(
					internalName,
					metadata,
					output
				);
				break;

			case WrapperTransformationType.ImmutableDictionary:

				emitImmutableDictionary
				(
					internalName,
					metadata,
					output
				);
				break;

			case WrapperTransformationType.Records:

				emitTransformation
				(
					internalName,
					metadata,
					output
				);
				break;
		}
	}

	// this is a single pass and cannot be anything further
	private static void emitNullableBoolean
	(
		String internalName,
		WrapperPropertyMetadata metadata,
		StringBuilder output
	)
	{
		_ = output.Append(
$$"""
			this.{{metadata.NewName}} = entity.{{internalName}}.HasValue;


""");
	}

	private static void emitSingleConservation
	(
		String internalName,
		WrapperPropertyMetadata metadata,
		StringBuilder output
	)
	{
		_ = output.Append(
$$"""
		this.{{metadata.NewName}} = entity.{{internalName}};


""");
	}

	private static void emitImmutableList
	(
		String internalName,
		WrapperPropertyMetadata metadata,
		StringBuilder output
	)
	{
		_ = output.Append(
$$"""
		if(entity.{{internalName}} is not null)
		{

""");
		
		if(metadata.AppliedTransformations.Next == WrapperTransformationType.Records)
		{
			_ = output.Append(
$$"""
			this.{{metadata.NewName}} = client.CollectionTransformer.TransformImmutableList
			<
				{{metadata.InternalType.GetFurthestWrappedTypeParameter()}},
				{{metadata.IntermediaryTypeDeclaration}}
			>
			(
				entity.{{internalName}}
			);

""");
		}
		else
		{
			_ = output.Append(
$$"""
			this.{{metadata.NewName}} = entity.{{internalName}}
				.Cast<{{metadata.IntermediaryTypeDeclaration}}>()
				.ToImmutableList();

""");
		}

		_ = output.Append(
"""
		}


""");
	}

	private static void emitImmutableDictionary
	(
		String internalName,
		WrapperPropertyMetadata metadata,
		StringBuilder output
	)
	{
		_ = output.Append(
$$"""
		if(entity.{{internalName}} is not null)
		{

""");

		if
		(
			metadata.AppliedTransformations.Next == WrapperTransformationType.Records
			|| metadata.AppliedTransformations.DictionaryNext == WrapperTransformationType.Records
		)
		{
			_ = output.Append(
$$"""
			this.{{metadata.NewName}} = client.CollectionTransformer.TransformImmutableDictionary
			<
				{{metadata.DictionaryMetadata!.Value.InternalKey}},
				{{metadata.DictionaryMetadata!.Value.InternalValue}},
				{{metadata.DictionaryMetadata!.Value.WrapperKey}},
				{{metadata.DictionaryMetadata!.Value.WrapperValue}}
			>
			(
				entity.{{internalName}}
			);

""");
		}
		else
		{
			_ = output.Append(
$$"""
			this.{{metadata.NewName}} = global::System.Runtime.CompilerServices.Unsafe.As
			<
				global::System.Collections.Generic.Dictionary
				<
					{{metadata.DictionaryMetadata!.Value.WrapperKey}},
					{{metadata.DictionaryMetadata!.Value.WrapperValue}}
				>
			>
			(
				entity.{{internalName}}
			)
			.ToImmutableDictionary();

""");
		}

		_ = output.Append(
"""
		}


""");
	}

	private static void emitTransformation
	(
		String internalName,
		WrapperPropertyMetadata metadata,
		StringBuilder output
	)
	{
		_ = output.Append(
$$"""
		if(entity.{{internalName}} is not null)
		{
			this.{{metadata.NewName}} = {{metadata.IntermediaryTypeDeclaration}}.FromInternalEntity
			(
				client,
				entity.{{internalName}}
			);
		}
		else
		{
			this.{{metadata.NewName}} = null;
		}


""");
	}

	private static void emitOptionalFolding
	(
		String internalName,
		WrapperPropertyMetadata metadata,
		StringBuilder output
	)
	{
		if(metadata.AppliedTransformations.Next == WrapperTransformationType.None)
		{
			_ = output.Append(
$$"""
		this.{{metadata.NewName}} = entity.{{internalName}}.IsDefined
			? entity.{{internalName}}.Value
			: null;


""");

			return;
		}

		_ = output.Append(
$$"""
		if(entity.{{internalName}}.IsDefined)
		{

""");

		metadata.AppliedTransformations.MoveNext();

		switch(metadata.AppliedTransformations.Current)
		{
			case WrapperTransformationType.ImmutableList:

				if(metadata.AppliedTransformations.Next == WrapperTransformationType.Records)
				{
					_ = output.Append(
$$"""
			this.{{metadata.NewName}} = client.CollectionTransformer.TransformImmutableList
			<
				{{metadata.InternalType.GetFurthestWrappedTypeParameter()}},
				{{metadata.IntermediaryTypeDeclaration}}
			>
			(
				entity.{{internalName}}.Value
			);

""");
				}
				else
				{
					_ = output.Append(
$$"""
			this.{{metadata.NewName}} = entity.{{internalName}}.Value
				.Cast<{{metadata.IntermediaryTypeDeclaration}}>()
				.ToImmutableList();

""");
				}

				break;

			case WrapperTransformationType.ImmutableDictionary:

				if
				(
					metadata.AppliedTransformations.Next == WrapperTransformationType.Records
					|| metadata.AppliedTransformations.DictionaryNext == WrapperTransformationType.Records
				)
				{
					_ = output.Append(
$$"""
			this.{{metadata.NewName}} = client.CollectionTransformer.TransformImmutableDictionary
			<
				{{metadata.DictionaryMetadata!.Value.InternalKey}},
				{{metadata.DictionaryMetadata!.Value.InternalValue}},
				{{metadata.DictionaryMetadata!.Value.WrapperKey}},
				{{metadata.DictionaryMetadata!.Value.WrapperValue}}
			>
			(
				entity.{{internalName}}.Value
			);

""");
				}
				else
				{
					_ = output.Append(
$$"""
			this.{{metadata.NewName}} = global::System.Runtime.CompilerServices.Unsafe.As
			<
				global::System.Collections.Generic.Dictionary
				<
					{{metadata.DictionaryMetadata!.Value.WrapperKey}},
					{{metadata.DictionaryMetadata!.Value.WrapperValue}}
				>
			>
			(
				entity.{{internalName}}.Value
			)
			.ToImmutableDictionary();

""");
				}

				break;

			case WrapperTransformationType.Snowflake:

				_ = output.Append(
$$"""
			this.{{metadata.NewName}} = entity.{{internalName}}.Value;

""");

				break;

			case WrapperTransformationType.Records:

				_ = output.Append(
$$"""
			this.{{metadata.NewName}} = {{metadata.IntermediaryTypeDeclaration}}.FromInternalEntity
			(
				client,
				entity.{{internalName}}.Value
			);

""");

				break;

			case WrapperTransformationType.ConservationDictionary:

				_ = output.Append(
$$"""
			this.{{metadata.NewName}} = global::System.Runtime.CompilerServices.Unsafe.As
			<
				global::System.Collections.Generic.Dictionary
				<
					{{metadata.DictionaryMetadata!.Value.WrapperKey}},
					{{metadata.DictionaryMetadata!.Value.WrapperValue}}
				>
			>
			(
				entity.{{internalName}}.Value
			)
			.ToImmutableDictionary();

""");

				break;

			case WrapperTransformationType.ConservationEnumerable:

				_ = output.Append(
$$"""
			this.{{metadata.NewName}} = global::System.Runtime.CompilerServices.Unsafe.As
			<
				{{metadata.IntermediaryTypeDeclaration}}[]
			>
			(
				entity.{{internalName}}.Value.ToArray()
			)
			.ToImmutableList();

""");

				break;

			case WrapperTransformationType.ConservationGeneral:
			case WrapperTransformationType.ConservationNullableValueType:

				_ = output.Append(
$$"""
			this.{{metadata.NewName}} = entity.{{internalName}}.Value;

""");

				break;
		}

		_ = output.Append(
$$"""
		}
		else
		{
			this.{{metadata.NewName}} = null;
		}


""");
	}
}

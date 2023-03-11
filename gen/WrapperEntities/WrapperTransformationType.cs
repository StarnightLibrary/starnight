namespace Starnight.SourceGenerators.WrapperEntities;

using System;

internal enum WrapperTransformationType : Byte
{
	None,
	OptionalFolding,
	ImmutableList,
	ImmutableDictionary,
	DictionaryKey,
	Snowflake,
	NullableBoolean,
	Records,
	ConservationNullableValueType,
	ConservationGeneral,
	ConservationEnumerable,
	ConservationDictionary
}

namespace Starnight.SourceGenerators.WrapperEntities;

using System;

[Flags]
internal enum WrapperTransformationTypes
{
	OptionalFolding = 1 << 0,
	ImmutableList = 1 << 1,
	ImmutableDictionary = 1 << 2,
	SnowflakeConversion = 1 << 3,
	EntityTransformation = 1 << 4,
	NullBoolean = 1 << 5,
	DictionaryKeyTransformation = 1 << 6
}

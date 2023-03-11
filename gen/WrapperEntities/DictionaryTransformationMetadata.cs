namespace Starnight.SourceGenerators.WrapperEntities;

using System;

internal record struct DictionaryTransformationMetadata
{
	public String InternalKey { get; set; }

	public String InternalValue { get; set; }

	public String WrapperKey { get; set; }

	public String WrapperValue { get; set; }
}

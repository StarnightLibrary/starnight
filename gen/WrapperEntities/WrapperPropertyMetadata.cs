namespace Starnight.SourceGenerators.WrapperEntities;

using System;

internal class WrapperPropertyMetadata
{
	public String NewName { get; set; } = null!;

	public WrapperTransformations AppliedTransformations { get; set; }

	public String TypeDeclaration { get; set; } = null!;

	public String? IntermediaryTypeDeclaration { get; set; } = null;

	public String InternalType { get; set; } = null!;

	public DictionaryTransformationMetadata? DictionaryMetadata { get; set; } = null!;
}

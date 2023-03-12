namespace Starnight.SourceGenerators.WrapperEntities;

using System;

using Microsoft.CodeAnalysis;

internal class WrapperPropertyMetadata
{
	public String NewName { get; set; } = null!;

	public WrapperTransformations AppliedTransformations { get; set; } = null!;

	public String TypeDeclaration { get; set; } = null!;

	public String? IntermediaryTypeDeclaration { get; set; } = null;

	public INamedTypeSymbol InternalType { get; set; } = null!;

	public DictionaryTransformationMetadata? DictionaryMetadata { get; set; } = null!;
}

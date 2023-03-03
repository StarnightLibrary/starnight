namespace Starnight.SourceGenerators.WrapperEntities;

using System;
using System.Collections.Generic;

using Microsoft.CodeAnalysis;

internal record WrapperMetadata
{
	public String ContainingNamespace { get; set; } = null!;
	public String TypeName { get; set; } = null!;
	public List<String> DisabledTransformations { get; set; } = null!;
	public Dictionary<String, String> Renames { get; set; } = null!;
	public Boolean GenerateInterfaceImplementation { get; set; } = false;
	public INamedTypeSymbol InternalType { get; set; } = null!;
}

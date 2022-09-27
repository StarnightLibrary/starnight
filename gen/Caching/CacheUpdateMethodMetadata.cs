namespace Starnight.SourceGenerators.Caching;

using System;

using Microsoft.CodeAnalysis;

internal struct CacheUpdateMethodMetadata
{
	public String ContainingTypeName { get; set; }

	public String MethodName { get; set; }

	public ITypeSymbol CachedType { get; set; }
}

namespace Starnight.SourceGenerators.Caching;

using System;

using Microsoft.CodeAnalysis;

internal record struct CacheUpdaterMetadata
{
	public String ContainingTypeName { get; set; }
	public String ContainingNamespaceName { get; set; }
	public String MethodName { get; set; }
	public Accessibility MethodAccessibility { get; set; }
	public ITypeSymbol ReturnType { get; set; }
	public ITypeSymbol FirstParameterType { get; set; }
	public ITypeSymbol SecondParameterType { get; set; }
	public String FirstParameter { get; set; }
	public String SecondParameter { get; set; }

	public String TrackingStruct => $"StarnightCachingTrackingType_{this.ReturnType.Name}";
}

namespace Starnight.SourceGenerators.GatewayEvents;

using System;

using Microsoft.CodeAnalysis;

internal struct GatewayEventMetadata
{
	public String EventName { get; set; }

	public String EventType { get; set; }

	public String PayloadType { get; set; }

	public INamedTypeSymbol DeclaringClass { get; set; }
}

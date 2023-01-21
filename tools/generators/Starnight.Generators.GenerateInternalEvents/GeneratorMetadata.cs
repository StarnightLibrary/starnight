namespace Starnight.Generators.GenerateInternalEvents;

using System;
using System.Collections.Generic;

internal sealed record GeneratorMetadata
{
	public required List<GeneratorMetadataEntry> Metadata { get; init; }

	public required List<String> Namespaces { get; init; }
}

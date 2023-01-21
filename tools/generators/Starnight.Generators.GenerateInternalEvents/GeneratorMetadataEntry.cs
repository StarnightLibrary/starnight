namespace Starnight.Generators.GenerateInternalEvents;

using System;

internal readonly record struct GeneratorMetadataEntry
{
	public readonly String Event { get; init; }

	public readonly String EventName { get; init; }

	// payload, without namespace name
	public readonly String Payload { get; init; }
}

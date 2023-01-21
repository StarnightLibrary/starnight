namespace Starnight.Generators.GenerateInternalEvents;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

using Spectre.Console;

internal static class MetadataExtractor
{
	public static GeneratorMetadata ExtractMetadata
	(
		JsonElement definitions
	)
	{
		Dictionary<String, (String, String)> events = new();

		foreach(JsonProperty v in definitions.EnumerateObject())
		{
			String @event = v.Name;

#pragma warning disable IL2026
#pragma warning disable IL3050
			String[] types = JsonSerializer.Deserialize<String[]>
			(
				v.Value
			)!;
#pragma warning restore IL2026
#pragma warning restore IL3050

			events.Add
			(
				@event,
				(
					types[0],
					types[1]
				)
			);
		}

		AnsiConsole.MarkupLine
		(
			"""
			  Deserialized event definitions.
			  Processing event definitions...
			"""
		);

		List<GeneratorMetadataEntry> metadataEntries = new();
		List<String> namespaces = new();

		foreach(KeyValuePair<String, (String, String)> @event in events)
		{
			ReadOnlySpan<Char> fullPayload = @event.Value.Item2.AsSpan();

			Int32 index = fullPayload.LastIndexOf('.');

			namespaces.Add
			(
				new(fullPayload[..index])
			);

			GeneratorMetadataEntry entry = new()
			{
				Event = @event.Key,
				EventName = @event.Value.Item1,
				Payload = new(fullPayload[index..].Trim('.'))
			};

			metadataEntries.Add(entry);
		}

		AnsiConsole.MarkupLine
		(
			"""
			  Processed event definitions.
			  Post-processing namespaces...
			"""
		);

		namespaces = namespaces.Distinct()
			.ToList();

		namespaces.Sort();

		AnsiConsole.MarkupLine
		(
			"  Post-processed namespaces."
		);

		return new()
		{
			Metadata = metadataEntries,
			Namespaces = namespaces
		};
	}
}

namespace Starnight.Generators.GenerateInternalEvents;

using System;
using System.IO;
using System.Text.Json;

using Spectre.Console;

public static class Program
{
	public static Int32 Main(String[] args)
	{
		if
		(
			args is { Length: 0 }
				or ["-h" or "--help"]
				or ["-?"]
		)
		{
			AnsiConsole.MarkupLine
			(
				"""
				Starnight.Internal Events generator v0.3.0

				  [plum1]Usage: generate-internal-events path/to/events-internal.json output/path[/]
				"""
			);
			
			return 0;
		}

		// doesn't supply a json file
		if(!args[0].EndsWith(".json"))
		{
			AnsiConsole.MarkupLine
			(
				$$"""
				[red]Expected a json file with event definitions as first parameter, received "{{args[0]}}"[/]
				"""
			);

			return 1;
		}

		if(!File.Exists(args[0]))
		{
			AnsiConsole.MarkupLine
			(
				$$"""
				[red]The event definition file "{{args[0]}}" could not be found.[/]
				"""
			); 
		}

		if(!Directory.Exists(args[1]))
		{
			AnsiConsole.MarkupLine
			(
				$$"""
				[red]The output directory "{{args[1]}}" could not be found.[/]
				"""
			);

			return 1;
		}

		AnsiConsole.MarkupLine
		(
			"""
			[bold]Starnight Internal Events generator, version 0.3.0[/]

			  Loading event definitions file...
			"""
		);

		using StreamReader reader = new(args[0]);

		JsonDocument document = JsonDocument.Parse
		(
			reader.ReadToEnd(),
			options: new()
			{
				CommentHandling = JsonCommentHandling.Skip,
				AllowTrailingCommas = true
			}
		);

		GeneratorMetadata metadata = MetadataExtractor.ExtractMetadata
		(
			document.RootElement
		);

		EventEmitter.Emit
		(
			metadata,
			args[1]
		);

		return 0;
	}
}

namespace Starnight.Generators.GenerateInternalEvents;

using System;
using System.IO;

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
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine
			(
				"Starnight.Internal Events generator v0.3.0\n"
			);

			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine
			(
				"    Usage: generate-internal-events path/to/events-internal.json output/path"
			);
			
			return 0;
		}

		// doesn't supply a json file
		if(!args[0].EndsWith(".json"))
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine
			(
				$"Expected a json file with event definitions as first parameter, " +
				$"received \"{args[0]}\""
			);

			return 1;
		}

		if(!File.Exists(args[0]))
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine
			(
				$"The event definition file \"{args[0]}\" could not be found."
			);

			return 1;
		}

		if(!Directory.Exists(args[1]))
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine
			(
				$"The output directory \"{args[1]}\" could not be found."
			);

			return 1;
		}

		return 0;
	}
}

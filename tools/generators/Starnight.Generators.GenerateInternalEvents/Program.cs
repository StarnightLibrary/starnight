namespace Starnight.Generators.GenerateInternalEvents;

using System;

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
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.WriteLine
			(
				"Starnight.Internal Events generator v0.3.0\n"
			);

			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine
			(
				"\tUsage: generate-internal-events path/to/events-internal.json output/path"
			);

			return 0;
		}

		return 0;
	}
}

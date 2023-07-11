namespace Starnight.Benchmarks.RegressionTests;

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text.Json;

using Spectre.Console;
using Spectre.Console.Cli;

public sealed class TrackCommand : Command<TrackCommand.Settings>
{
	public sealed class Settings : CommandSettings
	{
		[Description("Specifies whether the full diffs should be listed, as opposed to just the overview.")]
		[CommandOption("-d|--diffs")]
		public Boolean ListFullDiffs { get; init; }
	}

	public override Int32 Execute
	(
		[NotNull]
		CommandContext context,

		[NotNull]
		Settings settings
	)
	{
		// if the tree is clean, we need different logic
		ProcessStartInfo startInfo = new()
		{
			FileName = "git",
			Arguments = "describe --dirty --always",
			RedirectStandardOutput = true
		};

		Process git = Process.Start(startInfo)!;

		git.WaitForExit();

		if(git.ExitCode != 0)
		{
			AnsiConsole.MarkupLine
			(
				"[indianred1_1]--------------- an error occurred while fetching git info ---------------[/]\n" +
				"[indianred1_1]  printing output from git describe below:[/]\n\n"
			);

			Console.WriteLine(git.StandardOutput.ReadToEnd());
			return 1;
		}

		Boolean clean = !git.StandardOutput.ReadToEnd().Contains("dirty");

		if(clean)
		{
			Console.WriteLine
			(
				"Working tree is clean, running baseline benchmarks...\n"
			);

			runAndStoreBaseline();
		}
		else
		{
			runAndCompareToBaseline(settings);
		}

		return 0;
	}

	private static void runAndStoreBaseline()
	{
		if(Directory.Exists("./.cache/benchmarks"))
		{
			Directory.Delete("./.cache/benchmarks", true);
		};

#pragma warning disable IDE0002
		Starnight.Benchmarks.Program.Main
		(
			new String[]
			{
				"-j",
				"Long"
			}
		);
#pragma warning restore IDE0002

		String[] subdirectories = Directory.GetDirectories("./.cache/benchmarks");

		String[] files = subdirectories
		.AsParallel()
		.Select
		(
			directory =>
			{
				String fullPath = Directory.GetFiles(directory).First();
				String[] segments = fullPath.Split('/', '\\');
				return String.Join('/', segments[^2], segments[^1]);
			}
		)
		.ToArray();

		using StreamWriter writer = new(File.Create("./.cache/benchmarks/baseline.json"));

		writer.Write
		(
			JsonSerializer.Serialize(files)
		);

		Console.WriteLine("\nWrote baseline definition file.");
	}

	private static void runAndCompareToBaseline(Settings settings)
	{
#pragma warning disable IDE0002
		Starnight.Benchmarks.Program.Main
		(
			new String[]
			{
				"-j",
				"Medium"
			}
		);
#pragma warning restore IDE0002

		using StreamReader reader = new("./.cache/benchmarks/baseline.json");

		String[] baseline = JsonSerializer.Deserialize<String[]>(reader.ReadToEnd())!;

		BenchmarkComparer.CompareAndPrintResults(baseline, settings.ListFullDiffs);
	}
}

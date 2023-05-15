namespace Starnight.Benchmarks.RegressionTests;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

using Spectre.Console;

using Starnight.Benchmarks.ResultTransformation.Representation;

internal static class BenchmarkComparer
{
	public static void CompareAndPrintResults
	(
		String[] baseline,
		Boolean printFullResults
	)
	{
		Dictionary<String, Boolean> completed = new();
		List<(String name, Decimal timeRatio, Decimal memoryRatio)> diffs = new();

		Int32 addedContexts = 0, removedContexts = 0, preservedContexts = 0;
		Boolean emitWarning = false;

		foreach(String key in baseline)
		{
			completed.Add(key, false);
		}

		String[] directories = Directory.GetDirectories("./.cache/benchmarks");

		foreach(String directory in directories)
		{
			DirectoryInfo info = new(directory);

			FileInfo file = info
			.GetFiles()
			.OrderByDescending
			(
				xm => xm.CreationTime
			)
			.First();

			String? baselineComparison = baseline
			.Where
			(
				xm => xm.StartsWith(directory.Split('/', '\\')[^1])
			)
			.FirstOrDefault();

			if(baselineComparison is null)
			{
				StoredBenchmarkModel benchmark = JsonSerializer.Deserialize<StoredBenchmarkModel>
				(
					File.ReadAllText(file.FullName)
				)!;

				addedContexts += benchmark.Results.Count();

				continue;
			}

			// load benchmarks to compare

			completed[baselineComparison] = true;

			StoredBenchmarkModel current = JsonSerializer.Deserialize<StoredBenchmarkModel>
			(
				File.ReadAllText(file.FullName)
			)!;

			StoredBenchmarkModel baselineBenchmark = JsonSerializer.Deserialize<StoredBenchmarkModel>
			(
				File.ReadAllText($"./.cache/benchmarks/{baselineComparison}")
			)!;

			if(current.Environment != baselineBenchmark.Environment)
			{
				emitWarning = true;
			}

			// track added/removed contexts and add ratios for comparable contexts

			Int32 preserved = 0;

			foreach(BenchmarkResultModel result in current.Results)
			{
				BenchmarkResultModel? baselineResult = baselineBenchmark.Results
				.Where
				(
					xm => xm.Method == result.Method
				)
				.FirstOrDefault();

				if(baselineResult is null)
				{
					addedContexts++;
					continue;
				}

				Decimal timeRatio = (result.Mean / baselineResult.Mean * 100) - 100;
				Decimal memoryRatio = (result.AllocatedBytes / baselineResult.AllocatedBytes * 100) - 100;

				diffs.Add
				(
					(
						$"{baselineComparison.Split('/')[0]}#{result.Method}",
						timeRatio,
						memoryRatio
					)
				);

				preserved++;
			}

			preservedContexts += preserved;
			removedContexts += baselineBenchmark.Results.Count() - preserved;
		}

		// accumulate unhandled removed contexts

		foreach(KeyValuePair<String, Boolean> kv in completed)
		{
			if(kv.Value)
			{
				continue;
			}

			StoredBenchmarkModel unhandled = JsonSerializer.Deserialize<StoredBenchmarkModel>
			(
				File.ReadAllText($"./.cache/benchmarks/{kv.Key}")
			)!;

			removedContexts += unhandled.Results.Count();
		}

		// print results

		if(emitWarning)
		{
			AnsiConsole.MarkupLine
			(
				"[lightgoldenrod2_2]WARNING: Not all benchmark environments were matching. The benchmark " +
				"may be inaccurate. Use the results with caution.\n"
			);
		}

		AnsiConsole.MarkupLine
		(
			$"{addedContexts} contexts added, {removedContexts} contexts removed, " +
			$"{preservedContexts} contexts preserved.\n"
		);

		if(preservedContexts == 0)
		{
			return;
		}

		if(printFullResults)
		{
			AnsiConsole.MarkupLine("---- PERFORMANCE CHANGES ----\n");

			foreach
			(
				(String name, Decimal timeRatio, Decimal memoryRatio) element in diffs.OrderByDescending
				(
					xm => xm.timeRatio
				)
			)
			{
				if(element.timeRatio > 0.0m)
				{
					AnsiConsole.MarkupLine
					(
						$"{element.name}: [indianred1_1]+{Decimal.Abs(element.timeRatio):f2}%[/]"
					);
				}
				else
				{
					AnsiConsole.MarkupLine
					(
						$"{element.name}: [darkseagreen1_1]-{Decimal.Abs(element.timeRatio):f2}%[/]"
					);
				}
			}

			AnsiConsole.MarkupLine("\n---- MEMORY CHANGES ----\n");

			foreach
			(
				(String name, Decimal timeRatio, Decimal memoryRatio) element in diffs.OrderByDescending
				(
					xm => xm.memoryRatio
				)
			)
			{
				if(element.timeRatio > 0.0m)
				{
					AnsiConsole.MarkupLine
					(
						$"{element.name}: [indianred1_1]+{Decimal.Abs(element.memoryRatio):f2}%[/]"
					);
				}
				else
				{
					AnsiConsole.MarkupLine
					(
						$"{element.name}: [darkseagreen1_1]-{Decimal.Abs(element.memoryRatio):f2}%[/]"
					);
				}
			}
		}
		else
		{
			AnsiConsole.MarkupLine("---- WORST PERFORMANCE REGRESSIONS ----\n");

			foreach
			(
				(String name, Decimal timeRatio, Decimal memoryRatio) element in diffs.OrderByDescending
				(
					xm => xm.timeRatio
				)
				.Take(3)
			)
			{
				if(element.timeRatio < 0)
				{
					continue;
				}

				AnsiConsole.MarkupLine
				(
					$"{element.name}: [indianred1_1]+{Decimal.Abs(element.timeRatio):f2}%[/]"
				);
			}

			AnsiConsole.MarkupLine("\n---- BEST PERFORMANCE IMPROVEMENTS ----\n");

			foreach
			(
				(String name, Decimal timeRatio, Decimal memoryRatio) element in diffs.OrderByDescending
				(
					xm => xm.timeRatio
				)
				.TakeLast(3)
				.Reverse()
			)
			{
				if(element.timeRatio > 0)
				{
					continue;
				}

				AnsiConsole.MarkupLine
				(
					$"{element.name}: [darkseagreen1_1]-{Decimal.Abs(element.timeRatio):f2}%[/]"
				);
			}

			AnsiConsole.MarkupLine("\n---- WORST MEMORY REGRESSIONS ----\n");

			foreach
			(
				(String name, Decimal timeRatio, Decimal memoryRatio) element in diffs.OrderByDescending
				(
					xm => xm.memoryRatio
				)
				.Take(3)
			)
			{
				if(element.memoryRatio < 0)
				{
					continue;
				}

				AnsiConsole.MarkupLine
				(
					$"{element.name}: [indianred1_1]+{Decimal.Abs(element.memoryRatio):f2}%[/]"
				);
			}

			AnsiConsole.MarkupLine("\n---- BEST MEMORY IMPROVEMENTS ----\n");

			foreach
			(
				(String name, Decimal timeRatio, Decimal memoryRatio) element in diffs.OrderByDescending
				(
					xm => xm.memoryRatio
				)
				.TakeLast(3)
				.Reverse()
			)
			{
				if(element.memoryRatio > 0)
				{
					continue;
				}

				AnsiConsole.MarkupLine
				(
					$"{element.name}: [darkseagreen1_1]-{Decimal.Abs(element.memoryRatio):f2}%[/]"
				);
			}
		}
	}
}

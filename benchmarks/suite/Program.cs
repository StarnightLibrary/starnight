namespace Starnight.Benchmarks;

using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

using BenchmarkDotNet.Running;

using Starnight.Benchmarks.ResultTransformation;
using Starnight.Benchmarks.ResultTransformation.Models;
using Starnight.Benchmarks.ResultTransformation.Representation;

public class Program
{
	public static void Main(String[] args)
	{
		Console.WriteLine("Silently running Starnight benchmarks...");

		_ = BenchmarkRunner.Run
		(
			typeof(Program).Assembly,
			new SuiteConfiguration(),
			args
		);

		Console.WriteLine("Starnight benchmarks have ran, converting result data...");

		String[] files = Directory.GetFiles("./BenchmarkDotNet.Artifacts/results", "*.json");

		Console.WriteLine($"  Found {files.Length} result files...");

		JsonSerializerOptions makeItReadable = new()
		{
			WriteIndented = true
		};

		_ = Parallel.ForEach
		(
			files,
			filename =>
			{
				try
				{
					ReportModel report = JsonSerializer.Deserialize<ReportModel>
					(
						File.ReadAllText(filename)
					)!;

					StoredBenchmarkModel storedBenchmark = BenchmarkTransformer.Transform
					(
						report
					);

					String name = report.Title.Split('-')[0];

					_ = Directory.CreateDirectory($"./.cache/benchmarks/{name}");

					using StreamWriter writer = new
					(
						File.Create($"./.cache/benchmarks/{name}/{report.Title}.json")
					);

					writer.WriteLine
					(
						JsonSerializer.Serialize
						(
							storedBenchmark,
							makeItReadable
						)
					);
				}
				catch
				{
					Console.ForegroundColor = ConsoleColor.Red;

					Console.WriteLine($"  Failed to write result for {filename}, aborting...");

					Console.ResetColor();

					throw;
				}
			}
		);

		Console.WriteLine("Transformed and stored results.");
	}
}

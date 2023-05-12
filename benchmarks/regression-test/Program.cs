namespace Starnight.Benchmarks.RegressionTests;

using System;

using Spectre.Console.Cli;

internal class Program
{
	private static void Main(String[] args)
	{
		CommandApp app = new();

		app.Configure
		(
			config =>
			{
				config.AddCommand<CICommand>("ci")
				.IsHidden()
				.WithDescription
				(
					"Run in CI environments only. Tests the passed benchmarks against the benchmarks " +
					"from the latest commit to the repository."
				);

#if LOCAL_BUILD
				config.AddCommand<TrackCommand>("track")
				.WithDescription
				(
					"If the working tree is clean, runs the benchmark suite and saves it as the baseline " +
					"for future efforts. If the working tree is not clean, runs the benchmarks and compares " +
					"them against both the current baseline and the latest in-development benchmark run."
				)
				.WithExample
				(
					new[]
					{
						"track",
						"[-d|--diffs]"
					}
				);
#endif
			}
		);

		app.Run(args);
	}
}

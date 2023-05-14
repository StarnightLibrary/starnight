namespace Starnight.Benchmarks.ResultTransformation.Models;

using System;

/// <summary>
/// Represents the relevant-to-us statistics of a given benchmark run.
/// </summary>
public sealed record StatisticsModel
{
	/// <summary>
	/// Gets the mean execution time of the benchmark.
	/// </summary>
	public required Decimal Mean { get; init; }

	/// <summary>
	/// Gets the standard error of the mean.
	/// </summary>
	public required Decimal StandardError { get; init; }
}

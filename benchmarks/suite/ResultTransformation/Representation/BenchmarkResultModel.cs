namespace Starnight.Benchmarks.ResultTransformation.Representation;

using System;

/// <summary>
/// Represents the result of a single benchmark run.
/// </summary>
public sealed record BenchmarkResultModel
{
	/// <summary>
	/// The name of the given benchmark method, used as a display name.
	/// </summary>
	public required String Method { get; init; }

	/// <summary>
	/// Gets the mean execution time of the benchmark.
	/// </summary>
	public required Decimal Mean { get; init; }

	/// <summary>
	/// Gets the standard error of the mean.
	/// </summary>
	public required Decimal StandardError { get; init; }

	/// <summary>
	/// The amount of data allocated on the managed heap during a single run.
	/// </summary>
	public required Int32 AllocatedBytes { get; init; }
}

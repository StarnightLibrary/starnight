namespace Starnight.Benchmarks.ResultTransformation.Models;

using System;

/// <summary>
/// Represents a singular benchmark.
/// </summary>
public sealed record BenchmarkModel
{
	/// <summary>
	/// The name of the benchmark method, used as a display name.
	/// </summary>
	public required String Method { get; init; }

	/// <summary>
	/// The statistics for this benchmark run.
	/// </summary>
	public required StatisticsModel Statistics { get; init; }

	/// <summary>
	/// The memory usage statistics for this benchmark run.
	/// </summary>
	public required MemoryModel Memory { get; init; }
}

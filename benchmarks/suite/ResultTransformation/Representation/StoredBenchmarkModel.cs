namespace Starnight.Benchmarks.ResultTransformation.Representation;

using System.Collections.Generic;

using Starnight.Benchmarks.ResultTransformation.Models;

/// <summary>
/// Represents a transformed and stored benchmark.
/// </summary>
public sealed record StoredBenchmarkModel
{
	/// <summary>
	/// The environment this benchmark was ran in.
	/// </summary>
	public required HostEnvironmentModel Environment { get; init; }

	/// <summary>
	/// The results of the benchmark runs.
	/// </summary>
	public required IEnumerable<BenchmarkResultModel> Results { get; init; }
}

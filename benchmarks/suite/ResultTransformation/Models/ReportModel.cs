namespace Starnight.Benchmarks.ResultTransformation.Models;

using System;
using System.Collections.Generic;

/// <summary>
/// Represents the base report object.
/// </summary>
public sealed record ReportModel
{
	/// <summary>
	/// The title of the benchmark. This will later be transformed to the filename.
	/// </summary>
	public required String Title { get; init; }

	/// <summary>
	/// The host environment.
	/// </summary>
	public required HostEnvironmentModel HostEnvironmentInfo { get; init; }

	/// <summary>
	/// All benchmarks that were part of this run.
	/// </summary>
	public required IEnumerable<BenchmarkModel> Benchmarks { get; init; }
}

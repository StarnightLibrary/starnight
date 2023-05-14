namespace Starnight.Benchmarks.ResultTransformation.Models;

using System;

/// <summary>
/// Represents the host environment in which the benchmark is running. If two host environments are
/// different, the differences are considered non-representative.
/// </summary>
public sealed record HostEnvironmentModel
{
	/// <summary>
	/// The currently running version of BDN. Changes in version might imply behavioural differences.
	/// </summary>
	public required String BenchmarkDotNetVersion { get; init; }

	/// <summary>
	/// The reported name of the CPU.
	/// </summary>
	public required String ProcessorName { get; init; }

	/// <summary>
	/// The reported .NET Runtime version.
	/// </summary>
	public required String RuntimeVersion { get; init; }

	/// <summary>
	/// The reported CPU architecture.
	/// </summary>
	public required String Architecture { get; init; }

	/// <summary>
	/// An attached debugger might imply various reasons to discard a benchmark.
	/// </summary>
	public required Boolean HasAttachedDebugger { get; init; }

	/// <summary>
	/// Different JIT compilers are incomparable, obviously.
	/// </summary>
	public required Boolean HasRyuJit { get; init; }

	/// <summary>
	/// DEBUG or RELEASE. Incomparable, obviously.
	/// </summary>
	public required String Configuration { get; init; }
}

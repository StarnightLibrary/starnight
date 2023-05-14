namespace Starnight.Benchmarks.ResultTransformation.Models;

using System;

/// <summary>
/// Represents the to-us relevant memory data.
/// </summary>
public sealed record MemoryModel
{
	/// <summary>
	/// The amount of data allocated on the managed heap during a single run.
	/// </summary>
	public required Int32 BytesAllocatedPerOperation { get; init; }
}

namespace Starnight.Benchmarks.ResultTransformation;

using System.Collections.Generic;

using Starnight.Benchmarks.ResultTransformation.Models;
using Starnight.Benchmarks.ResultTransformation.Representation;

public static class BenchmarkTransformer
{
	public static StoredBenchmarkModel Transform
	(
		ReportModel report
	)
	{
		List<BenchmarkResultModel> runBenchmarks = new();

		foreach(BenchmarkModel model in report.Benchmarks)
		{
			runBenchmarks.Add
			(
				new BenchmarkResultModel
				{
					Method = model.Method,
					Mean = model.Statistics.Mean,
					StandardError = model.Statistics.StandardError,
					AllocatedBytes = model.Memory.BytesAllocatedPerOperation
				}
			);
		}

		return new()
		{
			Environment = report.HostEnvironmentInfo,
			Results = runBenchmarks
		};
	}
}

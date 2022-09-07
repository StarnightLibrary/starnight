namespace Starnight.Benchmarks;

using BenchmarkDotNet.Running;

using Starnight.Benchmarks.Transient;

internal class Program
{
	public static void Main() => BenchmarkRunner.Run<MethodInfoReflectionBenchmark>();
}

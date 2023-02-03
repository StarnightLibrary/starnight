namespace Starnight.Benchmarks.Transient;

using System.Reflection;
using System.Text.Json;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

[SimpleJob(RunStrategy.Throughput), MemoryDiagnoser]
public class MethodInfoReflectionBenchmark
{
	[Benchmark]
	public MethodInfo? GetDeserializerMethodMultiWhere()
	{
		return typeof(JsonSerializer)
			.GetMethods(BindingFlags.Static | BindingFlags.Public)
			.Where(xm => xm.Name == nameof(JsonSerializer.Deserialize))
			.Select(xm => new
			{
				Method = xm,
				Parameters = xm.GetParameters(),
				GenericArguments = xm.GetGenericArguments()
			})
			.Where(xm => xm.GenericArguments.Length == 1)
			.Where(xm => xm.Parameters.Length == 2)
			.Where(xm => xm.Parameters.First().ParameterType == typeof(JsonElement))
			.Where(xm => xm.Parameters.Last().ParameterType == typeof(JsonSerializerOptions))
			.Select(xm => xm.Method)
			.FirstOrDefault();
	}

	[Benchmark(Baseline = true)]
	public MethodInfo? GetDeserializerMethodSingleWhere()
	{
		return typeof(JsonSerializer)
			.GetMethods(BindingFlags.Static | BindingFlags.Public)
			.Where(xm => xm.Name == nameof(JsonSerializer.Deserialize))
			.Select(xm => new
			{
				Method = xm,
				Parameters = xm.GetParameters(),
				GenericArguments = xm.GetGenericArguments()
			})
			.Where(xm => xm.GenericArguments.Length == 1 &&
						 xm.Parameters.Length == 2 &&
						 xm.Parameters.First().ParameterType == typeof(JsonElement) &&
						 xm.Parameters.Last().ParameterType == typeof(JsonSerializerOptions))
			.Select(xm => xm.Method)
			.FirstOrDefault();
	}
}

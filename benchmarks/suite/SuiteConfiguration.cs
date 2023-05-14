namespace Starnight.Benchmarks;

using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters.Json;
using BenchmarkDotNet.Loggers;

internal class SuiteConfiguration : ManualConfig
{
	public SuiteConfiguration()
	{
		_ = this.AddLogger(NullLogger.Instance);
		_ = this.AddExporter(JsonExporter.BriefCompressed);
		_ = this.AddDiagnoser(MemoryDiagnoser.Default);
		_ = this.KeepBenchmarkFiles();
		this.UnionRule = ConfigUnionRule.AlwaysUseGlobal;
	}
}

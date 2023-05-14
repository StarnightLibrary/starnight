namespace Starnight.Benchmarks;

using System;

using BenchmarkDotNet.Running;

internal class Program
{
	private static void Main(String[] args)
	{
		Console.WriteLine("Silently running Starnight benchmarks...");

		_ = BenchmarkRunner.Run
		(
			typeof(Program).Assembly,
			new SuiteConfiguration(),
			args
		);
	}
}

namespace Starnight.Benchmarks;

using System.Runtime.CompilerServices;

using BenchmarkDotNet.Attributes;

[SimpleJob]
[MemoryDiagnoser]
public class UnsafeVsCastclass
{
	public readonly static Object test = new B()
	{
		OneThing = "a",
		TwoThing = "b",
		ThreeThing = "ab"
	};

	[Benchmark(Baseline = true)]
	public void Castclass()
	{
		A a = (A)test;
		a.OneThing = "a";
	}

	[Benchmark]
	public void UnsafeTest()
	{
		A a = Unsafe.As<A>(test);
		a.OneThing = "a";
	}
}

public class A
{
	public String OneThing { get; set; }

	public String TwoThing { get; set; }
}

public class B : A
{
	public String ThreeThing { get; set; }
}

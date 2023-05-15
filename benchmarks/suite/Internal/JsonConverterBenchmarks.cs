namespace Starnight.Benchmarks.Internal;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using BenchmarkDotNet.Attributes;

using Starnight.Internal;
using Starnight.Internal.Entities.Messages.Embeds;

public class JsonConverterBenchmarks
{
	private readonly JsonSerializerOptions options = StarnightInternalConstants.DefaultSerializerOptions;

	private const String optionalTestStringAllPresent = """
	{
		"opt_int": 7,
		"opt_null_int": null,
		"opt_complex": {
			"name": "ara",
			"value": "small",
			"inline": true
		}
	}
	""";

	private const String optionalTestStringSomePresent = """
	{
		"opt_null_int": 7,
		"opt_complex": {
			"name": "väinämöinen",
			"value": "ilmarinen"
		}
	}
	""";

	private readonly OptionalConverterTestObject optionalTestObjectAllPresent = new()
	{
		OptionalInteger = 7,
		OptionalNullableInteger = null,
		SlightlyMoreComplexObject = new DiscordEmbedField
		{
			Name = "this",
			Value = "that",
			Inline = true
		}
	};

	private readonly OptionalConverterTestObject optionalTestObjectSomePresent = new()
	{
		OptionalNullableInteger = 7,
		SlightlyMoreComplexObject = new DiscordEmbedField
		{
			Name = "that",
			Value = "this"
		}
	};

	[Benchmark]
	public void DeserializeOptionalAllPresent()
	{
		OptionalConverterTestObject test = JsonSerializer.Deserialize<OptionalConverterTestObject>
		(
			optionalTestStringAllPresent,
			this.options
		)!;

		// make sure the JIT doesn't remove the object
		GC.KeepAlive(test);
	}

	[Benchmark]
	public void DeserializeOptionalSomePresent()
	{
		OptionalConverterTestObject test = JsonSerializer.Deserialize<OptionalConverterTestObject>
		(
			optionalTestStringSomePresent,
			this.options
		)!;

		GC.KeepAlive(test);
	}

	[Benchmark]
	public void SerializeOptionalAllPresent()
	{
		String result = JsonSerializer.Serialize
		(
			this.optionalTestObjectAllPresent,
			this.options
		);

		GC.KeepAlive(result);
	}

	[Benchmark]
	public void SerializeOptionalSomePresent()
	{
		String result = JsonSerializer.Serialize
		(
			this.optionalTestObjectSomePresent,
			this.options
		);

		GC.KeepAlive(result);
	}
}

internal class OptionalConverterTestObject
{
	[JsonPropertyName("opt_int")]
	public Optional<Int32> OptionalInteger { get; init; }

	[JsonPropertyName("opt_null_int")]
	public Optional<Int32?> OptionalNullableInteger { get; init; }

	[JsonPropertyName("opt_complex")]
	public Optional<DiscordEmbedField> SlightlyMoreComplexObject { get; init; }
}

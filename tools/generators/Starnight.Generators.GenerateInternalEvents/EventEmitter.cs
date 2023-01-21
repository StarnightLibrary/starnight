namespace Starnight.Generators.GenerateInternalEvents;

using System;
using System.IO;
using System.Linq;
using System.Text;

using Spectre.Console;

internal static class EventEmitter
{
	public static void Emit
	(
		GeneratorMetadata metadata,
		String path
	)
	{
		StringBuilder codeBuilder = new
		(
$$"""
// auto-generated code
namespace {{path.Replace('/', '.').Trim('.')}};

using System;
using System.CodeDom.Compiler;
using System.Text.Json;

using Starnight.Internal;

"""
		);

		_ = codeBuilder.Append
		(
			String.Join
			(
				'\n',
				metadata.Namespaces.Select
				(
					xm => $"using {xm};"
				)
			)
		);

		_ = codeBuilder.Append
		(
"""


[GeneratedCode("starnight-internal-events-generator", "v0.3.0")]
internal static class EventDeserializer
{
	// methods here are separated to reduce IL size in the main method

"""
		);

		foreach(GeneratorMetadataEntry entry in metadata.Metadata)
		{
			_ = codeBuilder.Append
			(
$$"""
	private static IDiscordGatewayEvent deserialize{{entry.EventName}}
	(
		JsonElement element,
		String name
	)
	{
		return new {{entry.EventName}}
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<{{entry.Payload}}>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}


"""
			);
		}

		_ = codeBuilder.Append
		(
"""
	public static IDiscordGatewayEvent DeserializeEvent
	(
		JsonElement element,
		String name
	)
	{
		switch(name)
		{
"""
		);

		foreach(GeneratorMetadataEntry entry in metadata.Metadata)
		{
			_ = codeBuilder.Append
			(
$$"""
			case "{{entry.Event}}":
				return deserialize{{entry.EventName}}
				(
					element,
					name
				);


"""
			);
		}

		_ = codeBuilder.Append
		(
"""
			default:
				return new DiscordUnknownDispatchEvent
				{
					EventName = name,
					Sequence = element.GetProperty("s").GetInt32(),
					Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
					Data = element.GetProperty("d")
				};
		}
	}
}
"""
		);

		AnsiConsole.MarkupLine
		(
			"""
			  Generated code, writing to file...
			"""
		);

		if(!File.Exists($"{path}/EventDeserializer.cs"))
		{
			File.Create($"{path}/EventDeserializer.cs").Close();
		}

		using StreamWriter writer = new($"{path}/EventDeserializer.cs");

		writer.Write(codeBuilder.ToString());

		AnsiConsole.MarkupLine
		(
			"""
			  [darkseagreen1_1]Code generation successful![/]
			"""
		);
	}
}

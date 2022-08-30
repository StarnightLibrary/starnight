namespace Starnight.Internal.Converters;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

internal class OptionalParameterJsonConverterFactory : JsonConverterFactory
{
	public override Boolean CanConvert(Type typeToConvert)
		=> typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(Optional<>);

	public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
	{
		Type wrappedType = typeToConvert.GetGenericArguments()[0];

		return (JsonConverter)Activator.CreateInstance(typeof(OptionalParameterJsonConverter<>).MakeGenericType(wrappedType))!;
	}
}

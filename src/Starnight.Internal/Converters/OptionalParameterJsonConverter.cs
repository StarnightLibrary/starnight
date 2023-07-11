namespace Starnight.Internal.Converters;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

internal class OptionalParameterJsonConverter<TParamType> : JsonConverter<Optional<TParamType>>
{
	public override Optional<TParamType> Read
	(
		ref Utf8JsonReader reader,
		Type typeToConvert,
		JsonSerializerOptions options
	)
	{
		TParamType value = JsonSerializer.Deserialize<TParamType>(ref reader, options)!;
		return new Optional<TParamType>(value);
	}


	public override void Write
	(
		Utf8JsonWriter writer,
		Optional<TParamType> value,
		JsonSerializerOptions options
	)
	{
		if(value.HasValue)
		{
			JsonSerializer.Serialize<TParamType>(writer, value.Value, options);
		}
	}
}

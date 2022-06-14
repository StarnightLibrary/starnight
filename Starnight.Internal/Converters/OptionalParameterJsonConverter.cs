namespace Starnight.Internal.Converters;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Starnight.Internal;

internal class OptionalParameterJsonConverter<TParamType> : JsonConverter<OptionalParameter<TParamType>?>
{
	public override OptionalParameter<TParamType>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		=> throw new NotImplementedException();


	public override void Write(Utf8JsonWriter writer, OptionalParameter<TParamType>? value, JsonSerializerOptions options)
	{
		if(value is null)
			JsonSerializer.Serialize(writer, (Int32?)null, options);
		else
			JsonSerializer.Serialize(writer, value.Value, options);
	}
}

namespace Starnight.Internal.Converters;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// Provides serializations for discord's optional null booleans, see
/// <seealso href="https://discord.com/developers/docs/topics/permissions#role-object-role-tags-structure"/>.
/// </summary>
/// <remarks>
/// This needs to be applied to every null boolean property individually.
/// </remarks>
public class NullBooleanJsonConverter : JsonConverter<Boolean>
{
	// if the token type is False or True we have an actual boolean on our hands and should read it
	// appropriately. if not, we judge by the existence of the token (which is what discord sends).
	public override Boolean Read
	(
		ref Utf8JsonReader reader,
		Type typeToConvert,
		JsonSerializerOptions options
	)
	{
		Boolean? value = JsonSerializer.Deserialize<Boolean?>
		(
			ref reader,
			options
		);

		return value != false;
	}

	// slightly cursed, but since we can't ever actually send this to discord we don't need to deal
	// with any of this. we'll serialize it as a correct boolean so it can be read correctly if the
	// end user uses our models for serialization.
	public override void Write
	(
		Utf8JsonWriter writer,
		Boolean value,
		JsonSerializerOptions options
	)
		=> writer.WriteBooleanValue(value);
}

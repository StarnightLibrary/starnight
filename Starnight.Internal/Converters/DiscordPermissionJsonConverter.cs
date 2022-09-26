namespace Starnight.Internal.Converters;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities;

public class DiscordPermissionJsonConverter : JsonConverter<DiscordPermissions>
{
	public override DiscordPermissions Read
	(
		ref Utf8JsonReader reader,
		Type typeToConvert,
		JsonSerializerOptions options
	)
	{
		String? permissions = reader.GetString();

		if(permissions is not null && Int64.TryParse(permissions, out Int64 parsed))
		{
			return (DiscordPermissions)parsed;
		}
		else if(permissions is null)
		{
			Int64? integerPermissions = reader.GetInt64();

			if(integerPermissions is not null)
			{
				return (DiscordPermissions)integerPermissions;
			}
		}

		throw new FormatException("Received permissions from Discord in an invalid format.");
	}

	public override void Write
	(
		Utf8JsonWriter writer,
		DiscordPermissions value,
		JsonSerializerOptions options
	)
		=> writer.WriteStringValue(((Int64)value).ToString());
}

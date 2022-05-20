namespace Starnight.Internal.Rest.Payloads.Emojis;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a payload to PATCH /guilds/:guild_id/emojis/:emoji_id
/// </summary>
public record ModifyGuildEmojiRequestPayload
{
	/// <summary>
	/// New name of this emoji.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("name")]
	public String? Name { get; init; }

	/// <summary>
	/// New array of snowflakes allowed to use this emoji.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("roles")]
	public IEnumerable<Int64>? Roles { get; init; }
}

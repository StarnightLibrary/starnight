namespace Starnight.Internal.Rest.Payloads.Emojis;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a payload to POST /guilds/:guild_id/emojis.
/// </summary>
public sealed record CreateGuildEmojiRequestPayload
{
	/// <summary>
	/// Name of this emoji.
	/// </summary>
	[JsonPropertyName("name")]
	public String Name { get; init; } = null!;

	/// <summary>
	/// Image data for this emoji.
	/// </summary>
	[JsonPropertyName("image")]
	public String ImageData { get; init; } = null!;

	/// <summary>
	/// Array of snowflakes allowed to use this emoji.
	/// </summary>
	[JsonPropertyName("roles")]
	public Optional<IEnumerable<Int64>> Roles { get; init; }
}

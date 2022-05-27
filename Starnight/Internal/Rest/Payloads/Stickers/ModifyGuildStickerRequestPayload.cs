namespace Starnight.Internal.Rest.Payloads.Stickers;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a payload to PATCH /guilds/:guild_id/stickers/:sticker_id.
/// </summary>
public record ModifyGuildStickerRequestPayload
{
	/// <summary>
	/// Name of the sticker.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("name")]
	public String? Name { get; init; }

	/// <summary>
	/// Description for the sticker.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("description")]
	public String? Description { get; init; }

	/// <summary>
	/// Autocomplete and suggestion tags for the sticker.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("tags")]
	public String? Tags { get; init; }
}

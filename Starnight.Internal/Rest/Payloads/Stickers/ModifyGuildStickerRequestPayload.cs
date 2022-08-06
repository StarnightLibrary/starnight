namespace Starnight.Internal.Rest.Payloads.Stickers;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a payload to PATCH /guilds/:guild_id/stickers/:sticker_id.
/// </summary>
public sealed record ModifyGuildStickerRequestPayload
{
	/// <summary>
	/// Name of the sticker.
	/// </summary>
	[JsonPropertyName("name")]
	public Optional<String> Name { get; init; }

	/// <summary>
	/// Description for the sticker.
	/// </summary>
	[JsonPropertyName("description")]
	public Optional<String?> Description { get; init; }

	/// <summary>
	/// Autocomplete and suggestion tags for the sticker.
	/// </summary>
	[JsonPropertyName("tags")]
	public Optional<String> Tags { get; init; }
}

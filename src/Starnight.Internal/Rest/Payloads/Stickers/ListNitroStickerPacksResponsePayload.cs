namespace Starnight.Internal.Rest.Payloads.Stickers;

using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Stickers;

/// <summary>
/// Represents a response payload from GET /sticker-packs.
/// </summary>
public sealed record ListNitroStickerPacksResponsePayload
{
	/// <summary>
	/// The list of sticker packs available to nitro subscribers.
	/// </summary>
	[JsonPropertyName("sticker_packs")]
	public required IEnumerable<DiscordStickerPack> Packs { get; init; }
}

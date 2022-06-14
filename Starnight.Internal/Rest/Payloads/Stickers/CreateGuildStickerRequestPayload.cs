namespace Starnight.Internal.Rest.Payloads.Stickers;

using System;

/// <summary>
/// Represents a payload to POST /guilds/:guild_id/stickers.
/// </summary>
public record CreateGuildStickerRequestPayload
{
	/// <summary>
	/// Name of this sticker.
	/// </summary>
	public String Name { get; init; } = null!;

	/// <summary>
	/// Description of this sticker.
	/// </summary>
	public String Description { get; init; } = null!;

	/// <summary>
	/// Autocomplete suggestion tags for this sticker.
	/// </summary>
	public String Tags { get; init; } = null!;

	/// <summary>
	/// File contents of the sticker to upload.
	/// </summary>
	public Memory<Byte> File { get; init; }
}

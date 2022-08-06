namespace Starnight.Internal.Rest.Payloads.Stickers;

using System;

/// <summary>
/// Represents a payload to POST /guilds/:guild_id/stickers.
/// </summary>
public sealed record CreateGuildStickerRequestPayload
{
	/// <summary>
	/// Name of this sticker.
	/// </summary>
	public required String Name { get; init; }

	/// <summary>
	/// Description of this sticker.
	/// </summary>
	public required String Description { get; init; }

	/// <summary>
	/// Autocomplete suggestion tags for this sticker.
	/// </summary>
	public required String Tags { get; init; }

	/// <summary>
	/// File contents of the sticker to upload.
	/// </summary>
	public required Memory<Byte> File { get; init; }
}

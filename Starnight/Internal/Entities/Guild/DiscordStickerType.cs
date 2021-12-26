namespace Starnight.Internal.Entities.Guild;

/// <summary>
/// Represents the different types a sticker can take.
/// </summary>
public enum DiscordStickerType
{
	/// <summary>
	/// This is a standard sticker - from a pack, nitro or a removed, formerly purchasable pack.
	/// </summary>
	Standard = 1,

	/// <summary>
	/// This is a guild sticker.
	/// </summary>
	Guild
}

namespace Starnight.Internal.Rest.Resources;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Starnight.Internal.Entities.Stickers;
using Starnight.Internal.Rest.Payloads.Stickers;

/// <summary>
/// Represents a wrapper for all requests to discord's sticker rest resource.
/// </summary>
public interface IDiscordStickerRestResource
{
	/// <summary>
	/// Returns the given sticker object.
	/// </summary>
	/// <param name="stickerId">Snowflake identifier of the sticker in question.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<DiscordSticker> GetStickerAsync
	(
		Int64 stickerId,
		CancellationToken ct = default
	);

	/// <summary>
	/// Returns the list of sticker packs available to nitro subscribers.
	/// </summary>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<ListNitroStickerPacksResponsePayload> ListNitroStickerPacksAsync
	(
		CancellationToken ct = default
	);

	/// <summary>
	/// Returns the sticker objects for the given guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<IEnumerable<DiscordSticker>> ListGuildStickersAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	);

	/// <summary>
	/// Returns the specified guild sticker.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild owning this sticker.</param>
	/// <param name="stickerId">Snowflake identifier of the sticker in question.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<DiscordSticker> GetGuildStickerAsync
	(
		Int64 guildId,
		Int64 stickerId,
		CancellationToken ct = default
	);

	/// <summary>
	/// Creates a sticker in the specified guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="payload">Request payload, containing the raw, unencoded image.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>The newly created sticker object.</returns>
	/// <exception cref="ArgumentException">Thrown if the file could not be encoded to base64 for any reason.</exception>
	public ValueTask<DiscordSticker> CreateGuildStickerAsync
	(
		Int64 guildId,
		CreateGuildStickerRequestPayload payload,
		String? reason,
		CancellationToken ct = default
	);

	/// <summary>
	/// Modifies the given sticker.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild owning the sticker.</param>
	/// <param name="stickerId">Snowflake identifier of the sticker in question.</param>
	/// <param name="payload">Request payload.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>The newly updated sticker object.</returns>
	public ValueTask<DiscordSticker> ModifyGuildStickerAsync
	(
		Int64 guildId,
		Int64 stickerId,
		ModifyGuildStickerRequestPayload payload,
		String? reason,
		CancellationToken ct = default
	);

	/// <summary>
	/// Deletes the specified sticker.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild owning the sticker.</param>
	/// <param name="stickerId">Snowflake identifier of the sticker in question.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>Whether the deletion was successful.</returns>
	public ValueTask<Boolean> DeleteGuildStickerAsync
	(
		Int64 guildId,
		Int64 stickerId,
		String? reason,
		CancellationToken ct = default
	);
}

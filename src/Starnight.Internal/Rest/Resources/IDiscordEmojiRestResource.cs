namespace Starnight.Internal.Rest.Resources;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Rest.Payloads.Emojis;

/// <summary>
/// Represents a request wrapper for all request to discord's emoji rest resource.
/// </summary>
public interface IDiscordEmojiRestResource
{
	/// <summary>
	/// Fetches a list of emojis for the given guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<IEnumerable<DiscordEmoji>> ListGuildEmojisAsync
	(
		Int64 guildId,
		CancellationToken ct = default
	);

	/// <summary>
	/// Returns the specified emoji.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild owning the emoji.</param>
	/// <param name="emojiId">Snowflake identifier of the emoji in question.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<DiscordEmoji> GetGuildEmojiAsync
	(
		Int64 guildId,
		Int64 emojiId,
		CancellationToken ct = default
	);

	/// <summary>
	/// Creates a new guild emoji in the specified guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="payload">Payload for this request.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>The newly created emoji.</returns>
	public ValueTask<DiscordEmoji> CreateGuildEmojiAsync
	(
		Int64 guildId,
		CreateGuildEmojiRequestPayload payload,
		String? reason = null,
		CancellationToken ct = default
	);

	/// <summary>
	/// Modifies the given emoji.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild owning the emoji.</param>
	/// <param name="emojiId">Snowflake identifier of the emoji in question.</param>
	/// <param name="payload">Payload for this request.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>The newly updated emoji.</returns>
	public ValueTask<DiscordEmoji> ModifyGuildEmojiAsync
	(
		Int64 guildId,
		Int64 emojiId,
		ModifyGuildEmojiRequestPayload payload,
		String? reason = null,
		CancellationToken ct = default
	);

	/// <summary>
	/// Deletes the given emoji.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild owning this emoji.</param>
	/// <param name="emojiId">Snowflake identifier of the emoji to be deleted.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>Whether the deletion was successful.</returns>
	public ValueTask<Boolean> DeleteGuildEmojiAsync
	(
		Int64 guildId,
		Int64 emojiId,
		String? reason = null,
		CancellationToken ct = default
	);
}

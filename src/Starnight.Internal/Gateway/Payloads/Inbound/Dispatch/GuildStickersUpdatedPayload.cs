namespace Starnight.Internal.Gateway.Payloads.Inbound.Dispatch;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Stickers;

/// <summary>
/// Represents the payload of the GuildStickersUpdated event.
/// </summary>
public sealed record GuildStickersUpdatedPayload
{
	/// <summary>
	/// The ID of the guild this is taking place in.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public required Int64 GuildId { get; init; }

	/// <summary>
	/// The new list of stickers for this guild.
	/// </summary>
	[JsonPropertyName("stickers")]
	public required IEnumerable<DiscordSticker> Stickers { get; init; }
}

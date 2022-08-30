namespace Starnight.Internal.Gateway.Payloads.Clientbound.Dispatch;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Users;

/// <summary>
/// Represents the payload of a GuildBanAdded event.
/// </summary>
public sealed record GuildBanAddedPayload
{
    /// <summary>
    /// ID of the guild this ban was added in.
    /// </summary>
    [JsonPropertyName("guild_id")]
    public required Int64 GuildId { get; init; }

    /// <summary>
    /// The newly banned user.
    /// </summary>
    [JsonPropertyName("user")]
    public required DiscordUser User { get; init; }
}

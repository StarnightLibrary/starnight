namespace Starnight.Internal.Rest.Payloads.Guilds;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Channels;

/// <summary>
/// Represents the REST payload for POST /guilds/:guild_id/channels
/// </summary>
public sealed record CreateGuildChannelRequestPayload
{
    /// <summary>
    /// The name of the channel to be created.
    /// </summary>
    [JsonPropertyName("name")]
    public required String Name { get; init; }

    /// <summary>
    /// The channel type.
    /// </summary>
    [JsonPropertyName("type")]
    public Optional<DiscordChannelType?> Type { get; init; }

    /// <summary>
    /// The channel topic.
    /// </summary>
    [JsonPropertyName("topic")]
    public Optional<String?> Topic { get; init; }

    /// <summary>
    /// The voice channel bitrate.
    /// </summary>
    [JsonPropertyName("bitrate")]
    public Optional<Int32?> Bitrate { get; init; }

    /// <summary>
    /// The voice channel user limit.
    /// </summary>
    [JsonPropertyName("user_limit")]
    public Optional<Int32?> UserLimit { get; init; }

    /// <summary>
    /// The user slowmode in seconds.
    /// </summary>
    [JsonPropertyName("rate_limit_per_user")]
    public Optional<Int32?> Slowmode { get; init; }

    /// <summary>
    /// The sorting position in the channel list for this channel.
    /// </summary>
    [JsonPropertyName("position")]
    public Optional<Int32?> Position { get; init; }

    /// <summary>
    /// The permission overwrites for this channel.
    /// </summary>
    [JsonPropertyName("permission_overwrites")]
    public Optional<IEnumerable<DiscordChannelOverwrite>?> PermissionOverwrites { get; init; }

    /// <summary>
    /// The category channel ID for this channel.
    /// </summary>
    [JsonPropertyName("parent_id")]
    public Optional<Int64?> ParentChannelId { get; init; }

    /// <summary>
    /// Whether this channel is to be created as NSFW.
    /// </summary>
    [JsonPropertyName("nsfw")]
    public Optional<Boolean?> Nsfw { get; init; }
}

namespace Starnight.Internal.Rest.Payloads.Guilds;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guilds;

/// <summary>
/// Represents the REST payload for PATCH guilds/:guild_id.
/// </summary>
public sealed record ModifyGuildRequestPayload
{
    /// <summary>
    /// The name of the guild in question.
    /// </summary>
    [JsonPropertyName("name")]
    public Optional<String> Name { get; init; }

    /// <summary>
    /// The verification level of the guild in question.
    /// </summary>
    [JsonPropertyName("verification_level")]
    public Optional<DiscordGuildVerificationLevel?> VerificationLevel { get; init; }

    /// <summary>
    /// The default message notification level of the guild in question.
    /// </summary>
    [JsonPropertyName("default_message_notifications")]
    public Optional<DiscordGuildMessageNotificationsLevel?> NotificationsLevel { get; init; }

    /// <summary>
    /// The explicit content filter level for the guild in question.
    /// </summary>
    [JsonPropertyName("explicit_content_filter")]
    public Optional<DiscordGuildExplicitContentFilterLevel?> ExplicitContentFilterLevel { get; init; }

    /// <summary>
    /// The snowflake identifier of the AFK channel for the guild in question.
    /// </summary>
    [JsonPropertyName("afk_channel_id")]
    public Optional<Int64?> AfkChannelId { get; init; }

    [JsonPropertyName("afk_timeout")]
    public Optional<Int32> AfkTimeout { get; init; }

    /// <summary>
    /// The guild icon for this guild; in base64, prefixed with metadata.
    /// </summary>
    [JsonPropertyName("icon")]
    public Optional<String?> Icon { get; init; }

    /// <summary>
    /// The snowflake identifier of this guild's owner. Used to transfer guild ownership.
    /// </summary>
    [JsonPropertyName("owner_id")]
    public Optional<Int64> OwnerId { get; init; }

    /// <summary>
    /// The guild splash for this guild; in base64, prefixed with metadata.
    /// </summary>
    [JsonPropertyName("splash")]
    public Optional<String?> Splash { get; init; }

    /// <summary>
    /// The guild discovery splash for this guild; in base64, prefixed with metadata.
    /// </summary>
    [JsonPropertyName("discovery_splash")]
    public Optional<String?> DiscoverySplash { get; init; }

    /// <summary>
    /// The guild banner for this guild; in base64, prefixed with metadata.
    /// </summary>
    [JsonPropertyName("banner")]
    public Optional<String?> Banner { get; init; }

    /// <summary>
    /// The system channel snowflake identifier for this guild.
    /// </summary>
    [JsonPropertyName("system_channel_id")]
    public Optional<Int64?> SystemChannelId { get; init; }

    /// <summary>
    /// The system channel flags for this guild.
    /// </summary>
    [JsonPropertyName("system_channel_flags")]
    public Optional<DiscordGuildSystemChannelFlags> SystemChannelFlags { get; init; }

    /// <summary>
    /// The rules channel snowflake identifier for this guild.
    /// </summary>
    [JsonPropertyName("rules_channel_id")]
    public Optional<Int64?> RulesChannelId { get; init; }

    /// <summary>
    /// The public update channel snowflake identifier for this guild.
    /// </summary>
    [JsonPropertyName("public_updates_channel_id")]
    public Optional<Int64?> UpdateChannelId { get; init; }

    /// <summary>
    /// The preferred locale for this community guild.
    /// </summary>
    [JsonPropertyName("preferred_locale")]
    public Optional<String?> PreferredLocale { get; init; }

    /// <summary>
    /// The enabled guild features for this guild.
    /// </summary>
    [JsonPropertyName("features")]
    public Optional<IEnumerable<String>> Features { get; init; }

    /// <summary>
    /// The description for this guild, if it is discoverable.
    /// </summary>
    [JsonPropertyName("description")]
    public Optional<String?> Description { get; init; }

    /// <summary>
    /// Whether the guild should have a boost progress bar.
    /// </summary>
    [JsonPropertyName("premium_progress_bar_enabled")]
    public Optional<Boolean> BoostProgressBarEnabled { get; init; }
}

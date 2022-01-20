namespace Starnight.Internal.Rest.Payloads.Guild;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guilds;

/// <summary>
/// Represents the REST payload for PATCH guilds/:guild_id.
/// </summary>
public record ModifyGuildRequestPayload
{
	/// <summary>
	/// The name of the guild in question
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	[JsonPropertyName("name")]
	public String? Name { get; init; }

	/// <summary>
	/// The verification level of the guild in question.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("verification_level")]
	public DiscordGuildVerificationLevel? VerificationLevel { get; init; }

	/// <summary>
	/// The default message notification level of the guild in question.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("default_message_notifications")]
	public DiscordGuildMessageNotificationsLevel? NotificationsLevel { get; init; }

	/// <summary>
	/// The explicit content filter level for the guild in question.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("explicit_content_filter")]
	public DiscordGuildExplicitContentFilterLevel? ExplicitContentFilterLevel { get; init; }

	/// <summary>
	/// The snowflake identifier of the AFK channel for the guild in question.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("afk_channel_id")]
	public Int64? AfkChannelId { get; init; }

	/// <summary>
	/// The guild icon for this guild; in base64, prefixed with metadata.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	[JsonPropertyName("icon")]
	public String? Icon { get; init; }

	/// <summary>
	/// The snowflake identifier of this guild's owner. Used to transfer guild ownership.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("owner_id")]
	public Int64? OwnerId { get; init; }

	/// <summary>
	/// The guild splash for this guild; in base64, prefixed with metadata.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	[JsonPropertyName("splash")]
	public String? Splash { get; init; }

	/// <summary>
	/// The guild discovery splash for this guild; in base64, prefixed with metadata.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	[JsonPropertyName("discovery_splash")]
	public String? DiscoverySplash { get; init; }

	/// <summary>
	/// The guild banner for this guild; in base64, prefixed with metadata.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	[JsonPropertyName("banner")]
	public String? Banner { get; init; }

	/// <summary>
	/// The system channel snowflake identifier for this guild.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("system_channel_id")]
	public Int64? SystemChannelId { get; init; }

	/// <summary>
	/// The system channel flags for this guild.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("system_channel_flags")]
	public DiscordGuildSystemChannelFlags? SystemChannelFlags { get; init; }

	/// <summary>
	/// The rules channel snowflake identifier for this guild.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("rules_channel_id")]
	public Int64? RulesChannelId { get; init; }

	/// <summary>
	/// The public update channel snowflake identifier for this guild.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("public_updates_channel_id")]
	public Int64? UpdateChannelId { get; init; }

	/// <summary>
	/// The preferred locale for this community guild.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	[JsonPropertyName("preferred_locale")]
	public String? PreferredLocale { get; init; }

	/// <summary>
	/// The enabled guild features for this guild.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	[JsonPropertyName("features")]
	public String[]? Features { get; init; }

	/// <summary>
	/// The description for this guild, if it is discoverable.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	[JsonPropertyName("description")]
	public String? Description { get; init; }

	/// <summary>
	/// Whether the guild should have a boost progress bar.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	[JsonPropertyName("premium_progress_bar_enabled")]
	public Boolean? BoostProgressBarEnabled { get; init; }
}

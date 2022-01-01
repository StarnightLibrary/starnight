namespace Starnight.Internal.Entities.Guild;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Channel;
using Starnight.Internal.Entities.Sticker;
using Starnight.Internal.Entities.Voice;
using Starnight.Internal.Gateway.Objects.User;

/// <summary>
/// Represents a discord guild.
/// </summary>
public class DiscordGuild : DiscordSnowflakeObject
{
	/// <summary>
	/// Guild name; 2 - 100 characters, with no trailing or leading whitespace.
	/// </summary>
	[JsonPropertyName("name")]
	public String Name { get; init; } = default!;

	/// <summary>
	/// Icon hash for this guild.
	/// </summary>
	[JsonPropertyName("icon")]
	public String? Icon { get; init; }

	/// <summary>
	/// Another icon hash for this guild, invoked through the Guild Template object.
	/// </summary>
	[JsonPropertyName("icon_hash")]
	public String? IconHash { get; init; }

	/// <summary>
	/// Splash hash for this guild.
	/// </summary>
	[JsonPropertyName("splash")]
	public String? Splash { get; init; }

	/// <summary>
	/// Discovery splash hash for this guild; only present if this guild is discoverable.
	/// </summary>
	[JsonPropertyName("discovery_splash")]
	public String? DiscoverySplash { get; init; }

	/// <summary>
	/// Whether the current user owns this guild
	/// </summary>
	[JsonPropertyName("owner")]
	public Boolean? CurrentOwner { get; init; }

	/// <summary>
	/// Snowflake identifier of the guild owner.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("owner_id")]
	public Int64 OwnerId { get; init; }

	/// <summary>
	/// Total permissions for the current user in this guild.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("permissions")]
	public Int64? Permissions { get; init; }

	/// <summary>
	/// Voice region ID for this guild.
	/// </summary>
	[JsonPropertyName("region")]
	[Obsolete("This feature was removed from the API", DiagnosticId = "SE0005")]
	public String? VoiceRegion { get; init; }

	/// <summary>
	/// Snowflake identifier of this guilds AFK voice channel.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("afk_channel_id")]
	public Int64? AfkChannelId { get; init; }

	/// <summary>
	/// Voice AFK timeout in seconds.
	/// </summary>
	[JsonPropertyName("afk_timeout")]
	public Int32 AfkTimeout { get; init; }

	/// <summary>
	/// Whether the server widget is enabled.
	/// </summary>
	[JsonPropertyName("widget_enabled")]
	public Boolean? ServerWidgetEnabled { get; init; }

	/// <summary>
	/// Snowflake identifier of this guilds widget channel.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("widget_channel_id")]
	public Int64? WidgetChannelId { get; init; }

	/// <summary>
	/// The verification level required to message in this guild.
	/// </summary>
	[JsonPropertyName("verification_level")]
	public DiscordGuildVerificationLevel VerificationLevel { get; init; }

	/// <summary>
	/// The default notification level for members of this guild.
	/// </summary>
	[JsonPropertyName("default_message_notifications")]
	public DiscordGuildMessageNotificationsLevel NotificationLevel { get; init; }

	/// <summary>
	/// The explicit content filter level of this guild.
	/// </summary>
	[JsonPropertyName("explicit_content_filter")]
	public DiscordGuildExplicitContentFilterLevel ExplicitContentFilterLevel { get; init; }

	/// <summary>
	/// Roles in this guild.
	/// </summary>
	[JsonPropertyName("roles")]
	public DiscordRole[] Roles { get; init; } = default!;

	/// <summary>
	/// Emotes in this guild.
	/// </summary>
	[JsonPropertyName("emojis")]
	public DiscordEmote[] Emotes { get; init; } = default!;

	/// <summary>
	/// Enabled features for this guild.
	/// </summary>
	[JsonPropertyName("features")]
	public String[] Features { get; init; } = default!;

	/// <summary>
	/// Required MFA level for moderation actions in this guild.
	/// </summary>
	[JsonPropertyName("mfa_level")]
	public DiscordGuildMultiFactorAuthLevel MFALevel { get; init; }

	/// <summary>
	/// Application ID of the guild creator if it was created by a bot.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("application_id")]
	public Int64? ApplicationId { get; init; }

	/// <summary>
	/// System channel ID for this guild.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("system_channel_id")]
	public Int64? SystemChannelId { get; init; }

	/// <summary>
	/// System channel flags for this guild.
	/// </summary>
	[JsonPropertyName("system_channel_flags")]
	public DiscordGuildSystemChannelFlags SystemChannelFlags { get; init; }

	/// <summary>
	/// Rule channel ID for this guild.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("rules_channel_id")]
	public Int64? RuleChannelId { get; init; }

	/// <summary>
	/// When the current user joined the guild.
	/// </summary>
	[JsonPropertyName("joined_at")]
	public DateTime? JoinedAt { get; init; }

	/// <summary>
	/// Whether this guild is considered a large guild by discord.
	/// </summary>
	[JsonPropertyName("large")]
	public Boolean? Large { get; init; }

	/// <summary>
	/// Whether this guild is affected by an outage.
	/// </summary>
	[JsonPropertyName("unavailable")]
	public Boolean? Unavailable { get; init; }

	/// <summary>
	/// Member count for this guild.
	/// </summary>
	[JsonPropertyName("member_count")]
	public Int32? MemberCount { get; init; }

	/// <summary>
	/// Voice states for all members currently in voice channels.
	/// </summary>
	[JsonPropertyName("voice_states")]
	public DiscordVoiceState[]? VoiceStates { get; init; }

	/// <summary>
	/// All members in this guild.
	/// </summary>
	[JsonPropertyName("members")]
	public DiscordGuildMember[]? Members { get; init; }

	/// <summary>
	/// All channels in this guild.
	/// </summary>
	[JsonPropertyName("channels")]
	public DiscordChannel[]? Channels { get; init; }

	/// <summary>
	/// All threads in this guild that the current user has permission to view.
	/// </summary>
	[JsonPropertyName("threads")]
	public DiscordChannel[]? Threads { get; init; }

	/// <summary>
	/// Presences of the guild members. Offline members will be excluded from a certain point onwards.
	/// </summary>
	[JsonPropertyName("presences")]
	public DiscordPresenceUpdateObject[]? Presences { get; init; }

	/// <summary>
	/// The maximum number of presences for this guild. <c>null</c> everywhere save the largest guilds.
	/// </summary>
	[JsonPropertyName("max_presences")]
	public Int32? MaximumPresences { get; init; }

	/// <summary>
	/// The member limit for this guild.
	/// </summary>
	[JsonPropertyName("max_members")]
	public Int32? MaximumMembers { get; init; }

	/// <summary>
	/// The vanity invite URL for this guild.
	/// </summary>
	[JsonPropertyName("vanity_url_code")]
	public String? VanityUrl { get; init; }

	/// <summary>
	/// The description of this community guild.
	/// </summary>
	[JsonPropertyName("description")]
	public String? Description { get; init; }

	/// <summary>
	/// Hash of this guild's banner.
	/// </summary>
	[JsonPropertyName("banner")]
	public String? BannerHash { get; init; }

	/// <summary>
	/// Server boost level of this guild.
	/// </summary>
	[JsonPropertyName("premium_tier")]
	public Int32 BoostLevel { get; init; }

	/// <summary>
	/// The number of active server boosts for this guild.
	/// </summary>
	[JsonPropertyName("premium_subscription_count")]
	public Int32? ActiveBoosts { get; init; }

	/// <summary>
	/// The preferred locale of this community guild, defaults to "en-US".
	/// </summary>
	[JsonPropertyName("preferred_locale")]
	public String Locale { get; init; } = default!;

	/// <summary>
	/// Channel ID of the channel discord uses to send update notices.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("public_updates_channel_id")]
	public Int64? UpdateChannelId { get; init; }

	/// <summary>
	/// Maximum amount of users in a video channel.
	/// </summary>
	[JsonPropertyName("max_video_channel_users")]
	public Int32? MaximumVideoChannelUsers { get; init; }

	/// <summary>
	/// Approximate number of members in this guild.
	/// </summary>
	[JsonPropertyName("approximate_member_count")]
	public Int32? ApproximateMemberCount { get; init; }

	/// <summary>
	/// Approximate number of non-offline members in this guild.
	/// </summary>
	[JsonPropertyName("approximate_presence_count")]
	public Int32? ApproximatePresenceCount { get; init; }

	/// <summary>
	/// The welcome screen of this community guild.
	/// </summary>
	[JsonPropertyName("welcome_screen")]
	public DiscordGuildWelcomeScreen? WelcomeScreen { get; init; }

	/// <summary>
	/// Guild NSFW level.
	/// </summary>
	[JsonPropertyName("nsfw_level")]
	public DiscordGuildNsfwLevel NsfwLevel { get; init; }

	/// <summary>
	/// All active stage instances in this guild.
	/// </summary>
	[JsonPropertyName("stage_instances")]
	public DiscordStageInstance[]? Stages { get; init; }

	/// <summary>
	/// All stickers in this guild.
	/// </summary>
	[JsonPropertyName("stickers")]
	public DiscordSticker[]? Stickers { get; init; }

	/// <summary>
	/// All scheduled events for this guild.
	/// </summary>
	[JsonPropertyName("guild_scheduled_events")]
	public DiscordScheduledEvent[]? ScheduledEvents { get; init; }

	/// <summary>
	/// Whether this guild has the boost progress bar enabled.
	/// </summary>
	[JsonPropertyName("premium_progress_bar_enabled")]
	public Boolean BoostProgressBarEnabled { get; init; }
}

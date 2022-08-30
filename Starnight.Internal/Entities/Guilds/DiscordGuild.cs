namespace Starnight.Internal.Entities.Guilds;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Stickers;
using Starnight.Internal.Entities.Users;
using Starnight.Internal.Entities.Voice;

/// <summary>
/// Represents a discord guild.
/// </summary>
public sealed record DiscordGuild : DiscordSnowflakeObject
{
	/// <summary>
	/// Guild name; 2 - 100 characters, with no trailing or leading whitespace.
	/// </summary>
	[JsonPropertyName("name")]
	public required String Name { get; init; }

	/// <summary>
	/// Icon hash for this guild.
	/// </summary>
	[JsonPropertyName("icon")]
	public String? Icon { get; init; }

	/// <summary>
	/// Another icon hash for this guild, invoked through the Guild Template object.
	/// </summary>
	[JsonPropertyName("icon_hash")]
	public Optional<String?> IconHash { get; init; }

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
	public Optional<Boolean> CurrentOwner { get; init; }

	/// <summary>
	/// Snowflake identifier of the guild owner.
	/// </summary>
	[JsonPropertyName("owner_id")]
	public required Int64 OwnerId { get; init; }

	/// <summary>
	/// Total permissions for the current user in this guild.
	/// </summary>
	[JsonPropertyName("permissions")]
	public Optional<Int64> Permissions { get; init; }

	/// <summary>
	/// Snowflake identifier of this guilds AFK voice channel.
	/// </summary>
	[JsonPropertyName("afk_channel_id")]
	public Int64? AfkChannelId { get; init; }

	/// <summary>
	/// Voice AFK timeout in seconds.
	/// </summary>
	[JsonPropertyName("afk_timeout")]
	public required Int32 AfkTimeout { get; init; }

	/// <summary>
	/// Whether the server widget is enabled.
	/// </summary>
	[JsonPropertyName("widget_enabled")]
	public Optional<Boolean> ServerWidgetEnabled { get; init; }

	/// <summary>
	/// Snowflake identifier of this guilds widget channel.
	/// </summary>
	[JsonPropertyName("widget_channel_id")]
	public Optional<Int64?> WidgetChannelId { get; init; }

	/// <summary>
	/// The verification level required to message in this guild.
	/// </summary>
	[JsonPropertyName("verification_level")]
	public required DiscordGuildVerificationLevel VerificationLevel { get; init; }

	/// <summary>
	/// The default notification level for members of this guild.
	/// </summary>
	[JsonPropertyName("default_message_notifications")]
	public required DiscordGuildMessageNotificationsLevel NotificationLevel { get; init; }

	/// <summary>
	/// The explicit content filter level of this guild.
	/// </summary>
	[JsonPropertyName("explicit_content_filter")]
	public required DiscordGuildExplicitContentFilterLevel ExplicitContentFilterLevel { get; init; }

	/// <summary>
	/// Roles in this guild.
	/// </summary>
	[JsonPropertyName("roles")]
	public required IEnumerable<DiscordRole> Roles { get; init; }

	/// <summary>
	/// Emojis in this guild.
	/// </summary>
	[JsonPropertyName("emojis")]
	public required IEnumerable<DiscordEmoji> Emojis { get; init; }

	/// <summary>
	/// Enabled features for this guild.
	/// </summary>
	[JsonPropertyName("features")]
	public required IEnumerable<String> Features { get; init; }

	/// <summary>
	/// Required MFA level for moderation actions in this guild.
	/// </summary>
	[JsonPropertyName("mfa_level")]
	public required DiscordGuildMultiFactorAuthLevel MFALevel { get; init; }

	/// <summary>
	/// Application ID of the guild creator if it was created by a bot.
	/// </summary>
	[JsonPropertyName("application_id")]
	public Int64? ApplicationId { get; init; }

	/// <summary>
	/// System channel ID for this guild.
	/// </summary>
	[JsonPropertyName("system_channel_id")]
	public Int64? SystemChannelId { get; init; }

	/// <summary>
	/// System channel flags for this guild.
	/// </summary>
	[JsonPropertyName("system_channel_flags")]
	public required DiscordGuildSystemChannelFlags SystemChannelFlags { get; init; }

	/// <summary>
	/// Rule channel ID for this guild.
	/// </summary>
	[JsonPropertyName("rules_channel_id")]
	public Int64? RuleChannelId { get; init; }

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
	public required DiscordGuildBoostLevel BoostLevel { get; init; }

	/// <summary>
	/// The number of active server boosts for this guild.
	/// </summary>
	[JsonPropertyName("premium_subscription_count")]
	public Optional<Int32> ActiveBoosts { get; init; }

	/// <summary>
	/// The preferred locale of this community guild, defaults to "en-US".
	/// </summary>
	[JsonPropertyName("preferred_locale")]
	public required String Locale { get; init; }

	/// <summary>
	/// Channel ID of the channel discord uses to send update notices.
	/// </summary>
	[JsonPropertyName("public_updates_channel_id")]
	public Int64? UpdateChannelId { get; init; }

	/// <summary>
	/// Maximum amount of users in a video channel.
	/// </summary>
	[JsonPropertyName("max_video_channel_users")]
	public Optional<Int32> MaximumVideoChannelUsers { get; init; }

	/// <summary>
	/// Approximate number of members in this guild.
	/// </summary>
	[JsonPropertyName("approximate_member_count")]
	public Optional<Int32> ApproximateMemberCount { get; init; }

	/// <summary>
	/// Approximate number of non-offline members in this guild.
	/// </summary>
	[JsonPropertyName("approximate_presence_count")]
	public Optional<Int32> ApproximatePresenceCount { get; init; }

	/// <summary>
	/// The welcome screen of this community guild.
	/// </summary>
	[JsonPropertyName("welcome_screen")]
	public Optional<DiscordGuildWelcomeScreen> WelcomeScreen { get; init; }

	/// <summary>
	/// Guild NSFW level.
	/// </summary>
	[JsonPropertyName("nsfw_level")]
	public required DiscordGuildNsfwLevel NsfwLevel { get; init; }

	/// <summary>
	/// All stickers in this guild.
	/// </summary>
	[JsonPropertyName("stickers")]
	public Optional<IEnumerable<DiscordSticker>> Stickers { get; init; }

	/// <summary>
	/// Whether this guild has the boost progress bar enabled.
	/// </summary>
	[JsonPropertyName("premium_progress_bar_enabled")]
	public required Boolean BoostProgressBarEnabled { get; init; }

	/// <summary>
	/// Specifies when the current user joined this guild.
	/// </summary>
	[JsonPropertyName("joined_at")]
	public Optional<DateTimeOffset> JoinedAt { get; init; }

	/// <summary>
	/// Whether this guild is considered a large guild.
	/// </summary>
	[JsonPropertyName("large")]
	public Optional<Boolean> Large { get; init; }

	/// <summary>
	/// Whether this guild is currently unavailable due to an outage.
	/// </summary>
	[JsonPropertyName("unavailable")]
	public Optional<Boolean> Unavailable { get; init; }

	/// <summary>
	/// The total number of members in this guild
	/// </summary>
	[JsonPropertyName("member_count")]
	public Optional<Int32> MemberCount { get; init; }

	/// <summary>
	/// Voice states of members currently in voice channels.
	/// </summary>
	[JsonPropertyName("voice_states")]
	public Optional<IEnumerable<DiscordVoiceState>> VoiceStates { get; init; }

	/// <summary>
	/// Members of this guild.
	/// </summary>
	[JsonPropertyName("members")]
	public Optional<IEnumerable<DiscordGuildMember>> Members { get; init; }

	/// <summary>
	/// Channels in this guild.
	/// </summary>
	[JsonPropertyName("channels")]
	public Optional<IEnumerable<DiscordChannel>> Channels { get; init; }

	/// <summary>
	/// Threads in this guild.
	/// </summary>
	[JsonPropertyName("threads")]
	public Optional<IEnumerable<DiscordChannel>> Threads { get; init; }

	/// <summary>
	/// Presences of the members in the guild.
	/// </summary>
	[JsonPropertyName("presences")]
	public Optional<IEnumerable<DiscordPresence>> Presences { get; init; }

	/// <summary>
	/// Stage instances in this guild.
	/// </summary>
	[JsonPropertyName("stage_instances")]
	public Optional<IEnumerable<DiscordStageInstance>> StageInstances { get; init; }

	/// <summary>
	/// Scheduled events for this guild.
	/// </summary>
	[JsonPropertyName("guild_scheduled_events")]
	public Optional<IEnumerable<DiscordScheduledEvent>> ScheduledEvents { get; init; }
}

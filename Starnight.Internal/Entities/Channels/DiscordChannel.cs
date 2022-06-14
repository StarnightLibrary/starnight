namespace Starnight.Internal.Entities.Channels;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Channels.Threads;
using Starnight.Internal.Entities.Users;

/// <summary>
/// Represents any sort, shape or form of discord channel.
/// </summary>
public record DiscordChannel : DiscordSnowflakeObject
{
	/// <summary>
	/// Discord channel type.
	/// </summary>
	[JsonPropertyName("type")]
	public DiscordChannelType ChannelType { get; init; }

	/// <summary>
	/// Snowflake identifier of the guild this channel belongs to.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("guild_id")]
	public Int64? GuildId { get; init; }

	/// <summary>
	/// Position in the channel list, as rendered on the client.
	/// </summary>
	[JsonPropertyName("position")]
	public Int32? Position { get; init; }

	/// <summary>
	/// Array of all permission overwrites for this channel.
	/// </summary>
	[JsonPropertyName("permission_overwrites")]
	public IEnumerable<DiscordChannelOverwrite>? Overwrites { get; init; }

	/// <summary>
	/// Channel name, 1 - 100 characters.
	/// </summary>
	[JsonPropertyName("name")]
	public String? Name { get; init; }

	/// <summary>
	/// Channel topic, 0 - 1024 characters.
	/// </summary>
	[JsonPropertyName("topic")]
	public String? Topic { get; init; }

	/// <summary>
	/// Gets whether the channel is marked as NSFW.
	/// </summary>
	[JsonPropertyName("nsfw")]
	public Boolean? Nsfw { get; init; }

	/// <summary>
	/// The ID of the last message sent in this channel. This may not be an existing or even valid message.
	/// If this channel is a forum channel, this instead points to the ID of the latest thread.
	/// </summary>
	[JsonPropertyName("last_message_id")]
	public Int64? LastMessageId { get; init; }

	/// <summary>
	/// The bitrate of this voice channel (given it is a voice channel).
	/// </summary>
	[JsonPropertyName("bitrate")]
	public Int32? Bitrate { get; init; }

	/// <summary>
	/// The user limit of this voice channel (given it is a voice channel.)
	/// </summary>
	[JsonPropertyName("user_limit")]
	public Int32? UserLimit { get; init; }

	/// <summary>
	/// The amount of seconds (0 - 21600) a user has to wait before sending another message/creating another thread.
	/// Bots, as well as users with <see cref="DiscordPermissions.ManageMessages"/> or <see cref="DiscordPermissions.ManageChannels"/>,
	/// are unaffected.
	/// </summary>
	[JsonPropertyName("rate_limit_per_user")]
	public Int32 Slowmode { get; init; }

	/// <summary>
	/// The recipients (up to 10) of this DM channel (given it is a DM channel.)
	/// </summary>
	[JsonPropertyName("recipients")]
	public IEnumerable<DiscordUser>? Recipients { get; init; }

	/// <summary>
	/// Hash code of this channels icon.
	/// </summary>
	[JsonPropertyName("icon")]
	public String? Icon { get; init; }

	/// <summary>
	/// Creator of this group DM or thread.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("owner_id")]
	public Int64? OwnerId { get; init; }

	/// <summary>
	/// Application ID of the group DM creator if it was created by a bot.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("application_id")]
	public Int64? ApplicationId { get; init; }

	/// <summary>
	/// Parent of this channel. For guild channels: the parent category; for threads: the parent text channel.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("parent_id")]
	public Int64? ParentId { get; init; }

	/// <summary>
	/// Timestamp of the last pin action.
	/// </summary>
	[JsonPropertyName("last_pin_timestamp")]
	public DateTime? LastPinTime { get; init; }

	/// <summary>
	/// Voice region ID for this channel.
	/// </summary>
	[JsonPropertyName("rtc_region")]
	public String? RtcRegion { get; init; }

	/// <summary>
	/// Camera video quality mode of this voice channel.
	/// </summary>
	[JsonPropertyName("video_quality_mode")]
	public DiscordVideoQualityMode? VideoQualityMode { get; init; }

	/// <summary>
	/// An approximate counter for thread messages, stops counting at 50.
	/// </summary>
	[JsonPropertyName("message_count")]
	public Int32? MessageCount { get; init; }

	/// <summary>
	/// An approximate count of members in a thread, stops counting at 50.
	/// </summary>
	[JsonPropertyName("member_count")]
	public Int32? MemberCount { get; init; }

	/// <summary>
	/// Thread-specific fields.
	/// </summary>
	[JsonPropertyName("thread_metadata")]
	public DiscordThreadMetadata? ThreadMetadata { get; init; }

	/// <summary>
	/// Thread member object for the current bot if it has joined the thread. Only included on certain endpoints.
	/// </summary>
	[JsonPropertyName("member")]
	public DiscordThreadMember? ThreadMember { get; init; }

	/// <summary>
	/// Default auto archive duration clients will use in this channel. Can be set to 60, 1440, 4320 and 10080.
	/// </summary>
	[JsonPropertyName("default_auto_archive_duration")]
	public Int32? DefaultAutoArchiveDuration { get; init; }

	/// <summary>
	/// Permissions for this bot in this channel, including overwrites. This is only sent as part of the <c>resolved</c>
	/// data received on an application command interaction.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("permissions")]
	public Int64? Permissions { get; init; }

	/// <summary>
	/// Channel flags for this channel.
	/// </summary>
	[JsonPropertyName("flags")]
	public DiscordChannelFlags Flags { get; init; }
}

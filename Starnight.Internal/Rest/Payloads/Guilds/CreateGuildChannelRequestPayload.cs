namespace Starnight.Internal.Rest.Payloads.Guilds;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Channels.Threads;

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

	/// <summary>
	/// Channel voice region ID for this voice/stage channel.
	/// </summary>
	[JsonPropertyName("trc_region")]
	public Optional<String?> RtcRegion { get; init; }

	/// <summary>
	/// Indicates the camera video quality mode of this channel.
	/// </summary>
	[JsonPropertyName("video_quality_mode")]
	public Optional<DiscordVideoQualityMode?> VideoQualityMode { get; init; }

	/// <summary>
	/// The default auto archive duration clients use for newly created threads in this channel.
	/// </summary>
	[JsonPropertyName("default_auto_archive_duration")]
	public Optional<Int32?> DefaultAutoArchiveDuration { get; init; }

	/// <summary>
	/// Default reaction for threads in this forum channel.
	/// </summary>
	[JsonPropertyName("default_reaction_emoji")]
	public Optional<DiscordDefaultForumReaction?> DefaultReactionEmoji { get; init; }

	/// <summary>
	/// The set of tags that can be used in this forum channel.
	/// </summary>
	[JsonPropertyName("available_tags")]
	public Optional<IEnumerable<DiscordForumTag>?> AvailableTags { get; init; }

	/// <summary>
	/// The default sort order for this forum channel.
	/// </summary>
	[JsonPropertyName("default_sort_order")]
	public Optional<DiscordDefaultThreadSortOrder?> DefaultSortOrder { get; init; }
}

namespace Starnight.Internal.Rest.Payloads.Channels;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Channels;

/// <summary>
/// Represents a payload to PATCH /channels/:channel_id, where the channel ID points to a guild channel.
/// </summary>
public sealed record ModifyGuildChannelRequestPayload
{
	/// <summary>
	/// New channel name.
	/// </summary>
	[JsonPropertyName("name")]
	public Optional<String> Name { get; init; }

	/// <summary>
	/// New channel type for this channel. Only converting between <see cref="DiscordChannelType.GuildText"/> and
	/// <see cref="DiscordChannelType.GuildNews"/> is supported.
	/// </summary>
	[JsonPropertyName("type")]
	public Optional<DiscordChannelType> Type { get; init; }

	/// <summary>
	/// New position for this channel in the channel list.
	/// </summary>
	[JsonPropertyName("position")]
	public Optional<Int32?> Position { get; init; }

	/// <summary>
	/// New channel topic.
	/// </summary>
	[JsonPropertyName("topic")]
	public Optional<String?> ChannelTopic { get; init; }

	/// <summary>
	/// The new NSFW status for this channel.
	/// </summary>
	[JsonPropertyName("nsfw")]
	public Optional<Boolean?> Nsfw { get; init; }

	/// <summary>
	/// Slowmode for this channel in seconds.
	/// </summary>
	[JsonPropertyName("rate_limit_per_user")]
	public Optional<Int32?> Slowmode { get; init; }

	/// <summary>
	/// New bitrate for this voice channel.
	/// </summary>
	[JsonPropertyName("bitrate")]
	public Optional<Int32?> Bitrate { get; init; }

	/// <summary>
	/// New user limit for this voice channel. 0 represents no limit, 1 - 99 represents a limit.
	/// </summary>
	[JsonPropertyName("user_limit")]
	public Optional<Int32?> UserLimit { get; init; }

	/// <summary>
	/// New permission overwrites for this channel or category.
	/// </summary>
	[JsonPropertyName("permission_overwrites")]
	public Optional<IEnumerable<DiscordChannelOverwrite>?> PermissionOverwrites { get; init; }

	/// <summary>
	/// Snowflake identifier of the new parent category channel.
	/// </summary>
	[JsonPropertyName("parent_id")]
	public Optional<Int64?> ParentId { get; init; }

	/// <summary>
	/// Channel voice region ID, automatic when set to <c>null</c>.
	/// </summary>
	[JsonPropertyName("rtc_region")]
	public Optional<String?> VoiceChannelRegion { get; init; }

	/// <summary>
	/// The new camera video quality mode for this channel.
	/// </summary>
	[JsonPropertyName("video_quality_mode")]
	public Optional<DiscordVideoQualityMode?> VideoQualityMode { get; init; }

	/// <summary>
	/// New default auto archive duration for threads as used by the discord client.
	/// </summary>
	[JsonPropertyName("default_auto_archive_duration")]
	public Optional<Int32?> DefaultAutoArchiveDuration { get; init; }
}

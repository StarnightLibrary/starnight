namespace Starnight.Internal.Rest.Payloads.Channels;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Converters;
using Starnight.Internal.Entities.Channels;

/// <summary>
/// Represents a payload to PATCH /channels/:channel_id, where the channel ID points to a guild channel.
/// </summary>
public record ModifyGuildChannelRequestPayload
{
	/// <summary>
	/// New channel name.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("name")]
	public String? Name { get; init; }

	/// <summary>
	/// New channel type for this channel. Only converting between <see cref="DiscordChannelType.GuildText"/> and
	/// <see cref="DiscordChannelType.GuildNews"/> is supported.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("type")]
	public DiscordChannelType? Type { get; init; }

	/// <summary>
	/// New position for this channel in the channel list.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("position")]
	public Int32? Position { get; init; }

	/// <summary>
	/// New channel topic.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("topic")]
	public String? ChannelTopic { get; init; }

	/// <summary>
	/// The new NSFW status for this channel.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("nsfw")]
	public Boolean? Nsfw { get; init; }

	/// <summary>
	/// Slowmode for this channel in seconds.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("rate_limit_per_user")]
	public Int32? Slowmode { get; init; }

	/// <summary>
	/// New bitrate for this voice channel.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("bitrate")]
	public Int32? Bitrate { get; init; }

	/// <summary>
	/// New user limit for this voice channel. 0 represents no limit, 1 - 99 represents a limit.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("user_limit")]
	public Int32? UserLimit { get; init; }

	/// <summary>
	/// New permission overwrites for this channel or category.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("permission_overwrites")]
	public IEnumerable<DiscordChannelOverwrite>? PermissionOverwrites { get; init; }

	/// <summary>
	/// Snowflake identifier of the new parent category channel.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("parent_id")]
	public Int64? ParentId { get; init; }

	/// <summary>
	/// Channel voice region ID, automatic when set to <c>null</c>.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonConverter(typeof(OptionalParameterJsonConverterFactory))]
	[JsonPropertyName("rtc_region")]
	public OptionalParameter<String>? VoiceChannelRegion { get; init; }

	/// <summary>
	/// The new camera video quality mode for this channel.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("video_quality_mode")]
	public DiscordVideoQualityMode? VideoQualityMode { get; init; }

	/// <summary>
	/// New default auto archive duration for threads as used by the discord client.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("default_auto_archive_duration")]
	public Int32? DefaultAutoArchiveDuration { get; init; }
}

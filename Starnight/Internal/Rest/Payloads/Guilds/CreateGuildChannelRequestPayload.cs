namespace Starnight.Internal.Rest.Payloads.Guilds;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Channels;

/// <summary>
/// Represents the REST payload for POST /guilds/:guild_id/channels
/// </summary>
public record CreateGuildChannelRequestPayload
{
	/// <summary>
	/// The name of the channel to be created.
	/// </summary>
	[JsonPropertyName("name")]
	public String Name { get; init; } = null!;

	/// <summary>
	/// The channel type.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("type")]
	public DiscordChannelType? Type { get; init; }

	/// <summary>
	/// The channel topic.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	[JsonPropertyName("topic")]
	public String? Topic { get; init; }

	/// <summary>
	/// The voice channel bitrate.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("bitrate")]
	public Int32? Bitrate { get; init; }

	/// <summary>
	/// The voice channel user limit.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("user_limit")]
	public Int32? UserLimit { get; init; }

	/// <summary>
	/// The user slowmode in seconds.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("rate_limit_per_user")]
	public Int32? Slowmode { get; init; }

	/// <summary>
	/// The sorting position in the channel list for this channel.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("position")]
	public Int32? Position { get; init; }

	/// <summary>
	/// The permission overwrites for this channel.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	[JsonPropertyName("permission_overwrites")]
	public DiscordChannelOverwrite[]? PermissionOverwrites { get; init; }

	/// <summary>
	/// The category channel ID for this channel.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("parent_id")]
	public Int64? ParentChannelId { get; init; }

	/// <summary>
	/// Whether this channel is to be created as NSFW.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("nsfw")]
	public Boolean? Nsfw { get; init; }
}

namespace Starnight.Internal.Rest.Payloads.Guilds;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a payload for PATCH /guilds/:guild_id/channels
/// </summary>
public record ModifyGuildChannelPositionRequestPayload
{
	/// <summary>
	/// Snowflake identifier of the channel to be modified.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("id")]
	public Int64 ChannelId { get; init; }

	/// <summary>
	/// The new sorting position for this channel.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("position")]
	public Int32? Position { get; init; }

	/// <summary>
	/// Whether this channel should sync permissions with its new parent, if moving to a new parent category.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("lock_permissions")]
	public Boolean? SyncPermissions { get; init; }

	/// <summary>
	/// Snowflake identifier of this channels new parent channel.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("parent_id")]
	public Int64? ParentChannelId { get; init; }
}

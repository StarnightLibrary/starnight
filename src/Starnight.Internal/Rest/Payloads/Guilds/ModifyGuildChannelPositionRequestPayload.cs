namespace Starnight.Internal.Rest.Payloads.Guilds;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a payload for PATCH /guilds/:guild_id/channels
/// </summary>
public sealed record ModifyGuildChannelPositionRequestPayload
{
	/// <summary>
	/// Snowflake identifier of the channel to be modified.
	/// </summary>
	[JsonPropertyName("id")]
	public required Int64 ChannelId { get; init; }

	/// <summary>
	/// The new sorting position for this channel.
	/// </summary>
	[JsonPropertyName("position")]
	public Int32? Position { get; init; }

	/// <summary>
	/// Whether this channel should sync permissions with its new parent, if moving to a new parent category.
	/// </summary>
	[JsonPropertyName("lock_permissions")]
	public Boolean? SyncPermissions { get; init; }

	/// <summary>
	/// Snowflake identifier of this channels new parent channel.
	/// </summary>
	[JsonPropertyName("parent_id")]
	public Int64? ParentChannelId { get; init; }
}

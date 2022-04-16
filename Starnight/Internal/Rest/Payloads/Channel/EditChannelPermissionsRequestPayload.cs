namespace Starnight.Internal.Rest.Payloads.Channel;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities;
using Starnight.Internal.Entities.Channels;

/// <summary>
/// Represents a payload to PUT /channels/:channel_id/permissions/:overwrite_id.
/// </summary>
public record EditChannelPermissionsRequestPayload
{
	/// <summary>
	/// Overwrite type - either role or member.
	/// </summary>
	[JsonPropertyName("type")]
	public DiscordChannelOverwriteType Type { get; init; }

	/// <summary>
	/// Granted permissions, by overwrite, see <see cref="DiscordPermissions"/>
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("allow")]
	public Int64 Allow { get; init; }

	/// <summary>
	/// Denied permissions, by overwrite, see <see cref="DiscordPermissions"/>
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("deny")]
	public Int64 Deny { get; init; }
}

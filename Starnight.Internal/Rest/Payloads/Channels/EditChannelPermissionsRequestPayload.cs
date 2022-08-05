namespace Starnight.Internal.Rest.Payloads.Channels;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities;
using Starnight.Internal.Entities.Channels;

/// <summary>
/// Represents a payload to PUT /channels/:channel_id/permissions/:overwrite_id.
/// </summary>
public sealed record EditChannelPermissionsRequestPayload
{
	/// <summary>
	/// Overwrite type - either role or member.
	/// </summary>
	[JsonPropertyName("type")]
	public DiscordChannelOverwriteType Type { get; init; }

	/// <summary>
	/// Granted permissions, by overwrite, see <see cref="DiscordPermissions"/>
	/// </summary>
	[JsonPropertyName("allow")]
	public Optional<Int64?> Allow { get; init; }

	/// <summary>
	/// Denied permissions, by overwrite, see <see cref="DiscordPermissions"/>
	/// </summary>
	[JsonPropertyName("deny")]
	public Optional<Int64?> Deny { get; init; }
}

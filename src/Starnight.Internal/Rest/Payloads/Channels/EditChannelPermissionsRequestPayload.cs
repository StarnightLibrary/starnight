namespace Starnight.Internal.Rest.Payloads.Channels;

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
	public required DiscordChannelOverwriteType Type { get; init; }

	/// <summary>
	/// Granted permissions, by overwrite.
	/// </summary>
	[JsonPropertyName("allow")]
	public Optional<DiscordPermissions?> Allow { get; init; }

	/// <summary>
	/// Denied permissions, by overwrite.
	/// </summary>
	[JsonPropertyName("deny")]
	public Optional<DiscordPermissions?> Deny { get; init; }
}

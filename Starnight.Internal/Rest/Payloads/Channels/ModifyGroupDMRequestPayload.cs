namespace Starnight.Internal.Rest.Payloads.Channels;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a payload to PATCH /channels/:channel_id, where the channel ID points to a group DM.
/// </summary>
public record ModifyGroupDMRequestPayload
{
	/// <summary>
	/// The new name for this channel.
	/// </summary>
	[JsonPropertyName("name")]
	public Optional<String?> Name { get; init; }

	/// <summary>
	/// The new icon for this channel, as read from a png file. This cannot be base64-encoded.
	/// </summary>
	[JsonPropertyName("icon")]
	public Optional<Byte[]?> Icon { get; init; }
}

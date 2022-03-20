namespace Starnight.Internal.Rest.Payloads.Channel;

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
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("name")]
	public String? Name { get; init; }

	/// <summary>
	/// The new icon for this channel, as read from a png file. This cannot be base64-encoded.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("icon")]
	public Byte[]? Icon { get; init; }
}

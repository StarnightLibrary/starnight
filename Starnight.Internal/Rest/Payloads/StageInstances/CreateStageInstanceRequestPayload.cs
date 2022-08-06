namespace Starnight.Internal.Rest.Payloads.StageInstances;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Voice;

/// <summary>
/// Represents a payload to POST /stage-instances.
/// </summary>
public sealed record CreateStageInstanceRequestPayload
{
	/// <summary>
	/// The snowflake identifier of the parent stage channel.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public required Int64 ChannelId { get; init; }

	/// <summary>
	/// The topic of the stage instance.
	/// </summary>
	[JsonPropertyName("topic")]
	public required String Topic { get; init; }

	/// <summary>
	/// The privacy levvel of the stage instance.
	/// </summary>
	[JsonPropertyName("privacy_level")]
	public Optional<DiscordStagePrivacyLevel> PrivacyLevel { get; init; }

	/// <summary>
	/// Specifies whether @everyone should be notified that a stage instance has started.
	/// </summary>
	[JsonPropertyName("send_start_notification")]
	public Optional<Boolean> SendStartNotification { get; init; }
}

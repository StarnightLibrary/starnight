namespace Starnight.Internal.Rest.Payloads.StageInstances;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Voice;

/// <summary>
/// Represents a payload to POST /stage-instances.
/// </summary>
public record CreateStageInstanceRequestPayload
{
	/// <summary>
	/// The snowflake identifier of the parent stage channel.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("channel_id")]
	public Int64 ChannelId { get; init; }

	/// <summary>
	/// The topic of the stage instance.
	/// </summary>
	[JsonPropertyName("topic")]
	public String Topic { get; init; } = null!;

	/// <summary>
	/// The privacy levvel of the stage instance.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("privacy_level")]
	public DiscordStagePrivacyLevel? PrivacyLevel { get; init; }

	/// <summary>
	/// Specifies whether @everyone should be notified that a stage instance has started.
	/// </summary>
	[JsonPropertyName("send_start_notification")]
	public Boolean? SendStartNotification { get; init; }
}

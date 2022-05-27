namespace Starnight.Internal.Rest.Payloads.StageInstances;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Voice;

/// <summary>
/// Represents a payload to PATCH /stage-instances/:channel_id.
/// </summary>
public record ModifyStageInstanceRequestPayload
{
	/// <summary>
	/// The topic of the stage instance.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("topic")]
	public String? Topic { get; init; }

	/// <summary>
	/// The privacy level of the stage instance.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("privacy_level")]
	public DiscordStagePrivacyLevel? PrivacyLevel { get; init; }
}

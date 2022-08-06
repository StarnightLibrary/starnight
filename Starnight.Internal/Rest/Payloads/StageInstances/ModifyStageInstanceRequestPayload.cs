namespace Starnight.Internal.Rest.Payloads.StageInstances;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Voice;

/// <summary>
/// Represents a payload to PATCH /stage-instances/:channel_id.
/// </summary>
public sealed record ModifyStageInstanceRequestPayload
{
	/// <summary>
	/// The topic of the stage instance.
	/// </summary>
	[JsonPropertyName("topic")]
	public Optional<String> Topic { get; init; }

	/// <summary>
	/// The privacy level of the stage instance.
	/// </summary>
	[JsonPropertyName("privacy_level")]
	public Optional<DiscordStagePrivacyLevel> PrivacyLevel { get; init; }
}

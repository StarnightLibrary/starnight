namespace Starnight.Internal.Rest.Payloads.AutoModeration;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guilds.AutoModeration;

/// <summary>
/// Represents a payload to POST /guilds/:guild_id/auto-moderation/rules
/// </summary>
public record CreateAutoModerationRuleRequestPayload
{
	/// <summary>
	/// The rule name.
	/// </summary>
	[JsonPropertyName("name")]
	public String Name { get; init; } = null!;

	/// <summary>
	/// The event type this rule checks on.
	/// </summary>
	[JsonPropertyName("event_type")]
	public DiscordAutoModerationEventType EventType { get; init; }

	/// <summary>
	/// The trigger type this rule triggers on.
	/// </summary>
	[JsonPropertyName("trigger_type")]
	public DiscordAutoModerationTriggerType TriggerType { get; init; }

	/// <summary>
	/// Optional trigger metadata for this rule.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("trigger_metadata")]
	public DiscordAutoModerationTriggerMetadata? Metadata { get; init; }

	/// <summary>
	/// Actions to be executed when the rule is triggered.
	/// </summary>
	[JsonPropertyName("actions")]
	public IEnumerable<DiscordAutoModerationAction> Actions { get; init; } = null!;

	/// <summary>
	/// Specifies whether or not the rule is disabled.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("enabled")]
	public Boolean? Enabled { get; init; }

	/// <summary>
	/// List of up to 20 role IDs that shall not be affected by this rule.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("exempt_roles")]
	public IEnumerable<Int64>? ExemptRoles { get; init; }

	/// <summary>
	/// List of up to 20 channel IDs that shall not be affected by this rule.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("exempt_channels")]
	public IEnumerable<Int64>? ExemptChannels { get; init; }
}

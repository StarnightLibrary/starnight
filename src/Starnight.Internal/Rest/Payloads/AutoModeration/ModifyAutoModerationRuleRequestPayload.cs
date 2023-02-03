namespace Starnight.Internal.Rest.Payloads.AutoModeration;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guilds.AutoModeration;

/// <summary>
/// Represents a request payload to PATCH /guilds/:guild_id/auto-moderation/rules/:auto_moderation_rule_id.
/// </summary>
public sealed record ModifyAutoModerationRuleRequestPayload
{
	/// <summary>
	/// The new rule name.
	/// </summary>
	[JsonPropertyName("name")]
	public Optional<String> Name { get; init; }

	/// <summary>
	/// The new event type this rule will check on.
	/// </summary>
	[JsonPropertyName("event_type")]
	public Optional<DiscordAutoModerationEventType> EventType { get; init; }

	/// <summary>
	/// The new trigger metadata.
	/// </summary>
	[JsonPropertyName("trigger_metadata")]
	public Optional<DiscordAutoModerationTriggerMetadata> TriggerMetadata { get; init; }

	/// <summary>
	/// The new actions to execute when something triggers this rule.
	/// </summary>
	[JsonPropertyName("actions")]
	public Optional<IEnumerable<DiscordAutoModerationAction>> Actions { get; init; }

	/// <summary>
	/// Whether the rule shall be enabled.
	/// </summary>
	[JsonPropertyName("enabled")]
	public Optional<Boolean> Enabled { get; init; }

	/// <summary>
	/// The role IDs that should be exempt from this rule.
	/// </summary>
	/// <remarks>
	/// If you wish to retain any previously exempted role IDs they have to be passed here as well.
	/// </remarks>
	[JsonPropertyName("exempt_roles")]
	public Optional<IEnumerable<Int64>> ExemptRoles { get; init; }

	/// <summary>
	/// The channel IDs that should be exempt from this rule.
	/// </summary>
	/// <remarks>
	/// If you wish to retain any previously exempted channel IDs they have to be passed here as well.
	/// </remarks>
	[JsonPropertyName("exempt_channels")]
	public Optional<IEnumerable<Int64>> ExemptChannels { get; init; }
}

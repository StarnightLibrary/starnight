namespace Starnight.Internal.Entities.Guilds.AutoModeration;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Entities;

/// <summary>
/// Represents an automod rule.
/// </summary>
public sealed record DiscordAutoModerationRule : DiscordSnowflakeObject
{
	/// <summary>
	/// The snowflake identifier of the guild this rule belongs to.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public required Int64 GuildId { get; init; }

	/// <summary>
	/// The name of this rule.
	/// </summary>
	[JsonPropertyName("name")]
	public required String Name { get; init; }

	/// <summary>
	/// The snowflake identifier of the user who created this rule.
	/// </summary>
	[JsonPropertyName("creator_id")]
	public required Int64 CreatorId { get; init; }

	/// <summary>
	/// The event type this rule fires on.
	/// </summary>
	[JsonPropertyName("event_type")]
	public required DiscordAutoModerationEventType EventType { get; init; }

	/// <summary>
	/// The trigger type this rule fires on.
	/// </summary>
	[JsonPropertyName("trigger_type")]
	public required DiscordAutoModerationTriggerType TriggerType { get; init; }

	/// <summary>
	/// The trigger metadata associated with this object.
	/// </summary>
	[JsonPropertyName("metadata")]
	public required DiscordAutoModerationTriggerMetadata Metadata { get; init; }

	/// <summary>
	/// The actions executed when this rule is triggered.
	/// </summary>
	[JsonPropertyName("actions")]
	public required IEnumerable<DiscordAutoModerationAction> Actions { get; init; }

	/// <summary>
	/// Specifies whether or not the rule is enabled.
	/// </summary>
	[JsonPropertyName("enabled")]
	public required Boolean Enabled { get; init; }

	/// <summary>
	/// The role IDs that shall not be affected by this rule, maximum 20.
	/// </summary>
	[JsonPropertyName("exempt_roles")]
	public required IEnumerable<Int64>? ExemptRoles { get; init; }

	/// <summary>
	/// The channels where this rule shall not apply, maximum 50.
	/// </summary>
	[JsonPropertyName("exempt_channels")]
	public required IEnumerable<Int64>? ExemptChannels { get; init; }
}

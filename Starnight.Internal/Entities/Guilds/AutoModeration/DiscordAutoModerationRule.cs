namespace Starnight.Internal.Entities.Guilds.AutoModeration;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents an automod rule.
/// </summary>
public record DiscordAutoModerationRule : DiscordSnowflakeObject
{
	/// <summary>
	/// The snowflake identifier of the guild this rule belongs to.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("guild_id")]
	public Int64 GuildId { get; init; }

	/// <summary>
	/// The name of this rule.
	/// </summary>
	[JsonPropertyName("name")]
	public String Name { get; init; } = null!;

	/// <summary>
	/// The snowflake identifier of the user who created this rule.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("creator_id")]
	public Int64 CreatorId { get; init; }

	/// <summary>
	/// The event type this rule fires on.
	/// </summary>
	[JsonPropertyName("event_type")]
	public DiscordAutoModerationEventType EventType { get; init; }

	/// <summary>
	/// The trigger type this rule fires on.
	/// </summary>
	[JsonPropertyName("trigger_type")]
	public DiscordAutoModerationTriggerType TriggerType { get; init; }

	/// <summary>
	/// The trigger metadata associated with this object.
	/// </summary>
	[JsonPropertyName("metadata")]
	public DiscordAutoModerationTriggerMetadata Metadata { get; init; } = null!;

	/// <summary>
	/// The actions executed when this rule is triggered.
	/// </summary>
	[JsonPropertyName("actions")]
	public IEnumerable<DiscordAutoModerationAction> Actions { get; init; } = null!;

	/// <summary>
	/// Specifies whether or not the rule is enabled.
	/// </summary>
	[JsonPropertyName("enabled")]
	public Boolean Enabled { get; init; }

	/// <summary>
	/// The role IDs that shall not be affected by this rule, maximum 20.
	/// </summary>
	[JsonPropertyName("exempt_roles")]
	public IEnumerable<Int64>? ExemptRoles { get; init; }

	/// <summary>
	/// The channels where this rule shall not apply, maximum 50.
	/// </summary>
	[JsonPropertyName("exempt_channels")]
	public IEnumerable<Int64>? ExemptChannels { get; init; }
}

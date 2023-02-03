namespace Starnight.Internal.Entities.Guilds.Audit;

using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Entities.Guilds.AutoModeration;
using Starnight.Internal.Entities.Users;

/// <summary>
/// Represents a chunk of between 1 and 100 audit log events.
/// </summary>
public sealed record DiscordAuditLogObject
{
	/// <summary>
	/// Holds between 1 and 100 audit log entries.
	/// </summary>
	[JsonPropertyName("audit_log_entries")]
	public required IEnumerable<DiscordAuditLogEntry> Entries { get; init; }

	/// <summary>
	/// A list of all auto moderation rules found in this audit log chunk.
	/// </summary>
	[JsonPropertyName("auto_moderation_rules")]
	public required IEnumerable<DiscordAutoModerationRule> ReferencedAutoModerationRules { get; init; }

	/// <summary>
	/// A list of all scheduled events found in this audit log chunk.
	/// </summary>
	[JsonPropertyName("guild_scheduled_events")]
	public required IEnumerable<DiscordScheduledEvent> ReferencedScheduledEvents { get; init; }

	/// <summary>
	/// A list of all integrations found in this audit log chunk.
	/// </summary>
	[JsonPropertyName("integrations")]
	public required IEnumerable<DiscordGuildIntegration> ReferencedIntegrations { get; init; }

	/// <summary>
	/// A list of all threads found in this audit log chunk.
	/// </summary>
	[JsonPropertyName("threads")]
	public required IEnumerable<DiscordChannel> ReferencedThreads { get; init; }

	/// <summary>
	/// A list of all users found in this audit log chunk.
	/// </summary>
	[JsonPropertyName("users")]
	public required IEnumerable<DiscordUser> ReferencedUsers { get; init; }

	/// <summary>
	/// A list of all webhooks found in this audit log chunk.
	/// </summary>
	[JsonPropertyName("webhooks")]
	public required IEnumerable<DiscordWebhook> ReferencedWebhooks { get; init; }
}

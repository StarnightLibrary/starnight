namespace Starnight.Internal.Entities.Guild.Audit;

using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Channel;
using Starnight.Internal.Entities.User;

/// <summary>
/// Represents a chunk of between 1 and 100 audit log events.
/// </summary>
public class DiscordAuditLogObject
{
	/// <summary>
	/// Holds between 1 and 100 audit log entries.
	/// </summary>
	[JsonPropertyName("audit_log_entries")]
	public DiscordAuditLogEntry[] Entries { get; init; } = default!;

	/// <summary>
	/// A list of all scheduled events found in this audit log chunk.
	/// </summary>
	[JsonPropertyName("guild_scheduled_events")]
	public DiscordScheduledEvent[]? ReferencedScheduledEvents { get; init; }

	/// <summary>
	/// A list of all integrations found in this audit log chunk.
	/// </summary>
	[JsonPropertyName("integrations")]
	public DiscordGuildIntegration[]? ReferencedIntegrations { get; init; }

	/// <summary>
	/// A list of all threads found in this audit log chunk.
	/// </summary>
	[JsonPropertyName("threads")]
	public DiscordChannel[]? ReferencedThreads { get; init; }

	/// <summary>
	/// A list of all users found in this audit log chunk.
	/// </summary>
	[JsonPropertyName("users")]
	public DiscordUser[]? ReferencedUsers { get; init; }

	/// <summary>
	/// A list of all webhooks found in this audit log chunk.
	/// </summary>
	[JsonPropertyName("webhooks")]
	public DiscordWebhook[]? ReferencedWebhooks { get; init; }
}

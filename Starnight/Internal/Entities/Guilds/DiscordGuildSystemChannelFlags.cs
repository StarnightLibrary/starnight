namespace Starnight.Internal.Entities.Guilds;

using System;

/// <summary>
/// Represents flags for a guild system channel.
/// </summary>
[Flags]
public enum DiscordGuildSystemChannelFlags
{
	/// <summary>
	/// Suppress join notifications.
	/// </summary>
	SuppressJoinNotification = 1 << 0,

	/// <summary>
	/// Suppress server boost notifications.
	/// </summary>
	SuppressPremiumSubscriptions = 1 << 1,

	/// <summary>
	/// Suppress server setup tips.
	/// </summary>
	SuppressGuildReminderNotifications = 1 << 2,

	/// <summary>
	/// Disable sticker reply buttons on member join notifications.
	/// </summary>
	SuppressJoinNotificationReplies = 1 << 3
}

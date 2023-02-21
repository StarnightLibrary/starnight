namespace Starnight.Entities;

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
	SuppressJoinNotificationReplies = 1 << 3,

	/// <summary>
	/// Suppress role subscription purchase and renewal notifications.
	/// </summary>
	SuppressRoleSubscriptionPurchaseNotifications = 1 << 4,

	/// <summary>
	/// Hide role subscription sticker reply buttons.
	/// </summary>
	SuppressRoleSubscriptionPurchaseNotificationReplies = 1 << 5
}

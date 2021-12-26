namespace Starnight.Internal.Entities.Guild;

/// <summary>
/// Represents the different possible standard notification levels.
/// </summary>
public enum DiscordGuildMessageNotificationsLevel
{
	/// <summary>
	/// Members will receive notifications for all messages.
	/// </summary>
	AllMessages,

	/// <summary>
	/// Members will only receive notifications for messages mentioning them.
	/// </summary>
	OnlyMentions
}

namespace Starnight.Internal.Entities.Guilds.AutoModeration;

/// <summary>
/// Represents the different actions the auto moderation can take if a rule violation is detected.
/// </summary>
public enum DiscordAutoModerationActionType
{
	/// <summary>
	/// Blocks the message from being sent.
	/// </summary>
	BlockMessage = 1,

	/// <summary>
	/// Logs the message to a channel specified in the action metadata.
	/// </summary>
	SendAlertMessage,

	/// <summary>
	/// Times the user out for a specified duration.
	/// </summary>
	/// <remarks>
	/// This action type is only valid on <see cref="DiscordAutoModerationTriggerType.Keyword"/> and
	/// <see cref="DiscordAutoModerationTriggerType.KeywordPreset"/> rules.
	/// </remarks>
	Timeout
}

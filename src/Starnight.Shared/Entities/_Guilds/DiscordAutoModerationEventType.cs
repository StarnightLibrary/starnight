namespace Starnight.Entities;

/// <summary>
/// Indicates in what context a rule should be checked
/// </summary>
public enum DiscordAutoModerationEventType
{
	/// <summary>
	/// When a member sends or <b>edits</b> a message in the guild.
	/// </summary>
	MessageSend = 1
}

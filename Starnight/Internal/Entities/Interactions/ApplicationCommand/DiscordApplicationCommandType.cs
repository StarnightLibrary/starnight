namespace Starnight.Internal.Entities.Interactions.ApplicationCommand;

/// <summary>
/// Enumerates the different kinds of application commands.
/// </summary>
public enum DiscordApplicationCommandType
{
	/// <summary>
	/// Slash commands, displayed up above the chat bar when typing a '/'.
	/// </summary>
	ChatInput = 1,

	/// <summary>
	/// Right-click commands on a user.
	/// </summary>
	User,

	/// <summary>
	/// Right-click commands on a message.
	/// </summary>
	Message
}

namespace Starnight.Entities;

using Starnight.Internal.Entities.Interactions.ApplicationCommands;

/// <summary>
/// Enumerates the different types an application command option can take.
/// </summary>
public enum DiscordApplicationCommandOptionType
{
	SubCommand = 1,
	SubCommandGroup,
	String,

	/// <summary>
	/// Any integer between 2^53 and -2^53.
	/// </summary>
	Integer,
	Boolean,
	User,

	/// <summary>
	/// Includes <b>all</b> channel types, even categories. May be restricted using
	/// <see cref="DiscordApplicationCommandOption.ChannelTypes"/>.
	/// </summary>
	Channel,
	Role,

	/// <summary>
	/// Includes users and roles.
	/// </summary>
	Mentionable,

	/// <summary>
	/// Any decimal number between 2^53 and -2^53.
	/// </summary>
	Decimal,
	Attachment
}

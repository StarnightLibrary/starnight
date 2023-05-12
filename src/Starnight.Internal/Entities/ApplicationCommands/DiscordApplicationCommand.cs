namespace Starnight.Internal.Entities.ApplicationCommands;

using Starnight.Entities;

/// <summary>
/// Represents a single application command.
/// </summary>
public sealed record DiscordApplicationCommand : DiscordSnowflakeObject
{
	/// <summary>
	/// The type of the this command.
	/// </summary>
	public Optional<DiscordApplicationCommandType> Type { get; init; }
}

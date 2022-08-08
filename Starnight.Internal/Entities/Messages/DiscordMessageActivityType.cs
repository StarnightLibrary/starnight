namespace Starnight.Internal.Entities.Messages;

/// <summary>
/// Represents the different discord message activity types.
/// </summary>
/// <remarks>
/// Documentation for the different fields is missing: where the names are not self explanatory you are on your own.
/// Feel free to PR documentation. Additionally, instead of attempting to use more sane names than discord uses,
/// these names are only changed inasmuch from the discord docs as to conform with the C# naming rules.
/// </remarks>
public enum DiscordMessageActivityType
{
	Join = 1,
	Spectate,
	Listen,
	JoinRequest = 5
}
